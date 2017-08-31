namespace VolskNet.Core
{
    using System;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static bool ContainsWordExact(this string source, string toCheck, RegexOptions options)
        {
            return Regex.IsMatch(source, string.Format(@"(?i)\b{0}\b", toCheck), options);
        }
    }
}