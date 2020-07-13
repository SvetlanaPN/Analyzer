using System.Collections.Generic;

namespace CodeAnalyzer.Error {
    public enum ErrorType {
        None,
        Leak
    }
    public class ErrorInfo {
        public ErrorInfo(string ownerMethod, string variableType, ErrorType errorType, string variableName = null) {
            OwnerMethod = ownerMethod;
            VariableType = variableType;
            ErrorType = errorType;
            VariableName = variableName;
        }
        public ErrorType ErrorType { get; set; }
        public string VariableType { get; set; }
        public string VariableName { get; set; }
        public string OwnerMethod { get; set; }
        public override string ToString() {
            return $"ErrorType = {ErrorType}, VariableType = {VariableType}, VariableName = {VariableName}, OwnerMethod = {OwnerMethod}";
        }
    }
}