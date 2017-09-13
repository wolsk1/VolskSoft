namespace VolskNet
{
    using Newtonsoft.Json;
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

        /// <summary>
        /// Saves file to the specified path.
        /// </summary>
        /// <typeparam name="TPersistEntity">The type of the persist entity.</typeparam>
        /// <param name="pathToTheFile">The path to the file.</param>
        /// <param name="persistEntity">The persist entity.</param>
        public static void Save<TPersistEntity>(string pathToTheFile, TPersistEntity persistEntity)
        {
            using (var stream = GetFileStream(pathToTheFile, FileExtensions.Bin))
            {
                new BinaryFormatter().Serialize(stream, persistEntity);
            }
        }

        /// <summary>
        /// Saves file to the specified path.
        /// </summary>
        /// <typeparam name="TPersistEntity">The type of the persist entity.</typeparam>
        /// <param name="pathToTheFile">The path to the file.</param>
        /// <param name="persistEntity">The persist entity.</param>
        /// <param name="serializer">The serializer.</param>
        public static void Save<TPersistEntity>(string pathToTheFile, TPersistEntity persistEntity, JsonSerializer serializer)
        {
            using (var writer = new StreamWriter(GetFileStream(pathToTheFile, FileExtensions.Json)))
            {
                serializer.Formatting = Formatting.Indented;
                serializer.Serialize(writer, persistEntity);
            }
        }

        /// <summary>
        /// Loads file from the specified path.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="pathToTheFile">The path to the file.</param>
        /// <returns></returns>
        public static TEntity Load<TEntity>(string pathToTheFile) where TEntity : class
        {
            try
            {
                using (var stream = GetFileStream(pathToTheFile, FileExtensions.Bin))
                {
                    return (TEntity)new BinaryFormatter().Deserialize(stream);
                }
            }
            catch (IOException)
            {
            }

            return default(TEntity);
        }

        /// <summary>
        /// Loads file from the specified path.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="saveName">Name of the save.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns></returns>
        public static TEntity Load<TEntity>(string saveName, JsonSerializer serializer) where TEntity : class
        {
            try
            {
                using (var reader = new JsonTextReader(new StreamReader(GetFileStream(saveName, FileExtensions.Json))))
                {
                    return serializer.Deserialize<TEntity>(reader);
                }
            }
            catch (IOException)
            {
            }

            return default(TEntity);
        }
    }
}