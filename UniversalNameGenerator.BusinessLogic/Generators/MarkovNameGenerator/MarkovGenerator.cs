using System.Collections.Generic;
using System.Linq;

using UniversalNameGenerator.Infrastructure.Extensions;

namespace UniversalNameGenerator.BusinessLogic.Generators.MarkovNameGenerator
{
    public class MarkovGenerator
    {
        public int Order { get; private set; }

        float prior;

        List<MarkovModel> models;

        public MarkovGenerator(List<string> data, int order, float prior)
        {
            Order = order;
            this.prior = prior;

            List<string> letters = new List<string>();

            foreach (string word in data)
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
                MarkovModel model = new MarkovModel(data.ToList(), Order - i, prior, domain);
                models.Add(model);
            }
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
