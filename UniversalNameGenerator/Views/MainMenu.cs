using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace UniversalNameGenerator.Views
{
    public class MainMenu : Menu
    {
        List<string> settlementBases, settlementSuffixes, settlementFilters;

        /// <summary>
        /// Initializes a new instance of the <see cref="UniversalNameGenerator.Views.MainMenu"/> class.
        /// </summary>
        public MainMenu()
        {
            Title = "Universal Name Generator [RO]";

            AddCommand("settlements", "Generate 15 settlement names", GenerateSettlementNames);
        }

        /// <summary>
        /// Generates 15 settlement names.
        /// </summary>
        void GenerateSettlementNames()
        {
            Random rnd = new Random();
            int count;

            LoadWordLists();

            for (count = 0; count < 15; count++)
            {
                string nameBase = settlementBases[rnd.Next(0, settlementBases.Count)].ToLower();
                string nameSuffix = settlementSuffixes[rnd.Next(0, settlementSuffixes.Count)].ToLower();
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
        void LoadWordLists()
        {
            settlementBases = new List<string>(File.ReadAllLines(
                    Path.Combine("names", "romanian", "settlement_bases.txt")));
            settlementSuffixes = new List<string>(File.ReadAllLines(
                    Path.Combine("names", "romanian", "settlement_suffixes.txt")));
            settlementFilters = new List<string>(File.ReadAllLines(
                    Path.Combine("names", "romanian", "settlement_filters.txt")));
        }

        /// <summary>
        /// Determines whether the settlement name is valid.
        /// </summary>
        /// <returns><c>true</c> if the settlement name is valid; otherwise, <c>false</c>.</returns>
        /// <param name="name">Name.</param>
        bool SettlementNameIsValid(string name)
        {
            foreach (string pattern in settlementFilters)
                if (Regex.IsMatch(name, pattern))
                    return false;

            return true;
        }
    }
}