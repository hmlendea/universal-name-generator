using System;
using System.Collections.Generic;
using System.Linq;

using UniversalNameGenerator.DataAccess.DataObjects;
using UniversalNameGenerator.Models;
using UniversalNameGenerator.Models.Enumerations;

namespace UniversalNameGenerator.Service.Mapping
{
    /// <summary>
    /// Generation schema mapping extensions for converting between entities and domain models.
    /// </summary>
    static class GenerationSchemaMappingExtensions
    {
        /// <summary>
        /// Converts the entity into a domain model.
        /// </summary>
        /// <returns>The domain model.</returns>
        /// <param name="generationSchemaEntity">GenerationSchema entity.</param>
        internal static GenerationSchema ToDomainModel(this GenerationSchemaEntity generationSchemaEntity)
        {
            GenerationSchema generationSchema = new GenerationSchema
            {
                Id = generationSchemaEntity.Id,
                Name = generationSchemaEntity.Name,
                Category = generationSchemaEntity.Category,
                Schema = generationSchemaEntity.Schema,
                FilterlistPath = generationSchemaEntity.FilterlistPath,
                WordCasing = (WordCasing)Enum.Parse(typeof(WordCasing), generationSchemaEntity.WordCasing),
            };

            return generationSchema;
        }

        /// <summary>
        /// Converts the domain model into an entity.
        /// </summary>
        /// <returns>The entity.</returns>
        /// <param name="generationSchema">GenerationSchema.</param>
        internal static GenerationSchemaEntity ToEntity(this GenerationSchema generationSchema)
        {
            GenerationSchemaEntity generationSchemaEntity = new GenerationSchemaEntity
            {
                Id = generationSchema.Id,
                Name = generationSchema.Name,
                Category = generationSchema.Category,
                Schema = generationSchema.Schema,
                FilterlistPath = generationSchema.FilterlistPath,
                WordCasing = generationSchema.WordCasing.ToString()
            };

            return generationSchemaEntity;
        }

        /// <summary>
        /// Converts the entities into domain models.
        /// </summary>
        /// <returns>The domain models.</returns>
        /// <param name="generationSchemaEntities">GenerationSchema entities.</param>
        internal static IEnumerable<GenerationSchema> ToDomainModels(this IEnumerable<GenerationSchemaEntity> generationSchemaEntities)
        {
            IEnumerable<GenerationSchema> generationSchemas = generationSchemaEntities.Select(generationSchemaEntity => generationSchemaEntity.ToDomainModel());

            return generationSchemas;
        }

        /// <summary>
        /// Converts the domain models into entities.
        /// </summary>
        /// <returns>The entities.</returns>
        /// <param name="generationSchemas">GenerationSchemas.</param>
        internal static IEnumerable<GenerationSchemaEntity> ToEntities(this IEnumerable<GenerationSchema> generationSchemas)
        {
            IEnumerable<GenerationSchemaEntity> generationSchemaEntities = generationSchemas.Select(generationSchema => generationSchema.ToEntity());

            return generationSchemaEntities;
        }
    }
}
