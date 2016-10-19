using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RoslynPrefixNamespace
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class RoslynPrefixNamespaceAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "RoslynPrefixNamespace";
        
        internal static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        internal static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        internal static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        internal const string Category = "Naming";

        internal static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Namespace);
        }

        private static void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            var namespaceSymbol = (INamespaceSymbol)context.Symbol;
            if (!namespaceSymbol.ToDisplayString().StartsWith("Apress"))
            {
                var diagnostic = Diagnostic.Create(Rule, namespaceSymbol.Locations[0], namespaceSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }
    }
}
