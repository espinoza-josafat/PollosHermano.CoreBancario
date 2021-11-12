using System;
using System.IO;

namespace PollosHermano.MicroFramework.Tools.MicroJson.Loggers
{
    public class TextWriterLogger : ILogger
    {
        public TextWriter Writer { get; set; }

        public TextWriterLogger(TextWriter writer)
        {
            Writer = writer;
        }

        public void WriteLine(string message, params object[] arguments)
        {
            try
            {
                if (arguments?.Length > 0)
                    Writer.WriteLine(message, arguments);
                else
                    Writer.WriteLine(message);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
