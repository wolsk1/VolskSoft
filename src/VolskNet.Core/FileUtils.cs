namespace VolskNet
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Runtime.Serialization.Formatters.Binary;

    //TODO maybe move JSON methods to seperate project
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
        /// <param name="pathToTheFile">The path to the file.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns></returns>
        public static TEntity Load<TEntity>(string pathToTheFile, JsonSerializer serializer) where TEntity : class
        {
            try
            {
                using (var reader = new JsonTextReader(new StreamReader(GetFileStream(pathToTheFile, FileExtensions.Json))))
                {
                    return serializer.Deserialize<TEntity>(reader);
                }
            }
            catch (IOException)
            {
            }

            return default(TEntity);
        }

        /// <summary>
        /// Saves to file asynchronous.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="pathToTheFile">The path to the file.</param>
        /// <param name="fileEntity">The file entity.</param>
        /// <param name="retryTimes">The retry times.</param>
        /// <param name="retryWaitTimeSpan">The retry wait time span.</param>
        /// <returns></returns>
        public static async Task SaveToFileAsync<TEntity>(
            string pathToTheFile,
            TEntity fileEntity, 
            int retryTimes = default(int),
            TimeSpan retryWaitTimeSpan = default(TimeSpan))
        {
            var ready = false;
            var retryCount = 0;

            if (retryWaitTimeSpan == default(TimeSpan))
            {
                retryWaitTimeSpan = Defaults.RetryWaitTimeSpan;
            }
            if (retryTimes == default(int))
            {
                retryTimes = Defaults.RetryTimes;
            }

            do
            {
                try
                {
                    using (var file = File.Open(pathToTheFile, FileMode.Create))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var writer = new StreamWriter(memoryStream))
                            {
                                var serializer = JsonSerializer.Create();
                                serializer.Formatting = Formatting.Indented;
                                serializer.Serialize(writer, fileEntity);

                                await writer.FlushAsync().ConfigureAwait(false);
                                memoryStream.Seek(0, SeekOrigin.Begin);
                                await memoryStream.CopyToAsync(file).ConfigureAwait(false);
                                ready = true;
                            }
                        }

                        await file.FlushAsync().ConfigureAwait(false);
                    }
                }
                catch (Exception)
                {
                    retryCount++;
                    Thread.Sleep(retryWaitTimeSpan);
                }



            } while (!ready || retryCount.Equals(retryTimes));

        }
    }
}