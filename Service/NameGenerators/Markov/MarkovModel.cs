using System;
using System.Collections.Generic;
using System.Linq;

using NuciExtensions;

namespace UniversalNameGenerator.BusinessLogic.NameGenerators.Markov
{
    public class MarkovModel
    {
        int order;
        float prior;
        List<string> alphabet;
        Dictionary<string, List<string>> observations;
        Dictionary<string, List<float>> chains;
        Random random;

        public MarkovModel(IEnumerable<string> data, int order, float prior, IEnumerable<string> alphabet)
        {
            this.order = order;
            this.prior = prior;
            this.alphabet = alphabet.ToList();

            observations = new Dictionary<string, List<string>>();
            random = new Random();

            Train(data);
            BuildChains();
        }

        public string Generate(string context)
        {
            List<float> chain = chains.TryGetValue(context);

            if (chain == null)
            {
                return null;
            }

            return alphabet[SelectIndex(chain)];
        }

        void Train(IEnumerable<string> data)
        {
            List<string> dataList = data.ToList();

            while(dataList.Count != 0)
            {
                string d = dataList.Pop();

                d = $"{"#".Repeat(order)}{d}#";

                for(int i = 0; i < d.Length - order; i++)
                {
                    string key = d.Substring(i, order);

                    List<string> value = observations.TryGetValue(key);

                    if (value == null)
                    {
                        value = new List<string>();
                        observations.AddOrUpdate(key, value); // TODO: Does the update part cause issues?
                    }

                    char letter = d[i + order];
                    value.Add(letter.ToString());
                }
            }
        }

        void Retrain(IEnumerable<string> data)
        {
            Train(data);
            BuildChains();
        }

        void BuildChains()
        {
            chains = new Dictionary<string, List<float>>();

            foreach(string context in observations.Keys)
            {
                foreach (string prediction in alphabet)
                {
                    List<float> value = chains.TryGetValue(context);

                    if (value == null)
                    {
                        value = new List<float>();
                        chains.AddOrUpdate(context, value);
                    }

                    int matchesCount = CountMatches(observations[context], prediction);
                    value.Add(prior + matchesCount);
                }
            }
        }

        int CountMatches(IEnumerable<string> array, string val)
        {
            if (array == null)
            {
                return 0;
            }

            return array.Count(str => str == val);
        }

        int SelectIndex(IEnumerable<float> chain)
        {
            List<float> totals = new List<float>();
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
