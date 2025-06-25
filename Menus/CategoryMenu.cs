using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using NuciCLI;
using NuciCLI.Menus;
using NuciExtensions;

using UniversalNameGenerator.Models;
using UniversalNameGenerator.Service;

namespace UniversalNameGenerator.Menus
{
    /// <summary>
    /// Category menu.
    /// </summary>
    public class CategoryMenu : Menu
    {
        const string ColumnSeparator = " | ";

        readonly NameGeneratorService nameGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenu"/> class.
        /// </summary>
        public CategoryMenu(string category) : base($"{category} name generators")
        {
            nameGenerator = new NameGeneratorService();

            IEnumerable<GenerationSchema> schemas = nameGenerator
                .GetSchemas()
                .Where(s => s.Category?.Equals(category) ?? false)
                .OrderBy(s => s.Id);

            string categoryCommand = $"{category.ToLowerSnakeCase().Replace('_', '-')}";

            foreach (GenerationSchema schema in schemas)
            {
                void action() { GenerateNames(schema, 60); }
                string command = schema.Id.Replace($"{categoryCommand}-", "", ignoreCase: true, CultureInfo.InvariantCulture);

                AddCommand(command, schema.Name, action);
            }
        }

        void GenerateNames(GenerationSchema schema, int amount)
        {
            List<string> names = nameGenerator.GenerateNames(schema.Schema, amount, schema.FilterlistPath, schema.WordCasing).ToList();

            PrintResultsTable(names);
        }

        static void PrintResultsTable(List<string> results)
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