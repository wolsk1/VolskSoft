namespace VolskNet
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public class BinaryProvider
    {
        /// <summary>
        /// Saves file to the specified path.
        /// </summary>
        /// <typeparam name="TPersistEntity">The type of the persist entity.</typeparam>
        /// <param name="pathToTheFile">The path to the file.</param>
        /// <param name="persistEntity">The persist entity.</param>
        public static void Save<TPersistEntity>(string pathToTheFile, TPersistEntity persistEntity)
        {
            using (var stream = FileUtils.GetFileStream(pathToTheFile, FileExtensions.Bin))
            {
                new BinaryFormatter().Serialize(stream, persistEntity);
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
                using (var stream = FileUtils.GetFileStream(pathToTheFile, FileExtensions.Bin))
                {
                    return (TEntity)new BinaryFormatter().Deserialize(stream);
                }
            }
            catch (IOException)
            {
            }

            return default(TEntity);
        }
    }
}