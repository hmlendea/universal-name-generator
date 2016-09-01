using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

using UniversalNameGenerator.Models;
using UniversalNameGenerator.Controllers;

namespace UniversalNameGenerator.Views
{
    /// <summary>
    /// Main menu.
    /// </summary>
    public class LanguageMenu : Menu
    {
        readonly CategoryController categoryController;

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <value>The language.</value>
        public Language Language { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalNameGenerator.Views.LanguageMenu"/> class.
        /// </summary>
        /// <param name="language">Language.</param>
        public LanguageMenu(Language language)
        {
            Language = language;
            Title = "Universal Name Generator [" + Language.Name + "]";
            Prompt = Language.Id + " > ";

            categoryController = new CategoryController(Language);

            foreach (Category category in categoryController.GetAll())
                AddCommand(category.Id, "Generate 15 " + category.Name.ToLower(), delegate
                    {
                        GenerateNames(category);
                    });
        }

        /// <summary>
        /// Generates names for the specified category.
        /// </summary>
        /// <param name="category">Category.</param>
        void GenerateNames(Category category)
        {
            Dictionary<string, List<string>> wordlists = new Dictionary<string, List<string>>();
            List<string> filters;
            Random rnd = new Random();
            int count;

            foreach (string wordlistId in category.Wordlists)
            {
                List<string> wordlist = new List<string>(File.ReadAllLines(
                                                Path.Combine(MainClass.ApplicationDirectory, "Languages",
                                                    Language.Id, category.Id, wordlistId + ".txt")));

                wordlists.Add(wordlistId, wordlist);
            }

            filters = new List<string>(File.ReadAllLines(
                    Path.Combine(MainClass.ApplicationDirectory, "Languages",
                        Language.Id, category.Id, "filters.txt")));

            for (count = 0; count < 15; count++)
            {
                string name = category.GenerationSchema;

                foreach (string wordlistId in wordlists.Keys)
                    if (name.Contains("{" + wordlistId + "}"))
                    {
                        List<string> wordlist = wordlists[wordlistId];

                        name = name.Replace("{" + wordlistId + "}", wordlist[rnd.Next(0, wordlist.Count)]);
                    }
                
                if (!NameIsValid(name, filters))
                {
                    count -= 1;
                    continue;
                }

                // Capitalization
                name = CultureInfo.GetCultureInfo("ro-RO").TextInfo.ToTitleCase(name);

                Console.WriteLine(name);
            }
        }

        /// <summary>
        /// Determines whether the name is valid, based on a list of filters.
        /// </summary>
        /// <returns><c>true</c> if the name is valid; otherwise, <c>false</c>.</returns>
        /// <param name="name">Name.</param>
        /// <param name="filters">Filters.</param>
        bool NameIsValid(string name, List<string> filters)
        {
            foreach (string pattern in filters)
                if (Regex.IsMatch(name, pattern))
                    return false;

            return true;
        }
    }
}