using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UniversalNameGenerator.BusinessLogic.Generators;
using UniversalNameGenerator.BusinessLogic.Generators.Interfaces;
using UniversalNameGenerator.BusinessLogic.Interfaces;
using UniversalNameGenerator.DataAccess.Repositories;
using UniversalNameGenerator.Models;

namespace UniversalNameGenerator.BusinessLogic
{
    public class GeneratorManager : IGeneratorManager
    {
        Random random;

        public IEnumerable<string> GenerateNames(string languageId, string categoryId, int amount)
        {
            string repositoryFilePath = Path.Combine("Languages", languageId, "Categories.xml");
            random = new Random();

            RepositoryXml<Category> repository = new RepositoryXml<Category>(repositoryFilePath);
            Category category = repository.Get(categoryId);

            List<string> filters;
            Dictionary<string, List<string>> wordlists = new Dictionary<string, List<string>>();

            INameGenerator generator;
            
            // Load the wordlists
            foreach (string wordlistId in category.Wordlists)
            {
                List<string> wordlist = new List<string>(File.ReadAllLines(Path.Combine("Languages", languageId, wordlistId + ".txt")));

                wordlists.Add(wordlistId, wordlist);
            }

            // Load the filters
            if (!string.IsNullOrWhiteSpace(category.Filterlist))
            {
                filters = new List<string>(File.ReadAllLines(Path.Combine("Languages", languageId, category.Filterlist + ".txt")));
            }
            else
            {
                filters = new List<string>();
            }

            List<string> names = new List<string>();

            while (names.Count < amount)
            {
                string name = category.GenerationSchema;

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

                            generator = new RandomMixerNameGenerator(split[1], wordlistKeys.Select((k) => wordlists[k]).ToList());
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

                if (!names.Contains(name))
                {
                    names.Add(name);
                }
            }

            return names;
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
            string str = "";
            int targetLength = random.Next(minLength, maxLength + 1);

            while (str.Length != targetLength)
            {
                int i = random.Next(0, choices.Count);
                str += choices[i];
            }

            return str;
        }
    }
}
