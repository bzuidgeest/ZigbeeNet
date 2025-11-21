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

            // Determine return type
            var returnType = DetermineReturnType(frameDefinition, sanitizedCommandName);

            // Generate parameters
            var parameters = GenerateParameters(frameDefinition);

            // Generate method body
            var methodBody = GenerateMethodBody(frameDefinition, sanitizedCommandName);

            // Create method declaration
            var method = MethodDeclaration(returnType, sanitizedCommandName)
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(parameters.ToArray())
                .WithBody(methodBody)
                .WithLeadingTrivia(GenerateFunctionXmlDocumentation(frameDefinition, sanitizedCommandName));

            return method;
        }

        private TypeSyntax DetermineReturnType(FrameDefinition frameDefinition, string sanitizedCommandName)
        {
            if (frameDefinition.ResponseArguments.Count > 1)
            {
                // Multiple return values - return tuple
                var tupleElements = frameDefinition.ResponseArguments
                    .Select(arg => TupleElement(
                        ParseTypeName(_textService.GenerateVariableType(arg)),
                        Identifier(Sanitize.AsPropertyName(arg.Name))))
                    .ToArray();
                return TupleType(SeparatedList(tupleElements));
            }
            else if (frameDefinition.ResponseArguments.Count == 1)
            {
                // Single return value
                return ParseTypeName(_textService.GenerateVariableType(frameDefinition.ResponseArguments[0]));
            }
            else
            {
                // No return values - return response object
                return ParseTypeName($"{sanitizedCommandName}Response");
            }
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

            // Create transaction
            statements.Add(LocalDeclarationStatement(
                VariableDeclaration(ParseTypeName("ITransaction"))
                    .AddVariables(VariableDeclarator(Identifier("transaction"))
                        .WithInitializer(EqualsValueClause(
                            InvocationExpression(
                                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("_protocolHandler"),
                                    IdentifierName("SendTransaction")))
                            .AddArgumentListArguments(
                                Argument(ObjectCreationExpression(ParseTypeName("SingleResponseTransaction"))
                                    .AddArgumentListArguments(
                                        Argument(IdentifierName("request")),
                                        Argument(TypeOfExpression(ParseTypeName($"{sanitizedCommandName}Response")))))))))));

            // Get response
            statements.Add(LocalDeclarationStatement(
                VariableDeclaration(ParseTypeName($"{sanitizedCommandName}Response"))
                    .AddVariables(VariableDeclarator(Identifier("response"))
                        .WithInitializer(EqualsValueClause(
                            CastExpression(ParseTypeName($"{sanitizedCommandName}Response"),
                                InvocationExpression(
                                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("transaction"),
                                        IdentifierName("GetResponse")))))))));

            // Log response
            statements.Add(ExpressionStatement(
                InvocationExpression(
                    MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("_logger"),
                        IdentifierName("LogDebug")))
                .AddArgumentListArguments(
                    Argument(InvocationExpression(
                        MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("response"),
                            IdentifierName("ToString")))))));

            // Return statement
            statements.Add(GenerateReturnStatement(frameDefinition));

            return Block(statements);
        }

        private StatementSyntax GenerateReturnStatement(FrameDefinition frameDefinition)
        {
            if (frameDefinition.ResponseArguments.Count > 1)
            {
                // Return tuple
                var tupleElements = frameDefinition.ResponseArguments
                    .Select(arg => Argument(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("response"),
                        IdentifierName(Sanitize.AsPropertyName(arg.Name)))))
                    .ToArray();
                return ReturnStatement(TupleExpression(SeparatedList(tupleElements)));
            }
            else if (frameDefinition.ResponseArguments.Count == 1)
            {
                // Return single property
                return ReturnStatement(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("response"),
                    IdentifierName(Sanitize.AsPropertyName(frameDefinition.ResponseArguments[0].Name))));
            }
            else
            {
                // Return response object
                return ReturnStatement(IdentifierName("response"));
            }
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
    }
}

