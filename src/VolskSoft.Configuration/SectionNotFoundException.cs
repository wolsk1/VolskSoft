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
        public class SectionNotFoundException : Exception
        {
            /// <summary>
            /// Initialize new instance of <c>KeyNotFoundException</c> class.
            /// </summary>
            /// <param name="sectionKeyName">Name of the missing section key</param>
            public SectionNotFoundException(string sectionKeyName) 
                : base(GetMessage(sectionKeyName))
            {
            }

            /// <summary>
            /// Initialize new instance of <c>KeyNotFoundException</c> class.
            /// </summary>
            /// <param name="sectionKeyName">Name of the missing section key</param>
            /// <param name="message">A message of the error.</param>
            /// <param name="innerException">Inner exception</param>
            public SectionNotFoundException(string sectionKeyName, string message, Exception innerException) 
                : base(GetMessage(sectionKeyName, message), innerException)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="KeyNotFoundException"/> class.
            /// </summary>
            /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
            /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
            /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
            /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
            protected SectionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }

            private static string GetMessage(string keyName, string additionalMessage = null)
            {
                if (additionalMessage == null)
                {
                    additionalMessage = string.Empty;
                }

                return string.Format(CultureInfo.InvariantCulture,
                    "Section \"{0}\" is not found.{1}", 
                    keyName, 
                    additionalMessage);
            }
        }
}