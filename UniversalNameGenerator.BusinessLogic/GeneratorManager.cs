using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UniversalNameGenerator.BusinessLogic.Generators;
using UniversalNameGenerator.BusinessLogic.Generators.Interfaces;
using UniversalNameGenerator.BusinessLogic.Generators.MarkovNameGenerator;
using UniversalNameGenerator.BusinessLogic.Interfaces;

namespace UniversalNameGenerator.BusinessLogic
{
    public class GeneratorManager : IGeneratorManager
    {
        Random random;

        public IEnumerable<string> GenerateNames(string schema, string filterlist, int amount)
        {
            random = new Random();

            List<string> filters;

            // Load the filters
            if (!string.IsNullOrWhiteSpace(filterlist))
            {
                filters = new List<string>(File.ReadAllLines(Path.Combine("Wordlists", filterlist + ".txt")));
            }
            else
            {
                filters = new List<string>();
            }

            List<List<string>> z = new List<List<string>>();
            List<string> names = new List<string>();

            int generatorsCount = schema.Count(x => x == '{');

            while (z.Count < generatorsCount)
            {
                string name = schema;
                string currentGeneration = schema;

                while (currentGeneration.Contains("{") || currentGeneration.Contains("}"))
                {
                    int pos = currentGeneration.IndexOf('{') + 1;
                    string com = currentGeneration.Substring(pos, currentGeneration.IndexOf('}') - pos);
                    IEnumerable<string> values = new List<string>();

                    string[] split = com.Split(',');

                    switch (split[0])
                    {
                        case "random":
                            values = RandomStrings(amount, split[1].Split('|').ToList(), int.Parse(split[2]), int.Parse(split[3]));
                            break;

                        case "randomiser":
                            values = GenerateRandomiserNames(amount, split, filters);
                            break;

                        case "markov":
                            values = GenerateMarkovNames(amount, split, filters);
                            break;
                    }

                    currentGeneration = currentGeneration.Replace("{" + com + "}", string.Empty);
                    z.Add(values.ToList());
                }
            }

            for (int i = 0; i < z.Min(x => x.Count); i++)
            {
                string name = string.Empty;

                z.ForEach(x => name += x[i]);
                names.Add(name);
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
        IEnumerable<string> RandomStrings(int amount, List<string> choices, int minLength, int maxLength)
        {
            string str = "";
            int targetLength = random.Next(minLength, maxLength + 1);

            List<string> names = new List<string>();

            while (names.Count <= amount && names.Count != choices.Count)
            {
                while (str.Length != targetLength)
                {
                    int i = random.Next(0, choices.Count);
                    str += choices[i];
                }

                names.Add(str);
            }

            return names;
        }

        IEnumerable<string> GenerateRandomiserNames(int amount, string[] split, List<string> filters)
        {
            List<string> wordlistKeys = split[2].Split('|').ToList();

            List<List<string>> wordlists = new List<List<string>>();

            foreach (string wordlistId in wordlistKeys)
            {
                string filePath = Path.Combine("Wordlists", wordlistId + ".txt");
                List<string> wordlist = File.ReadAllLines(filePath).ToList();

                wordlists.Add(wordlist);
            }

            INameGenerator generator = new RandomMixerNameGenerator(split[1], wordlists);
            generator.ExcludedStrings = filters;

            return generator.Generate(amount);
        }

        IEnumerable<string> GenerateMarkovNames(int amount, string[] split, List<string> filters)
        {
            List<string> wordlistKeys = split[1].Split('|').ToList();

            List<List<string>> wordlists = new List<List<string>>();

            foreach (string wordlistId in wordlistKeys)
            {
                string filePath = Path.Combine("Wordlists", wordlistId + ".txt");
                List<string> wordlist = File.ReadAllLines(filePath).ToList();

                wordlists.Add(wordlist);
            }

            INameGenerator generator = new MarkovNameGenerator(wordlists, 3, 0.0f)
            {
                MinNameLength = 5,
                MaxNameLength = 12,
                ExcludedStrings = filters
            };
            
            return generator.Generate(amount);
        }
    }
}
