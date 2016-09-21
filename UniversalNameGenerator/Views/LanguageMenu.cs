using System;
using System.Collections.Generic;

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
                        GetNames(category);
                    });
        }

        /// <summary>
        /// Gets 15 names for the specified category.
        /// </summary>
        /// <param name="category">Category.</param>
        void GetNames(Category category)
        {
            List<string> names = new List<string>();

            while(names.Count != 15)
            {
                string name = categoryController.GenerateName(category.Id);

                if (!names.Contains(name))
                    names.Add(name);
            }

            foreach (string name in names)
                Console.WriteLine(name);
        }
    }
}