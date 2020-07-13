using CodeAnalyzer.Error;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeAnalyzer.Analyzers {
    public class Analyzer {
        HashSet<ErrorInfo> errors;
        readonly ErrorCollector[] errorCollectors;
        public Analyzer(ErrorCollector[] errorCollectors) {
            this.errorCollectors = errorCollectors;
            if(errorCollectors == null)
                throw new ArgumentNullException();
        }
        public HashSet<ErrorInfo> Errors {
            get {
                if(errors == null)
                    errors = new HashSet<ErrorInfo>();
                return errors;
            }
        }
        public void ClearErrors() {
            if(errors != null) {
                errors.Clear();
                foreach(ErrorCollector errorCollector in errorCollectors)
                    errorCollector.ClearErrors();
            }
        }
        public void Analyze(string code) {
            var root = GetRoot(code);
            var methods = GetMethods(root, code);
            foreach(var method in methods) {
                foreach(ErrorCollector errorCollector in errorCollectors) {
                    errorCollector.ProcessMethod(method);
                    Errors.UnionWith(errorCollector.GetErrors());
                    errorCollector.ClearErrors();
                }
            }
        }
        CompilationUnitSyntax GetRoot(string code) {
            var tree = CSharpSyntaxTree.ParseText(code);
            return tree.GetCompilationUnitRoot();
        }
        IEnumerable<MethodDeclarationSyntax> GetMethods(CompilationUnitSyntax root, string code) {
            var methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
            if(methods.Count() == 0) {
                code = CreateMethod(code);
                root = GetRoot(code);
                methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
            }
            return methods;
        }
        string CreateMethod(string code) {
            return $"void Test() {{{code}}}";
        }
    }
}