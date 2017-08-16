using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UniversalNameGenerator.BusinessLogic.Generators;
using UniversalNameGenerator.BusinessLogic.Generators.Interfaces;
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

            List<string> names = new List<string>();

            while (names.Count < amount)
            {
                string name = schema;

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
                            value = GenerateRandomiserName(split, filters);
                            break;

                        case "markov":
                            value = GenerateMarkovName(split, filters);
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

        string GenerateRandomiserName(string[] split, List<string> filters)
        {
            List<string> wordlistKeys = split[2].Split('|').ToList();

            List<List<string>> wordlists = new List<List<string>>();

            foreach (string wordlistId in wordlistKeys)
            {
                List<string> wordlist = File.ReadAllLines(Path.Combine("Wordlists", wordlistId + ".txt")).ToList();

                wordlists.Add(wordlist);
            }

            INameGenerator generator = new RandomMixerNameGenerator(split[1], wordlists);
            generator.ExcludedStrings = filters;

            return generator.GenerateName();
        }

        string GenerateMarkovName(string[] split, List<string> filters)
        {
            List<string> wordlist = File.ReadAllLines(Path.Combine("Wordlists", split[1] + ".txt")).ToList();

            INameGenerator generator = new MarkovNameGenerator(wordlist);
            generator.ExcludedStrings = filters;

            return generator.GenerateName();
        }
    }
}
