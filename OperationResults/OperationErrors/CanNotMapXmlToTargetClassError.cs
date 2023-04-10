using System;

namespace OperationResults.OperationErrors
{
    public class CanNotMapXmlToTargetClassError : OperationError
    {
        public string TargetClassName { get; set; }

        private CanNotMapXmlToTargetClassError() { }
        public CanNotMapXmlToTargetClassError(Exception ex, string targetClassName) : base(ex, $"Can't map data to {targetClassName} object")
            => TargetClassName = targetClassName;
    }
}
