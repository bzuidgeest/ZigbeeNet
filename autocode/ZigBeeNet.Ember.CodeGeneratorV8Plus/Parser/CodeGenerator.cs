using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
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
    internal class RoslynEmberNCPGenerator
    {
        private readonly CSharpLanguageService _textService;
        private readonly TypeMapperService _typeMapperService;

        public RoslynEmberNCPGenerator(CSharpLanguageService textService, TypeMapperService typeMapperService)
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
                .WithLeadingTrivia(GenerateXmlDocumentation(frameDefinition, sanitizedCommandName));

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
                        Identifier(Sanitize.PropertyName(arg.Name))))
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
                .Select(arg => Parameter(Identifier(Sanitize.PropertyName(arg.Name)))
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
                        .WithInitializer(EqualsValueClause(
                            ObjectCreationExpression(ParseTypeName($"{sanitizedCommandName}Request")))))));

            // Create transaction
            statements.Add(LocalDeclarationStatement(
                VariableDeclaration(ParseTypeName("IEzspTransaction"))
                    .AddVariables(VariableDeclarator(Identifier("transaction"))
                        .WithInitializer(EqualsValueClause(
                            InvocationExpression(
                                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("_protocolHandler"),
                                    IdentifierName("SendEzspTransaction")))
                            .AddArgumentListArguments(
                                Argument(ObjectCreationExpression(ParseTypeName("EzspSingleResponseTransaction"))
                                    .AddArgumentListArguments(
                                        Argument(IdentifierName("request")),
                                        Argument(TypeOfExpression(ParseTypeName($"{sanitizedCommandName}Response"))))))))));

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
                        IdentifierName(Sanitize.PropertyName(arg.Name)))))
                    .ToArray();
                return ReturnStatement(TupleExpression(SeparatedList(tupleElements)));
            }
            else if (frameDefinition.ResponseArguments.Count == 1)
            {
                // Return single property
                return ReturnStatement(MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("response"),
                    IdentifierName(Sanitize.PropertyName(frameDefinition.ResponseArguments[0].Name))));
            }
            else
            {
                // Return response object
                return ReturnStatement(IdentifierName("response"));
            }
        }

        private SyntaxTriviaList GenerateXmlDocumentation(FrameDefinition frameDefinition, string methodName)
        {
            var trivia = new List<SyntaxTrivia>();

            if (!string.IsNullOrWhiteSpace(frameDefinition.Description))
            {
                trivia.Add(Comment("/// <summary>"));
                trivia.Add(Comment($"/// {frameDefinition.Description.XmlEscape()}"));
                trivia.Add(Comment("/// </summary>"));

                // Add parameter documentation
                foreach (var param in frameDefinition.CommandArguments)
                {
                    trivia.Add(Comment($"/// <param name=\"{Sanitize.PropertyName(param.Name)}\">{param.Description?.XmlEscape() ?? "Parameter"}</param>"));
                }

                // Add return documentation based on return type
                if (frameDefinition.ResponseArguments.Count > 1)
                {
                    // Multiple return values - document as tuple
                    trivia.Add(Comment("/// <returns>A tuple containing:"));
                    foreach (var arg in frameDefinition.ResponseArguments)
                    {
                        var argName = Sanitize.PropertyName(arg.Name);
                        var argDescription = arg.Description?.XmlEscape() ?? argName;
                        trivia.Add(Comment($"/// - {argName}: {argDescription}"));
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

