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
        const string ColumnSeparator = " | ";

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
                                                          .OrderBy(s => s.Id)
                                                          .ToList();

            schemas.ForEach(schema => AddCommand(schema.Id,
                                                 schema.Name,
                                                 delegate { GenerateNames(schema, 40); }));
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

                    Console.Write(cellValue.PadRight(maxLength, ' '));

                    if (x < cols - 1)
                    {
                        Console.Write(ColumnSeparator);
                    }
                }

                Console.WriteLine();
            }
        }
    }
}