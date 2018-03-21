namespace VolskSoft.Bibliotheca
{
    using System.IO;
    using System.Text;
    using System.Collections.Generic;

    public class FileProvider
    {
        /// <summary>
        /// Writes the list to file.
        /// </summary>
        /// <param name="pathToFile">The path to file.</param>
        /// <param name="extension">The extension.</param>
        /// <param name="stringList">The string list.</param>
        /// <param name="encoding">The encoding.</param>
        public static void WriteListToFile(
            string pathToFile,
            FileExtensions extension,
            IEnumerable<string> stringList,
            Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            using (TextWriter writer = new StreamWriter(FileUtils.GetFileStream(pathToFile, FileExtensions.Sql, FileMode.Create), encoding))
            {
                foreach (var stringItem in stringList)
                {
                    writer.WriteLine(stringItem);
                }
            }
        }
    }
}