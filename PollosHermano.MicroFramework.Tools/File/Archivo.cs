using System;

namespace PollosHermano.MicroFramework.Tools.File
{
    public static class Archivo
    {
        public static string GetStringOfFile(string pathFile)
        {
            try
            {
                return System.IO.File.ReadAllText(pathFile);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return string.Empty;
        }
    }
}
