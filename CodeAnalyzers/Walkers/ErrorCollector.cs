using CodeAnalyzer.Error;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Collections.Generic;
using CodeAnalyzer.Services;

namespace CodeAnalyzer.Analyzers {
    public abstract class ErrorCollector: CSharpSyntaxWalker {
        protected string methodName;
        Dictionary<string, IContextService> contextServices;
        protected Dictionary<string, string> ids;
        protected HashSet<ErrorInfo> errors;
        readonly Type[] testedTypes;
        public ErrorCollector(Type[] testedTypes) {
            this.testedTypes = testedTypes;
            if(testedTypes == null)
                throw new ArgumentNullException();
        }
        protected Dictionary<string, string> IDs {
            get {
                if(ids == null)
                    ids = new Dictionary<string, string>();
                return ids;
            }
        }
        protected HashSet<ErrorInfo> Errors {
            get {
                if(errors == null)
                    errors = new HashSet<ErrorInfo>();
                return errors;
            }
        }
        protected IContextService GetContextService(string type) {
            if(contextServices == null)
                contextServices = new Dictionary<string, IContextService>();
            if(!contextServices.ContainsKey(type))
                contextServices.Add(type, ServiceContainer.Default.GetContextService(type));
            return contextServices[type];
        }
        protected bool IsProblematicMethod(string type, string methodPath) {
            var contextService = GetContextService(type);
            if(contextService != null) {
                var methods = contextService.GetProblematicMethods();
                return methods != null ? methods.Contains(methodPath) : false;
            }
            return false;
        }
        protected bool IsProblematicProperty(string type, string propertyPath) {
            var contextService = GetContextService(type);
            if(contextService != null) {
                var properties = contextService.GetProblematicProperties();
                return properties != null ? properties.Contains(propertyPath) : false;
            }
            return false;
        }
        public void ProcessMethod(MethodDeclarationSyntax node) {
            methodName = node.Identifier.ValueText;
            Visit(node);
        }
        protected bool IsRequiredType(string type) {
            return testedTypes.Any(t => type == t.Name || type == t.FullName);
        }
        protected string GetType(string type) {
            var testedType = testedTypes.FirstOrDefault(t => type == t.Name);
            if(testedType != null) return type;
            testedType = testedTypes.FirstOrDefault(t => type == t.FullName);
            return testedType != null ? testedType.Name : null;
        }
        public abstract IReadOnlyCollection<ErrorInfo> GetErrors();
        public void ClearErrors() {
            if(ids != null)
                ids.Clear();
            if(errors != null)
                errors.Clear();
        }
    }
}