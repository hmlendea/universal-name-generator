using System.Collections.Generic;

using UniversalNameGenerator.DataAccess.DataObjects;

namespace UniversalNameGenerator.DataAccess.Repositories.Interfaces
{
    /// <summary>
    /// GenerationSchema repository interface.
    /// </summary>
    public interface IGenerationSchemaRepository
    {
        /// <summary>
        /// Adds the specified generationSchema.
        /// </summary>
        /// <param name="generationSchemaEntity">GenerationSchema.</param>
        void Add(GenerationSchemaEntity generationSchemaEntity);

        /// <summary>
        /// Get the generationSchema with the specified identifier.
        /// </summary>
        /// <returns>The generationSchema.</returns>
        /// <param name="id">Identifier.</param>
        GenerationSchemaEntity Get(string id);

        /// <summary>
        /// Gets all the generationSchemas.
        /// </summary>
        /// <returns>The generationSchemas</returns>
        IEnumerable<GenerationSchemaEntity> GetAll();

        /// <summary>
        /// Updates the specified generationSchema.
        /// </summary>
        /// <param name="generationSchemaEntity">GenerationSchema.</param>
        void Update(GenerationSchemaEntity generationSchemaEntity);

        /// <summary>
        /// Removes the generationSchema with the specified identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        void Remove(string id);
    }
}
