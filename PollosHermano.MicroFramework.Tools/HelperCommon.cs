using System;
using System.Text.RegularExpressions;

namespace PollosHermano.MicroFramework.Tools
{
    public static class HelperCommon 
    {
        /// <summary>
        /// Checks whether the given value represents null.
        /// The DBNull.Value is treated as null.
        /// This comes handy if conversion is applied to values coming from or sending to a database via ADO.Net.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ValueRepresentsNull(object value)
        {
            return value == null || value == DBNull.Value;
        }

        /// <summary>
        /// Returns the default value of the given type.
        /// ValueTypes always have a parameterless constructor.
        /// The default value of other types is always null.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetDefaultValueOfType(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        public static bool IsGenericNullable(Type type)
        {
            return type.IsGenericType &&
                   type.GetGenericTypeDefinition() == typeof(Nullable<>).GetGenericTypeDefinition();
        }

        public static Type GetUnderlyingType(Type type)
        {
            return Nullable.GetUnderlyingType(type);
        }

        public static bool IsWhiteSpace(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsValidUrl(string url)
        {
            var pattern = @"^(?:http(s)?:\/\/)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";
            return new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase).IsMatch(url);
        }

        public static bool IsDecimalFormat(string input)
        {
            return decimal.TryParse(input, out decimal dummy);
        }

        public static bool IsIntegerFormat(string input)
        {
            return long.TryParse(input, out long dummy);
        }

        public static bool OnlyNumbers(string input)
        {
            return new Regex(@"^\d$").IsMatch(input);
        }

        public static bool IsEmailFormat(string input)
        {
            return new Regex("^(([^<>()[\\]\\.,;:\\s@\"]+ (\\.[^<> ()[\\]\\.,;:\\s@\"]+)*)|(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$").IsMatch(input);
        }
    }
}
