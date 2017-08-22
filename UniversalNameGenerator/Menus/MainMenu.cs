using System;
using System.Collections.Generic;
using System.Linq;

using UniversalNameGenerator.BusinessLogic.GenerationManagers;
using UniversalNameGenerator.BusinessLogic.GenerationManagers.Interfaces;
using UniversalNameGenerator.Models;

namespace UniversalNameGenerator.Menus
{
    /// <summary>
    /// Main menu.
    /// </summary>
    public class MainMenu : Menu
    {
        IGeneratorSchemaManager schemaManager;
        IGeneratorManager generator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenu"/> class.
        /// </summary>
        public MainMenu()
        {
            Title = "Universal Name Generator";

            schemaManager = new GeneratorSchemaManager();
            generator = new GeneratorManager();

            List<GenerationSchema> schemas = schemaManager.GetAll()
                                                          .OrderBy(s => s.Name)
                                                          .ToList();

            schemas.ForEach(schema => AddCommand(schema.Id,
                                                 schema.Name,
                                                 delegate { GenerateNames(schema, 10); }));
        }

        void GenerateNames(GenerationSchema schema, int amount)
        {
            List<string> names = generator.GenerateNames(schema.Schema, amount, schema.FilterlistPath, schema.WordCasing).ToList();

            names.ForEach(Console.WriteLine);
        }
    }
}