namespace Foundation.Core
{
    public static class Strings
    {
        public static bool IsNullOrWhiteSpace(this string @this) => string.IsNullOrWhiteSpace(@this);
        public static bool IsNullOrEmpty(this string @this) => string.IsNullOrEmpty(@this);
    }
}