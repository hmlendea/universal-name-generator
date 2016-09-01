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
        CategoryController categoryController;
        List<string> wordlistBases, wordlistSuffixes, wordlistFilters;

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
                AddCommand(category.Id, "Generate 15 " + category.Name.ToLower(), delegate { GenerateNames(category); });
        }

        /// <summary>
        /// Generates settlement names.
        /// </summary>
        /// <param name="category">Category.</param>
        void GenerateNames(Category category)
        {
            Random rnd = new Random();
            int count;

            LoadWordLists(category);

            for (count = 0; count < 15; count++)
            {
                string nameBase = wordlistBases[rnd.Next(0, wordlistBases.Count)].ToLower();
                string nameSuffix = wordlistSuffixes[rnd.Next(0, wordlistSuffixes.Count)].ToLower();
                string name = nameBase + nameSuffix;

                if (nameBase[nameBase.Length - 1] == nameSuffix[0])
                    name = name.Remove(nameBase.Length - 1, 1);

                if (!SettlementNameIsValid(name))
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
        /// Loads the word lists.
        /// </summary>
        /// <param name="category">Category.</param>
        void LoadWordLists(Category category)
        {
            wordlistBases = new List<string>(File.ReadAllLines(
                    Path.Combine(MainClass.ApplicationDirectory, "Languages",
                        Language.Id, category.Id + "_bases.txt")));
            
            wordlistSuffixes = new List<string>(File.ReadAllLines(
                    Path.Combine(MainClass.ApplicationDirectory, "Languages",
                        Language.Id, category.Id + "_suffixes.txt")));
            
            wordlistFilters = new List<string>(File.ReadAllLines(
                    Path.Combine(MainClass.ApplicationDirectory, "Languages",
                        Language.Id, category.Id + "_filters.txt")));
        }

        /// <summary>
        /// Determines whether the settlement name is valid.
        /// </summary>
        /// <returns><c>true</c> if the settlement name is valid; otherwise, <c>false</c>.</returns>
        /// <param name="name">Name.</param>
        bool SettlementNameIsValid(string name)
        {
            foreach (string pattern in wordlistFilters)
                if (Regex.IsMatch(name, pattern))
                    return false;

            return true;
        }
    }
}