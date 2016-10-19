using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestHelper;
using RoslynPrefixNamespace;

namespace RoslynPrefixNamespace.Test
{
    [TestClass]
    public class UnitTest : CodeFixVerifier
    {
        [TestMethod]
        public void AnEmptyFileHasNoDiagnostics()
        {
            var test = @"";

            VerifyCSharpDiagnostic(test);
        }

        //Diagnostic and CodeFix both triggered and checked for
        [TestMethod]
        public void TestPrefixNamespace()
        {
            var test = @"namespace Playground
    {
    }";
            var expected = new DiagnosticResult
            {
                Id = RoslynPrefixNamespaceAnalyzer.DiagnosticId,
                Message = String.Format("Namespace '{0}' is not prefixed", "Playground"),
                Severity = DiagnosticSeverity.Warning,
                Locations =
                    new[] {
                            new DiagnosticResultLocation("Test0.cs", 0, 11)
                        }
            };

            VerifyCSharpDiagnostic(test, expected);

            var fixtest = @"namespace ApressPlayground
    {
    }";
            VerifyCSharpFix(test, fixtest);
        }

        protected override CodeFixProvider GetCSharpCodeFixProvider()
        {
            return new RoslynPrefixNamespaceCodeFixProvider();
        }

        protected override DiagnosticAnalyzer GetCSharpDiagnosticAnalyzer()
        {
            return new RoslynPrefixNamespaceAnalyzer();
        }
    }
}