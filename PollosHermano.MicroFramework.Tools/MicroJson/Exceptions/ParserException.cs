using System;
using System.Runtime.Serialization;

namespace PollosHermano.MicroFramework.Tools.MicroJson.Exceptions
{
    [Serializable]
    public class ParserException : Exception
    {
        public int Line { get; set; }
        public int Column { get; set; }

        public ParserException(string message, int line, int column)
            : base(message)
        {
            Line = line;
            Column = column;
        }

        protected ParserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
