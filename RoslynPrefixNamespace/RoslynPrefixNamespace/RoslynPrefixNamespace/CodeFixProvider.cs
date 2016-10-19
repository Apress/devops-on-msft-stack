using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

namespace RoslynPrefixNamespace
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(RoslynPrefixNamespaceCodeFixProvider)), Shared]
    public class RoslynPrefixNamespaceCodeFixProvider : CodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(RoslynPrefixNamespaceAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            var diagnostic = context.Diagnostics.First();
            var diagnosticSpan = diagnostic.Location.SourceSpan;

            var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<NamespaceDeclarationSyntax>().First();

            context.RegisterCodeFix(
                CodeAction.Create("Prefix namespace", c => PrefixNamespaceAsync(context.Document, declaration, c)),
                diagnostic);
        }

        private async Task<Solution> PrefixNamespaceAsync(Document document, NamespaceDeclarationSyntax typeDecl, CancellationToken cancellationToken)
        {
            var identifierToken = typeDecl.Name;

            var semanticModel = await document.GetSemanticModelAsync(cancellationToken);
            var typeSymbol = semanticModel.GetDeclaredSymbol(typeDecl, cancellationToken);

            var originalSolution = document.Project.Solution;
            var optionSet = originalSolution.Workspace.Options;
            var newSolution = await Renamer.RenameSymbolAsync(
                document.Project.Solution,
                typeSymbol,
                "Apress" + identifierToken.ToString(),
                optionSet,
                cancellationToken).ConfigureAwait(false);

            return newSolution;
        }
    }
}