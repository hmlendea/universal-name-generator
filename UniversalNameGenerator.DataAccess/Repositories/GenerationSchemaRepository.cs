using System.Collections.Generic;
using System.Linq;

using UniversalNameGenerator.DataAccess.Exceptions;
using UniversalNameGenerator.DataAccess.Repositories.Interfaces;
using UniversalNameGenerator.Models;

namespace UniversalNameGenerator.DataAccess.Repositories
{
    /// <summary>
    /// GenerationSchema repository implementation.
    /// </summary>
    public class GenerationSchemaRepository : IGenerationSchemaRepository
    {
        readonly XmlDatabase<GenerationSchema> xmlDatabase;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerationSchemaRepository"/> class.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public GenerationSchemaRepository(string fileName)
        {
            xmlDatabase = new XmlDatabase<GenerationSchema>(fileName);
        }

        /// <summary>
        /// Adds the specified generation schema.
        /// </summary>
        /// <param name="generation schemaEntity">GenerationSchema.</param>
        public void Add(GenerationSchema schemaEntity)
        {
            List<GenerationSchema> schemaEntities = xmlDatabase.LoadEntities().ToList();
            schemaEntities.Add(schemaEntity);

            try
            {
                xmlDatabase.SaveEntities(schemaEntities);
            }
            catch
            {
                throw new DuplicateEntityException(schemaEntity.Id, nameof(GenerationSchema).Replace("Entity", ""));
            }
        }

        /// <summary>
        /// Get the generation schema with the specified identifier.
        /// </summary>
        /// <returns>The generation schema.</returns>
        /// <param name="id">Identifier.</param>
        public GenerationSchema Get(string id)
        {
            List<GenerationSchema> schemaEntities = xmlDatabase.LoadEntities().ToList();
            GenerationSchema schemaEntity = schemaEntities.FirstOrDefault(x => x.Id == id);

            if (schemaEntity == null)
            {
                throw new EntityNotFoundException(id, nameof(GenerationSchema).Replace("Entity", ""));
            }

            return schemaEntity;
        }

        /// <summary>
        /// Gets all the generation schemas.
        /// </summary>
        /// <returns>The generation schemas</returns>
        public IEnumerable<GenerationSchema> GetAll()
        {
            List<GenerationSchema> schemaEntities = xmlDatabase.LoadEntities().ToList();

            return schemaEntities;
        }

        /// <summary>
        /// Updates the specified generation schema.
        /// </summary>
        /// <param name="schemaEntity">Generation schema.</param>
        public void Update(GenerationSchema schemaEntity)
        {
            List<GenerationSchema> schemaEntities = xmlDatabase.LoadEntities().ToList();
            GenerationSchema schemaEntityToUpdate = schemaEntities.FirstOrDefault(x => x.Id == schemaEntity.Id);

            if (schemaEntityToUpdate == null)
            {
                throw new EntityNotFoundException(schemaEntity.Id, nameof(GenerationSchema).Replace("Entity", ""));
            }

            schemaEntityToUpdate.Name = schemaEntity.Name;
            schemaEntityToUpdate.Filterlist = schemaEntity.Filterlist;
            schemaEntityToUpdate.Schema = schemaEntity.Schema;

            xmlDatabase.SaveEntities(schemaEntities);
        }

        /// <summary>
        /// Removes the generation schema with the specified identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public void Remove(string id)
        {
            List<GenerationSchema> schemaEntities = xmlDatabase.LoadEntities().ToList();
            schemaEntities.RemoveAll(x => x.Id == id);

            try
            {
                xmlDatabase.SaveEntities(schemaEntities);
            }
            catch
            {
                throw new DuplicateEntityException(id, nameof(GenerationSchema).Replace("Entity", ""));
            }
        }
    }
}
