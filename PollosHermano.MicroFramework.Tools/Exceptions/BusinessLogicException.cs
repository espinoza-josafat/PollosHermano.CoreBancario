using System;
using System.Runtime.Serialization;

namespace PollosHermano.MicroFramework.Tools.Exceptions
{
    [Serializable]
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException(string message)
            : base(message)
        {
        }

        protected BusinessLogicException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
