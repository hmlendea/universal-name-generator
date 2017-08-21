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
        /// Adds the specified biome.
        /// </summary>
        /// <param name="biomeEntity">GenerationSchema.</param>
        void Add(GenerationSchemaEntity biomeEntity);

        /// <summary>
        /// Get the biome with the specified identifier.
        /// </summary>
        /// <returns>The biome.</returns>
        /// <param name="id">Identifier.</param>
        GenerationSchemaEntity Get(string id);

        /// <summary>
        /// Gets all the biomes.
        /// </summary>
        /// <returns>The biomes</returns>
        IEnumerable<GenerationSchemaEntity> GetAll();

        /// <summary>
        /// Updates the specified biome.
        /// </summary>
        /// <param name="biomeEntity">GenerationSchema.</param>
        void Update(GenerationSchemaEntity biomeEntity);

        /// <summary>
        /// Removes the biome with the specified identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        void Remove(string id);
    }
}
