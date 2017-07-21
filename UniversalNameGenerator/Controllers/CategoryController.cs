using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using UniversalNameGenerator.BusinessLogic.Generators;
using UniversalNameGenerator.BusinessLogic.Generators.Interfaces;
using UniversalNameGenerator.Models;
using UniversalNameGenerator.DataAccess.Repositories;

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

            INameGenerator generator;

            string name = string.Empty;
            
            // Load the wordlists
            foreach (string wordlistId in category.Wordlists)
            {
                List<string> wordlist = new List<string>(File.ReadAllLines(
                                                Path.Combine(MainClass.ApplicationDirectory, "Languages",
                                                    Language.Id, wordlistId + ".txt")));

                wordlists.Add(wordlistId, wordlist);
            }

            // Load the filters
            if (!string.IsNullOrWhiteSpace(category.Filterlist))
            {
                filters = new List<string>(File.ReadAllLines(
                        Path.Combine(MainClass.ApplicationDirectory, "Languages",
                            Language.Id, category.Filterlist + ".txt")));
            }
            else
            {
                filters = new List<string>();
            }

            while (!NameIsValid(name, filters))
            {
                name = category.GenerationSchema;

                // TODO: Remove this once the new generators are the only ones used
                foreach (string wordlistId in wordlists.Keys)
                {
                    if (name.Contains("{" + wordlistId + "}"))
                    {
                        string word = string.Empty;

                        while (string.IsNullOrWhiteSpace(word))
                        {
                            word = wordlists[wordlistId][rnd.Next(wordlists[wordlistId].Count)];
                        }

                        name = name.Replace("{" + wordlistId + "}", word);
                    }
                }

                while (name.Contains("{") || name.Contains("}"))
                {
                    int pos = name.IndexOf('{') + 1;
                    string com = name.Substring(pos, name.IndexOf('}') - pos);
                    string value = "";

                    string[] split = com.Split(',');

                    switch (split[0])
                    {
                        case "random":
                            value = RandomString(split[1].Split('|').ToList(), int.Parse(split[2]), int.Parse(split[3]));

                            break;

                        case "randomiser":
                            List<string> wordlistKeys = split[2].Split('|').ToList();

                            generator = new RandomMixerNameGenerator(split[1], wordlists[wordlistKeys[0]], wordlists[wordlistKeys[1]]);
                            generator.ExcludedStrings = filters;

                            value = generator.GenerateName();
                            break;

                        case "markov":
                            generator = new MarkovNameGenerator(wordlists[split[1]]);
                            generator.ExcludedStrings = filters;

                            value = generator.GenerateName();
                            break;
                    }

                    name = name.Replace("{" + com + "}", value);
                }
            }

            // Capitalization
            name = CultureInfo.GetCultureInfo("ro-RO").TextInfo.ToTitleCase(name);

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

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <returns>The string.</returns>
        /// <param name="charlist">Charlist.</param>
        /// <param name="minLength">Minimum length.</param>
        /// <param name="maxLength">Maximum length.</param>
        string RandomString(List<string> choices, int minLength, int maxLength)
        {
            Random rnd = new Random();
            string str = "";
            int targetLength = rnd.Next(minLength, maxLength + 1);

            while (str.Length != targetLength)
            {
                int i = rnd.Next(0, choices.Count);
                str += choices[i];
            }

            return str;
        }
    }
}
