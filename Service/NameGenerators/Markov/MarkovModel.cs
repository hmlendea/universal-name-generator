using System;
using System.Collections.Generic;
using System.Linq;

using NuciExtensions;

namespace UniversalNameGenerator.Service.NameGenerators.Markov
{
    public class MarkovModel
    {
        readonly int order;
        readonly float prior;
        readonly List<string> alphabet;
        readonly Dictionary<string, List<string>> observations;
        Dictionary<string, List<float>> chains;
        readonly Random random;

        public MarkovModel(IEnumerable<string> data, int order, float prior, IEnumerable<string> alphabet)
        {
            this.order = order;
            this.prior = prior;
            this.alphabet = alphabet.ToList();

            observations = [];
            random = new Random();

            Train(data);
            BuildChains();
        }

        public string Generate(string context)
        {
            List<float> chain = chains.TryGetValue(context);

            if (chain is null)
            {
                return null;
            }

            return alphabet[SelectIndex(chain)];
        }

        void Train(IEnumerable<string> data)
        {
            List<string> dataList = data.ToList();

            while(!dataList.IsEmpty())
            {
                string d = dataList.Pop();

                d = $"{"#".Repeat(order)}{d}#";

                for (int i = 0; i < d.Length - order; i++)
                {
                    string key = d.Substring(i, order);

                    List<string> value = observations.TryGetValue(key);

                    if (value is null)
                    {
                        value = [];
                        observations.AddOrUpdate(key, value); // TODO: Does the update part cause issues?
                    }

                    char letter = d[i + order];
                    value.Add(letter.ToString());
                }
            }
        }

        void BuildChains()
        {
            chains = [];

            foreach(string context in observations.Keys)
            {
                foreach (string prediction in alphabet)
                {
                    List<float> value = chains.TryGetValue(context);

                    if (value is null)
                    {
                        value = [];
                        chains.AddOrUpdate(context, value);
                    }

                    int matchesCount = CountMatches(observations[context], prediction);
                    value.Add(prior + matchesCount);
                }
            }
        }

        static int CountMatches(IEnumerable<string> array, string val)
        {
            if (array is null)
            {
                return 0;
            }

            return array.Count(str => str.Equals(val));
        }

        int SelectIndex(IEnumerable<float> chain)
        {
            List<float> totals = [];
            float accumulator = 0;

            foreach (float weight in chain)
            {
                accumulator += weight;
                totals.Add(accumulator);
            }

            float rand = (float)random.NextDouble() * accumulator;

            for (int i = 0; i < totals.Count; i++)
            {
                if (rand < totals[i])
                {
                    return i;
                }
            }

            return 0;
        }
    }
}
