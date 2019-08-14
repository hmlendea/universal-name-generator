using System.Collections.Generic;
using System.Linq;

using UniversalNameGenerator.DataAccess.DataObjects;
using UniversalNameGenerator.Models;

namespace UniversalNameGenerator.BusinessLogic.Mapping
{
    /// <summary>
    /// Generation schema mapping extensions for converting between entities and domain models.
    /// </summary>
    static class WordMappingExtensions
    {
        /// <summary>
        /// Converts the entity into a domain model.
        /// </summary>
        /// <returns>The domain model.</returns>
        /// <param name="wordEntity">Word entity.</param>
        internal static Word ToDomainModel(this WordEntity wordEntity)
        {
            Word word = new Word
            {
                Id = wordEntity.Id,
                Values = wordEntity.Values
            };

            return word;
        }

        /// <summary>
        /// Converts the domain model into an entity.
        /// </summary>
        /// <returns>The entity.</returns>
        /// <param name="word">Word.</param>
        internal static WordEntity ToEntity(this Word word)
        {
            WordEntity wordEntity = new WordEntity
            {
                Id = word.Id,
                Values = word.Values.ToList()
            };

            return wordEntity;
        }

        /// <summary>
        /// Converts the entities into domain models.
        /// </summary>
        /// <returns>The domain models.</returns>
        /// <param name="wordEntities">Word entities.</param>
        internal static IEnumerable<Word> ToDomainModels(this IEnumerable<WordEntity> wordEntities)
        {
            IEnumerable<Word> words = wordEntities.Select(wordEntity => wordEntity.ToDomainModel());

            return words;
        }

        /// <summary>
        /// Converts the domain models into entities.
        /// </summary>
        /// <returns>The entities.</returns>
        /// <param name="words">Words.</param>
        internal static IEnumerable<WordEntity> ToEntities(this IEnumerable<Word> words)
        {
            IEnumerable<WordEntity> wordEntities = words.Select(word => word.ToEntity());

            return wordEntities;
        }
    }
}
