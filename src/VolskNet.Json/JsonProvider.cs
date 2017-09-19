namespace VolskNet.Json
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public class JsonProvider
    {
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

        /// <summary>
        /// Saves file to the specified path.
        /// </summary>
        /// <typeparam name="TPersistEntity">The type of the persist entity.</typeparam>
        /// <param name="pathToTheFile">The path to the file.</param>
        /// <param name="persistEntity">The persist entity.</param>
        /// <param name="serializer">The serializer.</param>
        public static void Save<TPersistEntity>(string pathToTheFile, TPersistEntity persistEntity, JsonSerializer serializer)
        {
            using (var writer = new StreamWriter(FileUtils.GetFileStream(pathToTheFile, FileExtensions.Json)))
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
        /// <param name="serializer">The serializer.</param>
        /// <returns></returns>
        public static TEntity Load<TEntity>(string pathToTheFile, JsonSerializer serializer) where TEntity : class
        {
            try
            {
                using (var reader = new JsonTextReader(new StreamReader(FileUtils.GetFileStream(pathToTheFile, FileExtensions.Json))))
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