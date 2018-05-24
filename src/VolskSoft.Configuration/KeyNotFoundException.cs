namespace VolskSoft.Bibliotheca.Configuration
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <inheritdoc />
    /// <summary>
    /// Exception that is thrown when config settings provider does not contain requested key
    /// </summary>
    [Serializable]
    public class KeyNotFoundException : Exception
    {
        /// <summary>
        /// Initialize new instance of <c>KeyNotFoundException</c> class.
        /// </summary>
        /// <param name="keyName">Name of the missing key</param>
        public KeyNotFoundException(string keyName) 
            : base(GetMessage(keyName))
        {
        }

        /// <summary>
        /// Initialize new instance of <c>KeyNotFoundException</c> class.
        /// </summary>
        /// <param name="keyName">Name of the missing key</param>
        /// <param name="message">A message of the error.</param>
        /// <param name="innerException">Inner exception</param>
        public KeyNotFoundException(string keyName, string message, Exception innerException) 
            : base(GetMessage(keyName, message), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyNotFoundException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        protected KeyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        private static string GetMessage(string keyName, string additionalMessage = null)
        {
            if (additionalMessage == null)
            {
                additionalMessage = string.Empty;
            }

            return string.Format(CultureInfo.InvariantCulture,
                "Key named \"{0}\" does not exist in configuration.{1}", 
                keyName, 
                additionalMessage);
        }
    }
}