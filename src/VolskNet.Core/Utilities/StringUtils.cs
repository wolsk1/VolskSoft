namespace VolskSoft.Bibliotheca
{
    using System;

    public class StringUtils
    {
        /// <summary>
        /// This method is used to split <see cref="string"/> into seperated elements.
        /// </summary>
        /// <param name="splitText">String to split to.</param>
        /// <param name="separator">A seperator.</param>
        /// <returns>An array of delimited elements.</returns>
        /// <remarks>The maximum number of delimited elements is 1000.</remarks>
        public static string[] Split(string splitText, char separator)
        {
            return Split(splitText, separator, 1000);
        }


        /// <summary>
        /// This method is used to split <see cref="string"/> into seperated elements.
        /// </summary>
        /// <param name="splitText">String to split to.</param>
        /// <param name="separator">A seperator.</param>
        /// <param name="max">The maximum number of delimited elements to return.</param>
        /// <returns>An array of delimited elements.</returns>
        public static string[] Split(string splitText, char separator, int max)
        {
            return splitText.Split(new char[]
            {
                separator
            }, max);
        }

        /// <summary>
        /// Convert string[] to int[].
        /// </summary>
        /// <param name="array">An array of strings.</param>
        /// <returns>An array of int.</returns>
        /// <remarks>If any of the elements in string array will not be int,
        /// <see cref="System.FormatException"/> is thrown.</remarks>
        public static int[] ConvertToIntArray(string[] array)
        {
            var retVal = new int[array.Length];
            for (var i = 0; i < array.Length; i++)
            {
                retVal[i] = Convert.ToInt32(array[i]);
            }
            return retVal;
        }
    }
}