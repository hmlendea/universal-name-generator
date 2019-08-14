using System;
using System.Collections.Generic;
using System.Linq;

using NuciCLI;
using NuciCLI.Menus;

using UniversalNameGenerator.BusinessLogic.GenerationManagers;
using UniversalNameGenerator.BusinessLogic.GenerationManagers.Interfaces;
using UniversalNameGenerator.Models;

namespace UniversalNameGenerator.Menus
{
    /// <summary>
    /// Category menu.
    /// </summary>
    public class CategoryMenu : Menu
    {
        const string ColumnSeparator = " | ";

        IGeneratorSchemaManager schemaManager;
        IGeneratorManager generator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenu"/> class.
        /// </summary>
        public CategoryMenu(string category) : base($"{category} name generators")
        {
            schemaManager = new GeneratorSchemaManager();
            generator = new GeneratorManager();

            IEnumerable<GenerationSchema> schemas = schemaManager
                .GetAll()
                .Where(s => s.Category == category)
                .OrderBy(s => s.Id);

            foreach (GenerationSchema schema in schemas)
            {
                Action action = delegate { GenerateNames(schema, 60); };
                AddCommand(schema.Id, schema.Name, action);
            }
        }

        void GenerateNames(GenerationSchema schema, int amount)
        {
            List<string> names = generator.GenerateNames(schema.Schema, amount, schema.FilterlistPath, schema.WordCasing).ToList();

            PrintResultsTable(names);
        }

        void PrintResultsTable(List<string> results)
        {
            int maxLength = results.Max(x => x.Length);
            int cols = Console.BufferWidth / (maxLength + ColumnSeparator.Length);

            if (cols > results.Count)
            {
                cols = results.Count;
            }

            int rows = (results.Count - 1) / cols + 1;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    int index = x + cols * y;

                    string cellValue = string.Empty;

                    if (index < results.Count)
                    {
                        cellValue = results[index];
                    }

                    NuciConsole.Write(cellValue.PadRight(maxLength, ' '));

                    if (x < cols - 1)
                    {
                        NuciConsole.Write(ColumnSeparator);
                    }
                }

                NuciConsole.WriteLine();
            }
        }
    }
}