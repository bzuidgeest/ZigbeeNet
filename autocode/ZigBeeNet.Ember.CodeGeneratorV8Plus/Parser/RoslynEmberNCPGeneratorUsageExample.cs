using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Models;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Services;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Parser
{
    /// <summary>
    /// Example showing how to use RoslynEmberNCPGenerator to generate the EmberNCP class.
    /// This replaces the StringBuilder-based approach with Roslyn syntax trees.
    /// </summary>
    internal class RoslynEmberNCPGeneratorUsageExample
    {
        private readonly RoslynEmberNCPGenerator _generator;
        private readonly CSharpLanguageService _textService;

        public RoslynEmberNCPGeneratorUsageExample(CSharpLanguageService textService, TypeMapperService typeMapperService)
        {
            _textService = textService;
            _generator = new RoslynEmberNCPGenerator(textService, typeMapperService);
        }

        /// <summary>
        /// Generates the EmberNCP class with all frame methods using Roslyn.
        /// This is equivalent to the StringBuilder approach but with better type safety.
        /// </summary>
        public CompilationUnitSyntax GenerateEmberNCPClass(List<EzspSection> sections)
        {
            // Create using statements
            var usings = new[]
            {
                UsingDirective(ParseName("System")),
                UsingDirective(ParseName("System.Collections.Generic")),
                UsingDirective(ParseName("ZigBeeNet.Hardware.EmberV8Plus.Transaction")),
                UsingDirective(ParseName("ZigBeeNet.Hardware.EmberV8Plus.Internal")),
                UsingDirective(ParseName("ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations")),
                UsingDirective(ParseName("ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types")),
                UsingDirective(ParseName("Microsoft.Extensions.Logging")),
                UsingDirective(ParseName("ZigBeeNet.Util")),
            };

            // Create class members
            var classMembers = new List<MemberDeclarationSyntax>();

            // Add logger field
            classMembers.Add(FieldDeclaration(
                VariableDeclaration(ParseTypeName("ILogger"))
                    .AddVariables(VariableDeclarator(Identifier("_logger"))
                        .WithInitializer(EqualsValueClause(
                            InvocationExpression(
                                MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression,
                                    IdentifierName("LogManager"),
                                    GenericName(Identifier("GetLog"))
                                    .AddTypeArgumentListArguments(ParseTypeName("EmberNcp"))))))))
                .AddModifiers(Token(SyntaxKind.StaticKeyword), Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)));

            // Add protocol handler field
            classMembers.Add(FieldDeclaration(
                VariableDeclaration(ParseTypeName("IEzspProtocolHandler"))
                    .AddVariables(VariableDeclarator(Identifier("_protocolHandler"))))
                .AddModifiers(Token(SyntaxKind.PrivateKeyword)));

            // Add last status field
            classMembers.Add(FieldDeclaration(
                VariableDeclaration(ParseTypeName("ZigbeeEzspStatus"))
                    .AddVariables(VariableDeclarator(Identifier("_lastStatus"))))
                .AddModifiers(Token(SyntaxKind.PrivateKeyword)));

            // Add constructor
            classMembers.Add(GenerateConstructor());

            // Add GetLastStatus method
            classMembers.Add(GenerateGetLastStatusMethod());

            // Add frame methods
            foreach (var frameDefinition in sections.SelectMany(x => x.Frames ?? new List<FrameDefinition>()))
            {
                classMembers.Add(_generator.GenerateFrameMethod(frameDefinition));
            }

            // Create class declaration
            var classDeclaration = ClassDeclaration("EmberNcp")
                .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.PartialKeyword))
                .AddMembers(classMembers.ToArray());

            // Create namespace
            var namespaceDeclaration = NamespaceDeclaration(ParseName("ZigBeeNet.Hardware.EmberV8Plus.Ezsp"))
                .AddMembers(classDeclaration);

            // Create compilation unit
            return CompilationUnit()
                .AddUsings(usings)
                .AddMembers(namespaceDeclaration)
                .NormalizeWhitespace();
        }

        private ConstructorDeclarationSyntax GenerateConstructor()
        {
            return ConstructorDeclaration(Identifier("EmberNcp"))
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(
                    Parameter(Identifier("protocolHandler"))
                        .WithType(ParseTypeName("IEzspProtocolHandler")))
                .WithBody(Block(
                    ExpressionStatement(
                        AssignmentExpression(SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName("this._protocolHandler"),
                            IdentifierName("protocolHandler")))));
        }

        private MethodDeclarationSyntax GenerateGetLastStatusMethod()
        {
            return MethodDeclaration(ParseTypeName("ZigbeeEzspStatus"), "GetLastStatus")
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .WithBody(Block(
                    ReturnStatement(IdentifierName("_lastStatus"))))
                .WithLeadingTrivia(TriviaList(
                    Comment("/// <summary>"),
                    Comment("/// Returns the status from the last request."),
                    Comment("/// </summary>"),
                    Comment("/// <returns>The last status value</returns>")));
        }

        /// <summary>
        /// Converts the generated syntax tree to a string representation.
        /// </summary>
        public string GenerateCode(List<EzspSection> sections)
        {
            var compilationUnit = GenerateEmberNCPClass(sections);
            return compilationUnit.ToFullString();
        }
    }
}

