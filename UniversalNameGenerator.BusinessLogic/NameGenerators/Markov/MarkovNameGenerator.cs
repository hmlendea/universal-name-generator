using System.Collections.Generic;
using System.Linq;

using UniversalNameGenerator.Infrastructure.Extensions;

namespace UniversalNameGenerator.BusinessLogic.NameGenerators.Markov
{
    public class MarkovNameGenerator : NameGenerator
    {
        public int Order { get; private set; }

        public float Prior { get; private set; }

        public List<string> Data { get; private set; }

        List<MarkovModel> models;
        
        public MarkovNameGenerator(List<string> data, int order, float prior)
            : this(new List<List<string>> { data }, order, prior)
        {
        }

        public MarkovNameGenerator(List<List<string>> data, int order, float prior)
            : base(data)
        {
            List<string> mergedDataLists = data.SelectMany(x => x).ToList();

            Order = order;
            Prior = prior;
            Data = mergedDataLists;

            List<string> letters = new List<string>();

            foreach (string word in Data)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    letters.Add(word[i].ToString());
                }
            }

            letters.Sort();

            List<string> domain = letters.ToList();
            domain.Insert(0, "#");

            models = new List<MarkovModel>();
            for (int i = 0; i < order; i++)
            {
                MarkovModel model = new MarkovModel(Data.ToList(), Order - i, Prior, domain);
                models.Add(model);
            }
        }

        protected override string GenerationAlogrithm()
        {
            string name = string.Empty;

            name = Generate();
            name = name.Replace("#", string.Empty);

            if (IsNameValid(name) && !Data.Contains(name))
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
                if (letter != null)
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

                if (letter == null)
                {
                    myContext = myContext.Substring(1);
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
