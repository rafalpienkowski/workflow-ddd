using System;

namespace Workflow.Domain.Framework
{
    [System.Serializable]
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
        public BusinessException(string message, System.Exception inner) : base(message, inner) { }
        protected BusinessException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}