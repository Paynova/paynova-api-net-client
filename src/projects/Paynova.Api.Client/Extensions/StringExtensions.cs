using System;
using System.Text;

namespace Paynova.Api.Client.Extensions
{
    public static class StringExtensions
    {
        public static Uri ToUri(this string value)
        {
            return new Uri(value);
        }

        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            if (!char.IsUpper(s[0]))
                return s;

            var sb = new StringBuilder();
            var lastIndex = s.Length - 1;
            for (var i = 0; i < s.Length; i++)
            {
                if (i == 0 || i == lastIndex || char.IsUpper(s[i + 1]))
                {
                    sb.Append(char.ToLower(s[i]));
                    continue;
                }
                sb.Append(s.Substring(i));
                break;
            }

            return sb.ToString();
        }
    }
}