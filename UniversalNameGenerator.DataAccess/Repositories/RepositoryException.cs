using System;
using System.Runtime.Serialization;

namespace UniversalNameGenerator.DataAccess.Repositories
{
    /// <summary>
    /// Repository exception.
    /// </summary>
    [Serializable]
    public class RepositoryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalNameGenerator.Repositories.RepositoryException"/> class.
        /// </summary>
        public RepositoryException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalNameGenerator.Repositories.RepositoryException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public RepositoryException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalNameGenerator.Repositories.RepositoryException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public RepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalNameGenerator.Repositories.RepositoryException"/> class.
        /// </summary>
        /// <param name="info">Info.</param>
        /// <param name="context">Context.</param>
        protected RepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
