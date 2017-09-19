namespace VolskNet
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        /// <summary>
        /// Removes the white spaces.
        /// </summary>
        /// <param name="originalString">The original string.</param>
        /// <returns></returns>
        public static string RemoveWhiteSpaces(this string originalString)
        {
            return originalString?.Replace(" ", "");
        }

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

        /// <summary>
        /// Transforms string from the camel case to human readable text.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string FromPascalCase(this string sourceString)
        {
            if (string.IsNullOrEmpty(sourceString))
            {
                throw new ArgumentNullException(nameof(sourceString));
            }

            var regex = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            return regex.Replace(sourceString, " ");
        }

        /// <summary>
        /// To the pascal case.
        /// </summary>
        /// <param name="sourceString">The source string.</param>
        /// <returns></returns>
        public static string ToPascalCase(this string sourceString)
        {
            if (sourceString == null)
            {
                return null;
            }

            if (sourceString.Length < 2)
            {
                return sourceString.ToUpper();
            }

            var words = sourceString.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            return words.Aggregate(string.Empty, (current, word) =>
            current + word.Substring(0, 1).ToUpper() + word.Substring(1));
        }
    }
}