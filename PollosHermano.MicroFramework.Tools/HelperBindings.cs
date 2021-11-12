using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PollosHermano.MicroFramework.Tools
{
    public static class HelperBindings
    {
        public static string BindingParametersCatalog(string text, IDictionary<string, string> keys)
        {
            return FindReplaceKeys(text, keys, @"(\*\{.+?\}\*)", "*{", "}*");
        }

        public static string BindingKeyTemplate(string text, string key, string value)
        {
            return FindReplaceKey(text, key, value, @"(\=\#.+?\#\=)", "=#", "#=");
        }

        public static string BindingWildcard(string text, string key, string value)
        {
            return FindReplaceKey(text, key, value, @"(\=\?.+?\?\=)", "=?", "?=");
        }

        public static string FindReplaceKey(string text, string key, string value, string pattern, string startWith, string endWith)
        {
            return FindReplaceKeys(text, new Dictionary<string, string>
            {
                { key, value }
            }, pattern, startWith, endWith);
        }

        public static string FindReplaceKeys(string text, IDictionary<string, string> dictionary, string pattern, string startWith, string endWith)
        {
            if (string.IsNullOrWhiteSpace(text) || dictionary?.Count == 0)
                return text;

            var matches = Regex.Matches(text, pattern);
            if (matches?.Count > 0)
            {
                var items = matches.GroupBy(x => x.Value).Select(x => x.Key);

                if (items != null)
                {
                    foreach (var item in items)
                    {
                        var key = item.Replace(startWith, string.Empty).Replace(endWith, string.Empty);

                        if (dictionary.ContainsKey(key))
                        {
                            text = text.Replace(item, dictionary[key]);
                        }
                    }
                }
            }

            return text;
        }
    }
}
