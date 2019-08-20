using System.Text.RegularExpressions;

namespace Foundation.Core
{
    public static class Strings
    {
        public static bool IsNullOrWhiteSpace(this string @this) => string.IsNullOrWhiteSpace(@this);
        public static bool IsNullOrEmpty(this string @this) => string.IsNullOrEmpty(@this);
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
    }
}