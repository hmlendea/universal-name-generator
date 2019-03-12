using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using NuciExtensions;

using UniversalNameGenerator.BusinessLogic.GenerationManagers.Interfaces;
using UniversalNameGenerator.BusinessLogic.Mapping;
using UniversalNameGenerator.BusinessLogic.NameGenerators.Interfaces;
using UniversalNameGenerator.BusinessLogic.NameGenerators.Markov;
using UniversalNameGenerator.BusinessLogic.NameGenerators.Randomiser;
using UniversalNameGenerator.DataAccess.Repositories;
using UniversalNameGenerator.DataAccess.Repositories.Interfaces;
using UniversalNameGenerator.Models;
using UniversalNameGenerator.Models.Enumerations;

namespace UniversalNameGenerator.BusinessLogic.GenerationManagers
{
    public class GeneratorManager : IGeneratorManager
    {
        Random random;

        Dictionary<string, INameGenerator> generators;

        public GeneratorManager()
        {
            generators = new Dictionary<string, INameGenerator>();
        }

        public IEnumerable<string> GenerateNames(string schema, int amount, string filterlist, WordCasing casing)
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
                            values = GenerateRandomiserNames(schema, amount, split, filters);
                            break;

                        case "markov":
                            values = GenerateMarkovNames(schema, amount, split, filters);
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

                switch(casing)
                {
                    case WordCasing.Lower:
                        name = name.ToLower();
                        break;

                    case WordCasing.Upper:
                        name = name.ToUpper();
                        break;

                    case WordCasing.Title:
                        name = name.ToTitleCase();
                        break;

                    case WordCasing.Sentence:
                        name = name.ToSentanceCase();
                        break;
                }

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

        IEnumerable<string> GenerateRandomiserNames(string schema, int amount, string[] split, List<string> filters)
        {
            int minLength = int.Parse(split[2]);
            int maxLength = int.Parse(split[3]);
            List<string> wordlistKeys = split[4].Split('|').ToList();

            List<List<Word>> wordlists = new List<List<Word>>();

            foreach (string wordlistId in wordlistKeys)
            {
                string filePath = Path.Combine("Wordlists", wordlistId + ".txt");

                IWordRepository wordRepository = new WordRepository(filePath);
                List<Word> wordlist = wordRepository.GetAll().ToDomainModels().ToList();

                wordlists.Add(wordlist);
            }

            INameGenerator generator;

            if (generators.Keys.Contains(schema))
            {
                generator = generators[schema];
            }
            else
            {
                generator = new RandomiserNameGenerator(split[1], wordlists)
                {
                    MinNameLength = minLength,
                    MaxNameLength = maxLength,
                    ExcludedStrings = filters
                };
                generators.Add(schema, generator);
            }

            return generator.Generate(amount);
        }

        IEnumerable<string> GenerateMarkovNames(string schema, int amount, string[] split, List<string> filters)
        {
            int minLength = int.Parse(split[1]);
            int maxLength = int.Parse(split[2]);
            List<string> wordlistKeys = split[3].Split('|').ToList();

            List<List<Word>> wordlists = new List<List<Word>>();

            foreach (string wordlistId in wordlistKeys)
            {
                string filePath = Path.Combine("Wordlists", wordlistId + ".txt");

                IWordRepository wordRepository = new WordRepository(filePath);
                List<Word> wordlist = wordRepository.GetAll().ToDomainModels().ToList();

                wordlists.Add(wordlist);
            }

            INameGenerator generator;

            if (generators.Keys.Contains(schema))
            {
                generator = generators[schema];
            }
            else
            {
                generator = new MarkovNameGenerator(wordlists, 4, 0.0f)
                {
                    MinNameLength = minLength,
                    MaxNameLength = maxLength,
                    ExcludedStrings = filters
                };
                generators.Add(schema, generator);
            }

            return generator.Generate(amount);
        }
    }
}
