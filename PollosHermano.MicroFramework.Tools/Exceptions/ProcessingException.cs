using System;
using System.Runtime.Serialization;

namespace PollosHermano.MicroFramework.Tools.Exceptions
{
    [Serializable]
    public class ProcessingException : Exception
    {
        public ProcessingException(string message)
            : base(message)
        {
        }

        protected ProcessingException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
