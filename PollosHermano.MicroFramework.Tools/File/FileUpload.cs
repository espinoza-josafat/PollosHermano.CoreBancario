using System;
using System.IO;
using System.Net;
using System.Text;

namespace PollosHermano.MicroFramework.Tools.File
{
    public static class FileUpload
    {
        public static string GetFileBase64FromUrl(string url)
        {
            var stringBuilder = new StringBuilder();
            var byteArray = GetFileFromUrl(url);

            if (byteArray?.Length > 0)
                stringBuilder.Append(Convert.ToBase64String(byteArray, 0, byteArray.Length));

            return stringBuilder.ToString();
        }

        public static byte[] GetFileFromUrl(string url)
        {
            byte[] result;

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                using (var stream = httpWebResponse.GetResponseStream())
                using (var binaryReader = new BinaryReader(stream))
                {
                    var length = (int)httpWebResponse.ContentLength;
                    result = binaryReader.ReadBytes(length);
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }
        
        public static string GetFile(string url)
        {
            string result = null;

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                using (var stream = httpWebResponse.GetResponseStream())
                using (var streamReader = new StreamReader(stream))
                    result = streamReader.ReadToEnd();
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }
    }
}
