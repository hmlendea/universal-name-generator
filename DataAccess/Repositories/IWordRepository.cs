using System.Collections.Generic;

using UniversalNameGenerator.DataAccess.DataObjects;

namespace UniversalNameGenerator.DataAccess.Repositories
{
    /// <summary>
    /// Word repository interface.
    /// </summary>
    public interface IWordRepository
    {
        /// <summary>
        /// Gets all the words.
        /// </summary>
        /// <returns>The words</returns>
        IEnumerable<WordEntity> GetAll();
    }
}
