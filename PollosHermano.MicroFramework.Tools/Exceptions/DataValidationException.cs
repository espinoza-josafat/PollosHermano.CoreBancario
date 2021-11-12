using System;
using System.Runtime.Serialization;

namespace PollosHermano.MicroFramework.Tools.Exceptions
{
    [Serializable]
    public class DataValidationException : Exception
    {
        public DataValidationException(string message)
            : base(message)
        {
        }

        protected DataValidationException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
