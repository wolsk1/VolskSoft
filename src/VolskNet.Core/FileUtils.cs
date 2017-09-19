namespace VolskNet
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
   
    public class FileUtils
    {
        /// <summary>
        /// Gets the file stream.
        /// </summary>
        /// <param name="pathToTheFile">The path to the file.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <param name="fileMode">The file mode. (default OpenCreate)</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">pathToTheFile</exception>
        public static FileStream GetFileStream(string pathToTheFile, FileExtensions fileExtension, FileMode fileMode = FileMode.OpenOrCreate)
        {
            if (string.IsNullOrEmpty(pathToTheFile))
            {
                throw new ArgumentNullException("pathToTheFile");
            }

            return File.Open(
                string.Format("{0}.{1}", pathToTheFile, fileExtension.ToString().ToLower()),
                fileMode,
                FileAccess.ReadWrite);
        }
    }
}