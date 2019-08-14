using System;
using System.Collections.Generic;
using System.Linq;

using NuciCLI.Menus;
using NuciExtensions;

using UniversalNameGenerator.Service.GenerationManagers;

namespace UniversalNameGenerator.Menus
{
    /// <summary>
    /// Main menu.
    /// </summary>
    public class MainMenu : Menu
    {
        IGeneratorSchemaManager schemaManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenu"/> class.
        /// </summary>
        public MainMenu() : base("Universal Name Generator")
        {
            schemaManager = new GeneratorSchemaManager();

            IEnumerable<string> categories = schemaManager
                .GetAll()
                .Select(x => x.Category)
                .Distinct()
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .OrderBy(x => x);
            
            foreach (string category in categories)
            {
                Action action = delegate { MenuManager.Instance.OpenMenu<CategoryMenu>(category); };
                string id = $"{category.ToLowerSnakeCase().Replace('_', '-')}-names";
                string description = $"Generate {category} names";
                
                AddCommand(id, description, action);
            }
        }
    }
}
