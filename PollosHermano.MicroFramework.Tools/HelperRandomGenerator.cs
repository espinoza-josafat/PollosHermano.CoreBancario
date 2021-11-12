using System;
using System.Security.Cryptography;
using System.Text;

namespace PollosHermano.MicroFramework.Tools
{
    public static class HelperRandomGenerator
    {
        // Generate a random number between two numbers    
        public static int RandomNumber(int min, int max)
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] data = new byte[4];
            rng.GetBytes(data);
            int seed = BitConverter.ToInt32(data, 0);
            return new Random(seed).Next(min, max);
        }

        // Generate a random string with a given size    
        public static string RandomString(int size, bool lowerCase)
        {
            var stringBuilder = new StringBuilder();
            var random = new Random();
            char ch;
            var rng = new RNGCryptoServiceProvider();            

            for (int i = 0; i < size; i++)
            {
                var data = new byte[4];
                rng.GetBytes(data);
                var randUint = BitConverter.ToUInt32(data, 0);
                var randomDouble = randUint / (uint.MaxValue + 1.0);

                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomDouble + 65)));
                //ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                stringBuilder.Append(ch);
            }

            if (lowerCase)
                return stringBuilder.ToString().ToLower();

            return stringBuilder.ToString();
        }

        // Generate a random password    
        public static string RandomPassword()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(RandomString(4, true));
            stringBuilder.Append(RandomNumber(1000, 9999));
            stringBuilder.Append(RandomString(2, false));
            return stringBuilder.ToString();
        }
    }
}
