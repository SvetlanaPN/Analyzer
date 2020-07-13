using CodeAnalyzer.Error;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Collections.Generic;


namespace CodeAnalyzer.Analyzers {
    public class MemoryLeakCollector : ErrorCollector {
        public MemoryLeakCollector(Type[] testedTypes) : base(testedTypes) { }
        public override void VisitVariableDeclaration(VariableDeclarationSyntax node) {
            string type = GetType(node.Type.ToString());
            if(IsRequiredType(type) && !IsInUsing(node)) {
                var variableIds = node.Variables.Select(v => v.Identifier.ValueText);
                foreach(var variableId in variableIds)
                    if(!IDs.ContainsKey(variableId))
                        IDs.Add(variableId, type);
            }
            base.VisitVariableDeclaration(node);
        }
        bool IsInUsing(VariableDeclarationSyntax node) {
            return node.Parent is UsingStatementSyntax;
        }
        public override void VisitVariableDeclarator(VariableDeclaratorSyntax node) {
            string id = node.Identifier.ValueText;
            if(IDs.ContainsKey(id)) {
                var initializer = node.Initializer as EqualsValueClauseSyntax;
                if(initializer != null && !(initializer.Value is ObjectCreationExpressionSyntax))
                    IDs.Remove(id);
            }
            base.VisitVariableDeclarator(node);
        }
        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node) {
            var expression = node.Expression as IdentifierNameSyntax;
            if(expression != null) {
                string id = expression.Identifier.ValueText;
                if(IsDisposedCalled(id, node.Name.ToString()))
                    IDs.Remove(id);
            }
            base.VisitMemberAccessExpression(node);
        }
        bool IsDisposedCalled(string id, string methodName) {
            return IDs.ContainsKey(id) && methodName == "Dispose";
        }
        public override void VisitUsingStatement(UsingStatementSyntax node) {
            var expression = node.Expression as AssignmentExpressionSyntax;
            if(expression != null) {
                var left = expression.Left as IdentifierNameSyntax;
                if(left != null)
                    RemoveID(left);
            }
            else {
                var identifierExpression = node.Expression as IdentifierNameSyntax;
                if(identifierExpression != null)
                    RemoveID(identifierExpression);
            }
            base.VisitUsingStatement(node);
        }
        void RemoveID(IdentifierNameSyntax identifierExpression) {
            string id = identifierExpression.Identifier.ValueText;
            if(IDs.ContainsKey(id))
                IDs.Remove(id);
        }
        void RemoveID(IdentifierNameSyntax identifierExpression, string callerName, bool isProperty = true) {
            string id = identifierExpression.Identifier.ValueText;
            if(IDs.ContainsKey(id)) {
                bool isProblematicCaller = isProperty ? IsProblematicProperty(IDs[id], callerName) : IsProblematicMethod(IDs[id], callerName);
                if(!isProblematicCaller)
                    IDs.Remove(id);
            }
        }
        public override void VisitAssignmentExpression(AssignmentExpressionSyntax node) {
            var objectCreation = node.Right as ObjectCreationExpressionSyntax;
            if(objectCreation != null) {
                string type = GetType(objectCreation.Type.ToString());
                if(IsRequiredType(type) && IsProblematicProperty(type, node.Left.ToString()))
                    Errors.Add(new ErrorInfo(methodName, type, ErrorType.Leak));
            }
            else {
                var leftNameIdentifier = node.Left as IdentifierNameSyntax;
                if(leftNameIdentifier != null)
                    RemoveID(leftNameIdentifier);
                var nameIdentifier = node.Right as IdentifierNameSyntax;
                if(nameIdentifier != null)
                    RemoveID(nameIdentifier, node.Left.ToString());
            }
            base.VisitAssignmentExpression(node);
        }
        public override void VisitInvocationExpression(InvocationExpressionSyntax node) {
            var expression = node.Expression as MemberAccessExpressionSyntax;
            if(expression != null) {
                var argumentExpressions = node.ArgumentList.Arguments.Select(a => a.Expression).OfType<IdentifierNameSyntax>();
                foreach(var argumentExpression in argumentExpressions)
                    RemoveID(argumentExpression, expression.ToString(), false);

                var argumentTypes = node.ArgumentList.Arguments.Where(a => a.Expression is ObjectCreationExpressionSyntax).
                    Select(a => ((ObjectCreationExpressionSyntax)a.Expression).Type);
                foreach(var argumentType in argumentTypes) {
                    string type = GetType(argumentType.ToString());
                    if(IsRequiredType(type) && IsProblematicMethod(type, expression.ToString()))
                        Errors.Add(new ErrorInfo(methodName, type, ErrorType.Leak));
                }
            }
            base.VisitInvocationExpression(node);
        }
        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node) {
            if(node.ArgumentList != null) {
                foreach(var arg in node.ArgumentList.Arguments) {
                    var expression = arg.Expression as IdentifierNameSyntax;
                    if(expression != null)
                        RemoveID(expression);
                }
            }
            base.VisitObjectCreationExpression(node);
        }
        public override void VisitReturnStatement(ReturnStatementSyntax node) {
            var expression = node.Expression as IdentifierNameSyntax;
            if(expression != null)
                RemoveID(expression);
            base.VisitReturnStatement(node);
        }
        public override IReadOnlyCollection<ErrorInfo> GetErrors() {
            if(ids != null)
                Errors.UnionWith(IDs.Select(id => new ErrorInfo(methodName, id.Value, ErrorType.Leak, id.Key)));
            return Errors;
        }
    }
}