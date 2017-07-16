using System.IO;
using System.Collections.Generic;

using UniversalNameGenerator.Models;
using UniversalNameGenerator.DataAccess.Repositories;

namespace UniversalNameGenerator.Controllers
{
    /// <summary>
    /// Language controller.
    /// </summary>
    public class LanguageController
    {
        string repositoryFilePath = Path.Combine(MainClass.ApplicationDirectory, "Languages", "Languages.xml");

        /// <summary>
        /// Adds the Language.
        /// </summary>
        /// <returns>The Language.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Name.</param>
        public void Create(string id, string name)
        {
            RepositoryXml<Language> repository = new RepositoryXml<Language>(repositoryFilePath);
            Language Language = new Language
            {
                Id = id,
                Name = name
            };

            repository.Add(Language);
            repository.Save();
        }

        /// <summary>
        /// Gets the Language by identifier.
        /// </summary>
        /// <returns>The Language by identifier.</returns>
        /// <param name="id">Identifier.</param>
        public Language Get(string id)
        {
            RepositoryXml<Language> repository = new RepositoryXml<Language>(repositoryFilePath);

            return repository.Get(id);
        }

        /// <summary>
        /// Gets all Languages.
        /// </summary>
        /// <returns>The Languages.</returns>
        public List<Language> GetAll()
        {
            RepositoryXml<Language> repository = new RepositoryXml<Language>(repositoryFilePath);

            return repository.GetAll();
        }

        /// <summary>
        /// Modifies the name and description.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Name.</param>
        public void Modify(string id, string name)
        {
            RepositoryXml<Language> repository = new RepositoryXml<Language>(repositoryFilePath);
            Language Language = repository.Get(id);

            Language.Name = name;
        }

        /// <summary>
        /// Removes the Language.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public void Delete(string id)
        {
            RepositoryXml<Language> repository = new RepositoryXml<Language>(repositoryFilePath);

            repository.Remove(id);
        }
    }
}
