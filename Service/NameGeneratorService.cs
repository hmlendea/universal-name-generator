using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using NuciDAL.Repositories;
using NuciExtensions;

using UniversalNameGenerator.DataAccess;
using UniversalNameGenerator.DataAccess.DataObjects;
using UniversalNameGenerator.DataAccess.Repositories;
using UniversalNameGenerator.Models;
using UniversalNameGenerator.Service.Mapping;
using UniversalNameGenerator.Service.NameGenerators;
using UniversalNameGenerator.Service.NameGenerators.Markov;
using UniversalNameGenerator.Service.NameGenerators.Randomiser;

namespace UniversalNameGenerator.Service
{
    public class NameGeneratorService : INameGeneratorService
    {
        Random random;

        readonly Dictionary<string, INameGenerator> generators;

        readonly XmlRepository<GenerationSchemaEntity> schemaRepository;

        public NameGeneratorService()
        {
            generators = [];
            schemaRepository = new("GenerationSchemas.xml");
        }

        public IEnumerable<string> GenerateNames(string schema, int amount, string filterlist, WordCasing casing)
        {
            random = new Random();

            List<string> filters;

            // Load the filters
            if (!string.IsNullOrWhiteSpace(filterlist))
            {
                filters = [.. File.ReadAllLines(Path.Combine("Wordlists", filterlist + ".lst"))];
            }
            else
            {
                filters = [];
            }

            List<List<string>> z = [];
            List<string> names = [];

            int generatorsCount = schema.Count(x => x.Equals('{'));

            while (z.Count < generatorsCount)
            {
                string name = schema;
                string currentGeneration = schema;

                while (currentGeneration.Contains('{') || currentGeneration.Contains('}'))
                {
                    int pos = currentGeneration.IndexOf('{') + 1;
                    string com = currentGeneration[pos..currentGeneration.IndexOf('}')];
                    IEnumerable<string> values = [];

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

                name = GetNameWithCasing(name, casing);
                names.Add(name);
            }

            return names;
        }

        public IEnumerable<GenerationSchema> GetSchemas()
            => schemaRepository.GetAll().ToDomainModels();

        static string GetNameWithCasing(string name, WordCasing casing)
        {
            if (casing.Equals(WordCasing.Lower))
            {
                return name.ToLower();
            }

            if (casing.Equals(WordCasing.Upper))
            {
                return name.ToUpper();
            }

            if (casing.Equals(WordCasing.Title))
            {
                return name.ToTitleCase();
            }

            if (casing.Equals(WordCasing.Sentence))
            {
                return name.ToSentenceCase();
            }

            return name;
        }

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <returns>The string.</returns>
        /// <param name="charlist">Charlist.</param>
        /// <param name="minLength">Minimum length.</param>
        /// <param name="maxLength">Maximum length.</param>
        List<string> RandomStrings(int amount, List<string> choices, int minLength, int maxLength)
        {
            string str = string.Empty;
            int targetLength = random.Next(minLength, maxLength + 1);

            List<string> names = [];

            while (names.Count <= amount && names.Count.NotEquals(choices.Count))
            {
                while (str.Length.NotEquals(targetLength))
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

            List<Wordlist> wordlists = GetWordLists(wordlistKeys);

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

            List<Wordlist> wordlists = GetWordLists(wordlistKeys);

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

        static List<Wordlist> GetWordLists(List<string> wordlistKeys)
        {
            List<Wordlist> wordlists = [];

            foreach (string wordlistId in wordlistKeys)
            {
                string filePath = Path.Combine(ApplicationPaths.WordlistsDirectory, $"{wordlistId}.lst");

                WordRepository wordRepository = new(filePath);
                IEnumerable<Word> words = wordRepository.GetAll().ToDomainModels();
                Wordlist wordlist = [.. words];

                wordlists.Add(wordlist);
            }

            return wordlists;
        }
    }
}
