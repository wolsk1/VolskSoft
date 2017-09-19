namespace VolskNet
{
    using System;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        /// <summary>
        /// Determines whether [contains] [the specified to check].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="toCheck">To check.</param>
        /// <param name="comp">The comp.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified to check]; otherwise, <c>false</c>.
        /// </returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// Determines whether [contains word exact] [the specified to check].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="toCheck">To check.</param>
        /// <param name="options">The options.</param>
        /// <returns>
        ///   <c>true</c> if [contains word exact] [the specified to check]; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsWordExact(this string source, string toCheck, RegexOptions options)
        {
            return Regex.IsMatch(source, string.Format(@"(?i)\b{0}\b", toCheck), options);
        }
    }
}