using System;
using System.Text.RegularExpressions;
using System.Globalization;
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
        readonly Random rnd;

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <value>The language.</value>
        public Language Language { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalNameGenerator.Controllers.CategoryController"/> class.
        /// </summary>
        /// <param name="language">Language.</param>
        public CategoryController(Language language)
        {
            Language = language;

            repositoryFilePath = Path.Combine(MainClass.ApplicationDirectory, "Languages", Language.Id, "Categories.xml");
            rnd = new Random();
        }

        /// <summary>
        /// Adds the category.
        /// </summary>
        /// <returns>The category.</returns>
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
        /// Gets the category by identifier.
        /// </summary>
        /// <returns>The category.</returns>
        /// <param name="id">Identifier.</param>
        public Category Get(string id)
        {
            RepositoryXml<Category> repository = new RepositoryXml<Category>(repositoryFilePath);

            return repository.Get(id);
        }

        /// <summary>
        /// Gets all categories.
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

        /// <summary>
        /// Generates a name.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        public string GenerateName(string id)
        {
            RepositoryXml<Category> repository = new RepositoryXml<Category>(repositoryFilePath);
            Category category = repository.Get(id);

            List<string> filters;
            Dictionary<string, List<string>> wordlists = new Dictionary<string, List<string>>();
            string name = string.Empty;

            foreach (string wordlistId in category.Wordlists)
            {
                List<string> wordlist = new List<string>(File.ReadAllLines(
                                                Path.Combine(MainClass.ApplicationDirectory, "Languages",
                                                    Language.Id, wordlistId + ".txt")));

                wordlists.Add(wordlistId, wordlist);
            }

            filters = new List<string>(File.ReadAllLines(
                    Path.Combine(MainClass.ApplicationDirectory, "Languages",
                        Language.Id, category.Filterlist + ".txt")));
            
            while (!NameIsValid(name, filters))
            {
                name = category.GenerationSchema;

                foreach (string wordlistId in wordlists.Keys)
                    if (name.Contains("{" + wordlistId + "}"))
                    {
                        string word = string.Empty;

                        while (string.IsNullOrWhiteSpace(word))
                            word = wordlists[wordlistId][rnd.Next(wordlists[wordlistId].Count)];

                        name = name.Replace("{" + wordlistId + "}", word);
                    }

                // Capitalization
                name = CultureInfo.GetCultureInfo("ro-RO").TextInfo.ToTitleCase(name);
            }

            return name;
        }

        /// <summary>
        /// Determines whether the name is valid, based on a list of filters.
        /// </summary>
        /// <returns><c>true</c> if the name is valid; otherwise, <c>false</c>.</returns>
        /// <param name="name">Name.</param>
        /// <param name="filters">Filters.</param>
        bool NameIsValid(string name, List<string> filters)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            foreach (string pattern in filters)
                if (Regex.IsMatch(name, pattern))
                    return false;

            return true;
        }
    }
}
