using NuciDAL.Repositories;

using UniversalNameGenerator.DataAccess.DataObjects;
using UniversalNameGenerator.DataAccess.Repositories.Interfaces;

namespace UniversalNameGenerator.DataAccess.Repositories
{
    /// <summary>
    /// GenerationSchema repository implementation.
    /// </summary>
    public class GenerationSchemaRepository : XmlRepository<GenerationSchemaEntity>, IGenerationSchemaRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerationSchemaRepository"/> class.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public GenerationSchemaRepository(string fileName)
            : base(fileName)
        {
        }

        /// <summary>
        /// Updates the specified generation schema.
        /// </summary>
        /// <param name="schemaEntity">Generation schema.</param>
        public override void Update(GenerationSchemaEntity schemaEntity)
        {
            LoadEntitiesIfNeeded();

            GenerationSchemaEntity schemaEntityToUpdate = Get(schemaEntity.Id);

            if (schemaEntityToUpdate == null)
            {
                throw new EntityNotFoundException(schemaEntity.Id, nameof(GenerationSchemaEntity));
            }

            schemaEntityToUpdate.Name = schemaEntity.Name;
            schemaEntityToUpdate.FilterlistPath = schemaEntity.FilterlistPath;
            schemaEntityToUpdate.Schema = schemaEntity.Schema;
            
            XmlFile.SaveEntities(Entities.Values);
        }
    }
}
