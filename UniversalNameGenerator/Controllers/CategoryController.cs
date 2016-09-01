using System.IO;
using System.Collections.Generic;

using UniversalNameGenerator.Models;
using UniversalNameGenerator.Repositories;

namespace UniversalNameGenerator.Controllers
{
    /// <summary>
    /// Category controller.
    /// </summary>
    public class CategoryController
    {
        readonly string repositoryFilePath;

        public Language Language { get; private set; }

        public CategoryController(Language language)
        {
            Language = language;

            repositoryFilePath = Path.Combine(MainClass.ApplicationDirectory, "Languages", Language.Id, "Categories.xml");
        }

        /// <summary>
        /// Adds the Category.
        /// </summary>
        /// <returns>The Category.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Name.</param>
        /// <param name="wordlists">Wordlists.</param>
        public void Create(string id, string name, List<string> wordlists)
        {
            RepositoryXml<Category> repository = new RepositoryXml<Category>(repositoryFilePath);
            Category Category = new Category
            {
                Id = id,
                Name = name,
                Wordlists = wordlists
            };

            repository.Add(Category);
            repository.Save();
        }

        /// <summary>
        /// Gets the Category by identifier.
        /// </summary>
        /// <returns>The Category by identifier.</returns>
        /// <param name="id">Identifier.</param>
        public Category Get(string id)
        {
            RepositoryXml<Category> repository = new RepositoryXml<Category>(repositoryFilePath);

            return repository.Get(id);
        }

        /// <summary>
        /// Gets all Categorys.
        /// </summary>
        /// <returns>The Categorys.</returns>
        public List<Category> GetAll()
        {
            RepositoryXml<Category> repository = new RepositoryXml<Category>(repositoryFilePath);

            return repository.GetAll();
        }

        /// <summary>
        /// Modifies the name and description.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="name">Name.</param>
        /// <param name="wordlists">Wordlists.</param>
        public void Modify(string id, string name, List<string> wordlists)
        {
            RepositoryXml<Category> repository = new RepositoryXml<Category>(repositoryFilePath);
            Category Category = repository.Get(id);

            Category.Name = name;
            Category.Wordlists = wordlists;
        }

        /// <summary>
        /// Removes the Category.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public void Delete(string id)
        {
            RepositoryXml<Category> repository = new RepositoryXml<Category>(repositoryFilePath);

            repository.Remove(id);
        }
    }
}
