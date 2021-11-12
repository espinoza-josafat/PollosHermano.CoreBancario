using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;

namespace PollosHermano.MicroFramework.Tools
{
    public static class ExtensionMethods
    {
        public static List<Enum> GetFlags(this Enum e)
        {
            return Enum.GetValues(e.GetType()).Cast<Enum>().Where(e.HasFlag).ToList();
        }

        public static string ToPlain(this SecureString source)
        {
            var returnValue = IntPtr.Zero;

            try
            {
                returnValue = Marshal.SecureStringToGlobalAllocUnicode(source);

                return Marshal.PtrToStringUni(returnValue);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(returnValue);
            }
        }
    }
}
