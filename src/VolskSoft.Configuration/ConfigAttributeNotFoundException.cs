namespace VolskSoft.Bibliotheca.Configuration
{
    using System;
    using System.Runtime.Serialization;

    /// <inheritdoc />
    /// <summary>
    /// Exception that is thrown when config does not contain requested configuration attribute
    /// </summary>
    [Serializable]
    public class ConfigAttributeNotFoundException : Exception
    {
        private const string DefeaultMessage = "Missing configuration attribute ";

        /// <inheritdoc />
        /// <summary>
        /// Initialize new instance of <c>ConfigAttributeNotFoundException</c> class.
        /// </summary>
        public ConfigAttributeNotFoundException(string attributeKey)
            : base(string.Format("{0}\"{1}\"", DefeaultMessage, attributeKey))
        {

        }

        /// <summary>
        /// Initialize new instance of <c>ConfigAttributeNotFoundException</c> class.
        /// </summary>
        /// <param name="message">A message of the error.</param>
        /// <param name="innerException">Inner exception</param>
        public ConfigAttributeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyNotFoundException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
        protected ConfigAttributeNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}