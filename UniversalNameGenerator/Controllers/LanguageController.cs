﻿using System.IO;
using System.Collections.Generic;

using UniversalNameGenerator.Models;
using UniversalNameGenerator.Repositories;

namespace UniversalNameGenerator.Controllers
{
    public class LanguageController
    {
        readonly string repositoryXmlFile = Path.Combine("names", "languages.xml");

        /// <summary>
        /// Adds the Language.
        /// </summary>
        /// <returns>The Language.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Name.</param>
        /// <param name="categories">Categories.</param>
        public void Create(string id, string name, List<string> categories)
        {
            RepositoryXml<Language> repository = new RepositoryXml<Language>(repositoryXmlFile);
            Language Language = new Language()
            {
                Id = id,
                Name = name,
                Categories = categories
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
            RepositoryXml<Language> repository = new RepositoryXml<Language>(repositoryXmlFile);

            return repository.Get(id);
        }

        /// <summary>
        /// Gets all Languages.
        /// </summary>
        /// <returns>The Languages.</returns>
        public List<Language> GetAll()
        {
            RepositoryXml<Language> repository = new RepositoryXml<Language>(repositoryXmlFile);

            return repository.GetAll();
        }

        /// <summary>
        /// Modifies the name and description.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Name.</param>
        /// <param name="categories">Categories.</param>
        public void Modify(string id, string name, List<string> categories)
        {
            RepositoryXml<Language> repository = new RepositoryXml<Language>(repositoryXmlFile);
            Language Language = repository.Get(id);

            Language.Name = name;
            Language.Categories = categories;
        }

        /// <summary>
        /// Removes the Language.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public void Delete(string id)
        {
            RepositoryXml<Language> repository = new RepositoryXml<Language>(repositoryXmlFile);

            repository.Remove(id);
        }
    }
}
