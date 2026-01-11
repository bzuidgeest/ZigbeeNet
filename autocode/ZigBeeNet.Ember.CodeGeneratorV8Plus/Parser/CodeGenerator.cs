using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Models;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Services;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Utility;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Parser
{
    /// <summary>
    /// Generates EmberNCP class methods using Roslyn syntax trees instead of StringBuilder.
    /// This provides better type safety and easier manipulation of generated code.
    /// </summary>
    internal class CodeGenerator
    {
        private readonly CSharpLanguageService _textService;
        private readonly TypeMapperService _typeMapperService;

        public CodeGenerator(CSharpLanguageService textService, TypeMapperService typeMapperService)
        {
            _textService = textService;
            _typeMapperService = typeMapperService;
        }

        /// <summary>
        /// Generates a method for a frame definition with XML documentation.
        /// </summary>
        public MethodDeclarationSyntax GenerateFrameMethod(FrameDefinition frameDefinition)
        {
            string sanitizedCommandName = Sanitize.FunctionName(frameDefinition.CommandName);

            // Determine return type (wrapped in Task<>)
            var returnType = DetermineReturnType(frameDefinition, sanitizedCommandName);
            var asyncReturnType = GenericName("Task")
                .AddTypeArgumentListArguments(returnType);

            // Generate parameters
            var parameters = GenerateParameters(frameDefinition);

            // Add CancellationToken parameter with default value
            parameters.Add(
                Parameter(Identifier("cancellationToken"))
                    .WithType(ParseTypeName("CancellationToken"))
                    .WithDefault(EqualsValueClause(
                        LiteralExpression(SyntaxKind.DefaultLiteralExpression,
                            Token(SyntaxKind.DefaultKeyword)))));

            // Generate method body
            var methodBody = GenerateMethodBody(frameDefinition, sanitizedCommandName);

            // Create method declaration
            var method = MethodDeclaration(asyncReturnType, sanitizedCommandName)
                .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.AsyncKeyword))
                .AddParameterListParameters(parameters.ToArray())
                .WithBody(methodBody)
                .WithLeadingTrivia(GenerateFunctionXmlDocumentation(frameDefinition, sanitizedCommandName));

            return method;
        }

        private TypeSyntax DetermineReturnType(FrameDefinition frameDefinition, string sanitizedCommandName)
        {
            // Separate Status and non-Status return parameters
            var statusArg = frameDefinition.ResponseArguments.FirstOrDefault(arg => arg.Type == "sl_status_t");
            var nonStatusResponseArgs = frameDefinition.ResponseArguments
                .Where(arg => arg.Type != "sl_status_t")
                .ToList();

            // Case 1: No return values at all
            if (frameDefinition.ResponseArguments.Count == 0)
            {
                return ParseTypeName($"{sanitizedCommandName}Response");
            }

            // Case 2: Only Status return value
            if (statusArg != null && nonStatusResponseArgs.Count == 0)
            {
                return ParseTypeName(_textService.GenerateVariableType(statusArg));
            }

            // Case 3: Single non-Status return value (no Status)
            if (statusArg == null && nonStatusResponseArgs.Count == 1)
            {
                return ParseTypeName(_textService.GenerateVariableType(nonStatusResponseArgs[0]));
            }

            // Case 4: Status + single other return value → tuple (Status, OtherType)
            if (statusArg != null && nonStatusResponseArgs.Count == 1)
            {
                var tupleElements = new[]
                {
                    TupleElement(
                        ParseTypeName(_textService.GenerateVariableType(statusArg)),
                        Identifier(Sanitize.AsPropertyName(statusArg.Name))),
                    TupleElement(
                        ParseTypeName(_textService.GenerateVariableType(nonStatusResponseArgs[0])),
                        Identifier(Sanitize.AsPropertyName(nonStatusResponseArgs[0].Name)))
                };
                return TupleType(SeparatedList(tupleElements));
            }

            // Case 5: Multiple non-Status returns (no Status) → RecordStruct
            if (statusArg == null && nonStatusResponseArgs.Count > 1)
            {
                return ParseTypeName(sanitizedCommandName);
            }

            // Case 6: Status + multiple other returns → tuple (Status, RecordStruct)
            if (statusArg != null && nonStatusResponseArgs.Count > 1)
            {
                var tupleElements = new[]
                {
                    TupleElement(
                        ParseTypeName(_textService.GenerateVariableType(statusArg)),
                        Identifier(Sanitize.AsPropertyName(statusArg.Name))),
                    TupleElement(
                        ParseTypeName(sanitizedCommandName),
                        Identifier("Result"))
                };
                return TupleType(SeparatedList(tupleElements));
            }

            // Fallback (should not reach here)
            return ParseTypeName($"{sanitizedCommandName}Response");
        }

        private List<ParameterSyntax> GenerateParameters(FrameDefinition frameDefinition)
        {
            return frameDefinition.CommandArguments
                .Select(arg => Parameter(Identifier(arg.Name.AsFieldName()))
                    .WithType(ParseTypeName(_textService.GenerateVariableType(arg))))
                .ToList();
        }

        private BlockSyntax GenerateMethodBody(FrameDefinition frameDefinition, string sanitizedCommandName)
        {
            var statements = new List<StatementSyntax>();

            // Create request: {CommandName}Request request = new {CommandName}Request();
            statements.Add(LocalDeclarationStatement(
                VariableDeclaration(ParseTypeName($"{sanitizedCommandName}Request"))
                    .AddVariables(VariableDeclarator(Identifier("request"))
                        .WithInitializer(EqualsValueClause(ObjectCreationExpression(ParseTypeName($"{sanitizedCommandName}Request"))
                            .WithArgumentList(ArgumentList()))))));

            // Set request properties
            foreach (var commandArgument in frameDefinition.CommandArguments)
            {
                statements.Add(ExpressionStatement(
                    AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                        MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("request"),
                            IdentifierName(Sanitize.AsPropertyName(commandArgument.Name))),
                        IdentifierName(commandArgument.Name.AsFieldName()))));
            }

            // Call SendFrameAsync and cast to response type
            // {CommandName}Response? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as {CommandName}Response;
            statements.Add(LocalDeclarationStatement(
                VariableDeclaration(
                    NullableType(ParseTypeName($"{sanitizedCommandName}Response")))
                    .AddVariables(VariableDeclarator(Identifier("response"))
                        .WithInitializer(EqualsValueClause(
                            BinaryExpression(SyntaxKind.AsExpression,
                                AwaitExpression(
                                    InvocationExpression(
                                        IdentifierName("SendFrameAsync"))
                                    .AddArgumentListArguments(
                                        Argument(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("request"),
                                            IdentifierName("SequenceNumber"))),
                                        Argument(InvocationExpression(
                                            MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                                IdentifierName("request"),
                                                IdentifierName("GetFrameBytes")))),
                                        Argument(LiteralExpression(SyntaxKind.FalseLiteralExpression)),
                                        Argument(IdentifierName("cancellationToken")))),
                                ParseTypeName($"{sanitizedCommandName}Response")))))));

            // Log response: _logger.LogDebug(response?.ToString());
            statements.Add(ExpressionStatement(
                InvocationExpression(
                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("_logger"),
                        IdentifierName("LogDebug")))
                .AddArgumentListArguments(
                    Argument(
                        ConditionalAccessExpression(
                            IdentifierName("response"),
                            InvocationExpression(
                                MemberBindingExpression(IdentifierName("ToString"))))))));

            // Return statement
            statements.Add(GenerateReturnStatement(frameDefinition));

            return Block(statements);
        }

        private StatementSyntax GenerateReturnStatement(FrameDefinition frameDefinition)
        {
            // Separate Status and non-Status return parameters
            var statusArg = frameDefinition.ResponseArguments.FirstOrDefault(arg => arg.Type == "sl_status_t");
            var nonStatusResponseArgs = frameDefinition.ResponseArguments
                .Where(arg => arg.Type != "sl_status_t")
                .ToList();

            // Case 1: No return values at all
            if (frameDefinition.ResponseArguments.Count == 0)
            {
                return ReturnStatement(IdentifierName("response"));
            }

            // Case 2: Only Status return value
            if (statusArg != null && nonStatusResponseArgs.Count == 0)
            {
                return ReturnStatement(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("response"),
                    IdentifierName(Sanitize.AsPropertyName(statusArg.Name))));
            }

            // Case 3: Single non-Status return value (no Status)
            if (statusArg == null && nonStatusResponseArgs.Count == 1)
            {
                return ReturnStatement(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("response"),
                    IdentifierName(Sanitize.AsPropertyName(nonStatusResponseArgs[0].Name))));
            }

            // Case 4: Status + single other return value → tuple (Status, OtherType)
            if (statusArg != null && nonStatusResponseArgs.Count == 1)
            {
                var tupleElements = new[]
                {
                    Argument(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("response"),
                        IdentifierName(Sanitize.AsPropertyName(statusArg.Name)))),
                    Argument(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("response"),
                        IdentifierName(Sanitize.AsPropertyName(nonStatusResponseArgs[0].Name))))
                };
                return ReturnStatement(TupleExpression(SeparatedList(tupleElements)));
            }

            // Case 5: Multiple non-Status returns (no Status) → RecordStruct
            if (statusArg == null && nonStatusResponseArgs.Count > 1)
            {
                string sanitizedCommandName = Sanitize.FunctionName(frameDefinition.CommandName);
                var arguments = nonStatusResponseArgs
                    .Select(arg => Argument(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("response"),
                        IdentifierName(Sanitize.AsPropertyName(arg.Name)))))
                    .ToArray();

                return ReturnStatement(
                    ObjectCreationExpression(ParseTypeName(sanitizedCommandName))
                        .WithArgumentList(ArgumentList(SeparatedList(arguments))));
            }

            // Case 6: Status + multiple other returns → tuple (Status, RecordStruct)
            if (statusArg != null && nonStatusResponseArgs.Count > 1)
            {
                string sanitizedCommandName = Sanitize.FunctionName(frameDefinition.CommandName);
                var recordArguments = nonStatusResponseArgs
                    .Select(arg => Argument(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("response"),
                        IdentifierName(Sanitize.AsPropertyName(arg.Name)))))
                    .ToArray();

                var tupleElements = new[]
                {
                    Argument(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("response"),
                        IdentifierName(Sanitize.AsPropertyName(statusArg.Name)))),
                    Argument(ObjectCreationExpression(ParseTypeName(sanitizedCommandName))
                        .WithArgumentList(ArgumentList(SeparatedList(recordArguments))))
                };
                return ReturnStatement(TupleExpression(SeparatedList(tupleElements)));
            }

            // Fallback (should not reach here)
            return ReturnStatement(IdentifierName("response"));
        }

        /// <summary>
        /// Generates a readonly record struct with the specified name and properties.
        /// </summary>
        /// <param name="recordName">The name of the record struct</param>
        /// <param name="properties">Dictionary of property names and their C# type names</param>
        /// <param name="summary">Optional summary for XML documentation</param>
        /// <param name="propertyDescriptions">Optional dictionary of property descriptions for XML documentation</param>
        /// <param name="addLeadingBlankLine">Whether to add a leading blank line before the XML documentation</param>
        /// <returns>A RecordDeclarationSyntax representing the readonly record struct</returns>
        public RecordDeclarationSyntax GenerateRecordStruct(
            string recordName,
            Dictionary<string, string> properties,
            string? summary = null,
            Dictionary<string, string>? propertyDescriptions = null,
            bool addLeadingBlankLine = false)
        {
            // Generate parameters for the record
            var parameters = properties
                .Select(prop => Parameter(Identifier(prop.Key))
                    .WithType(ParseTypeName(prop.Value)))
                .ToArray();

            // Create record struct declaration with readonly modifier
            var recordDeclaration = RecordDeclaration(
                    Token(SyntaxKind.RecordKeyword)
                        .WithTrailingTrivia(Space),
                    recordName)
                .AddModifiers(
                    Token(SyntaxKind.PublicKeyword),
                    Token(SyntaxKind.ReadOnlyKeyword))
                .WithClassOrStructKeyword(Token(SyntaxKind.StructKeyword))
                .WithParameterList(ParameterList(SeparatedList(parameters)))
                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));

            // Add XML documentation
            var xmlDocComments = new List<SyntaxTrivia>();

            // Add leading blank line if requested
            if (addLeadingBlankLine)
            {
                xmlDocComments.Add(CarriageReturnLineFeed);
            }

            xmlDocComments.Add(Comment("/// <summary>"));
            xmlDocComments.Add(Comment($"/// {summary ?? $"Result type for {recordName}."}"));
            xmlDocComments.Add(Comment("/// </summary>"));

            if (propertyDescriptions != null)
            {
                foreach (var prop in properties)
                {
                    if (propertyDescriptions.TryGetValue(prop.Key, out var description))
                    {
                        xmlDocComments.Add(Comment($"/// <param name=\"{prop.Key}\">{description}</param>"));
                    }
                }
            }

            recordDeclaration = recordDeclaration.WithLeadingTrivia(TriviaList(xmlDocComments));

            return recordDeclaration;
        }

        private SyntaxTriviaList GenerateFunctionXmlDocumentation(FrameDefinition frameDefinition, string methodName)
        {
            var trivia = new List<SyntaxTrivia>();

            if (!string.IsNullOrWhiteSpace(frameDefinition.Description))
            {
                trivia.Add(Comment("/// <summary>"));
                Regex.Split(frameDefinition.Description, @"\r\n|[\r\n]").ToList().ForEach(line =>
                {
                    trivia.Add(Comment($"/// {line.XmlEscape()}"));
                });
                trivia.Add(Comment("/// </summary>"));

                // Add parameter documentation
                foreach (var commandArgument in frameDefinition.CommandArguments)
                {
                    IEnumerable<string> lines = Regex.Split(commandArgument.Description ?? "", @"\r\n|[\r\n]").Where(x => string.IsNullOrWhiteSpace(x) == false);
                    if (lines.Count() == 1)
                    {
                        trivia.Add(Comment($"/// <param name=\"{Sanitize.AsPropertyName(commandArgument.Name)}\">{lines.First().XmlEscape() ?? "Parameter"}</param>"));
                    }
                    else
                    {
                        trivia.Add(Comment($"/// <param name=\"{Sanitize.AsPropertyName(commandArgument.Name)}\">"));
                        foreach (string line in lines)
                        {
                            trivia.Add(Comment($"/// {line.XmlEscape()}"));
                        }
                        trivia.Add(Comment($"/// </param>"));
                    }
                }

                // Add return documentation based on return type
                if (frameDefinition.ResponseArguments.Count > 1)
                {
                    // Multiple return values - document as tuple
                    trivia.Add(Comment("/// <returns>A tuple containing:"));
                    foreach (var responseArgument in frameDefinition.ResponseArguments)
                    {
                        string argName = Sanitize.AsPropertyName(responseArgument.Name);
                        string argDescription = argName;
                        IEnumerable<string> lines = Regex.Split(responseArgument.Description ?? "", @"\r\n|[\r\n]").Where(x => string.IsNullOrWhiteSpace(x) == false);
                        if (lines.Count() <= 1)
                        {
                            trivia.Add(Comment($"/// - {argName}: {lines.FirstOrDefault() ?? ""}"));
                        }
                        else
                        {
                            trivia.Add(Comment($"/// - {argName}: {lines.First()}"));
                            foreach (string line in lines.Skip(1))
                            {
                                trivia.Add(Comment($"/// {line}"));
                            }
                        }
                    }
                    trivia.Add(Comment("/// </returns>"));
                }
                else if (frameDefinition.ResponseArguments.Count == 1)
                {
                    // Single return value - document the specific property
                    var returnArg = frameDefinition.ResponseArguments[0];
                    var returnDescription = returnArg.Description?.XmlEscape() ?? "The response value";
                    trivia.Add(Comment($"/// <returns>{returnDescription}</returns>"));
                }
                else
                {
                    // No return values - document the response object
                    trivia.Add(Comment($"/// <returns>The {methodName}Response object from the NCP</returns>"));
                }
            }

            return TriviaList(trivia);
        }

        /// <summary>
        /// Generates XML documentation for a property/field.
        /// </summary>
        public SyntaxTriviaList GeneratePropertyXmlDocumentation(FrameArgument arg)
        {
            var trivia = new List<SyntaxTrivia>();

            if (!string.IsNullOrWhiteSpace(arg.Description))
            {
                trivia.Add(Comment("/// <summary>"));
                Regex.Split(arg.Description, @"\r\n|[\r\n]").ToList().ForEach(line =>
                {
                    trivia.Add(Comment($"/// {line.XmlEscape()}"));
                });
                trivia.Add(Comment("/// </summary>"));
            }

            return TriviaList(trivia);
        }

        /// <summary>
        /// Generates XML documentation for a class.
        /// </summary>
        public SyntaxTriviaList GenerateClassXmlDocumentation(FrameDefinition frameDefinition)
        {
            var trivia = new List<SyntaxTrivia>();

            if (!string.IsNullOrWhiteSpace(frameDefinition.Description))
            {
                trivia.Add(Comment("/// <summary>"));
                Regex.Split(frameDefinition.Description, @"\r\n|[\r\n]").ToList().ForEach(line =>
                {
                    trivia.Add(Comment($"/// {line.XmlEscape()}"));
                });
                if (!string.IsNullOrWhiteSpace(frameDefinition.Value))
                {
                    trivia.Add(Comment($"/// Frame value: {frameDefinition.Value}"));
                }
                trivia.Add(Comment("/// </summary>"));
            }

            return TriviaList(trivia);
        }

        /// <summary>
        /// Generates the auto-generated header trivia for compilation units.
        /// This header indicates that the code was generated by a tool and should not be manually edited.
        /// </summary>
        public SyntaxTriviaList GenerateAutoGeneratedHeaderTrivia()
        {
            var triviaList = new List<SyntaxTrivia>();

            // Add the auto-generated header comment
            triviaList.Add(Comment("//------------------------------------------------------------------------------"));
            triviaList.Add(CarriageReturnLineFeed);
            triviaList.Add(Comment("// <auto-generated>"));
            triviaList.Add(CarriageReturnLineFeed);
            triviaList.Add(Comment("//     This code was generated by a tool."));
            triviaList.Add(CarriageReturnLineFeed);
            triviaList.Add(Comment("//"));
            triviaList.Add(CarriageReturnLineFeed);
            triviaList.Add(Comment("//     Changes to this file may cause incorrect behavior and will be lost if"));
            triviaList.Add(CarriageReturnLineFeed);
            triviaList.Add(Comment("//     the code is regenerated."));
            triviaList.Add(CarriageReturnLineFeed);
            triviaList.Add(Comment("// </auto-generated>"));
            triviaList.Add(CarriageReturnLineFeed);
            triviaList.Add(Comment("//------------------------------------------------------------------------------"));
            triviaList.Add(CarriageReturnLineFeed);
            triviaList.Add(LineFeed);

            return TriviaList(triviaList);
        }
    }
}

