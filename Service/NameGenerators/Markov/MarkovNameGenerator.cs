using System.Collections.Generic;
using System.Linq;

using NuciExtensions;

using UniversalNameGenerator.Models;

namespace UniversalNameGenerator.Service.NameGenerators.Markov
{
    public class MarkovNameGenerator : NameGenerator
    {
        public int Order { get; private set; }

        public float Prior { get; private set; }

        public List<Word> Data { get; private set; }

        readonly List<MarkovModel> models;

        public MarkovNameGenerator(List<Word> data, int order, float prior)
            : this([[.. data]], order, prior)
        {
        }

        public MarkovNameGenerator(List<Wordlist> data, int order, float prior) : base(data)
        {
            List<Word> mergedDataLists = data.SelectMany(x => x).ToList();

            Order = order;
            Prior = prior;
            Data = mergedDataLists;

            List<string> letters = [];
            List<string> wordValues = mergedDataLists.SelectMany(x => x.Values).ToList();

            foreach (string wordValue in wordValues)
            {
                for (int i = 0; i < wordValue.Length; i++)
                {
                    letters.Add(wordValue[i].ToString());
                }
            }

            letters.Sort();

            List<string> domain = letters.Distinct().ToList();
            domain.Insert(0, "#");

            models = [];
            for (int i = 0; i < order; i++)
            {
                MarkovModel model = new(wordValues.ToList(), Order - i, Prior, domain.ToList());
                models.Add(model);
            }
        }

        protected override string GenerationAlogrithm()
        {
            string name = string.Empty;

            name = Generate();
            name = name.Replace("#", string.Empty);

            if (IsNameValid(name) && !Data.Any(w => w.Values.Contains(name)))
            {
                return name;
            }

            return null;
        }

        public string Generate()
        {
            string word = "#".Repeat(Order);
            string letter = GetLetter(word);

            while(letter != "#")
            {
                if (letter is not null)
                {
                    word += letter;
                }

                letter = GetLetter(word);
            }

            return word;
        }

        public string GetLetter(string context)
        {
            string letter = null;
            string myContext = context.Substring(context.Length - Order, Order);

            foreach (MarkovModel model in models)
            {
                letter = model.Generate(myContext);

                if (letter is null)
                {
                    myContext = myContext[1..];
                }
                else
                {
                    break;
                }
            }

            return letter;
        }
    }
}
