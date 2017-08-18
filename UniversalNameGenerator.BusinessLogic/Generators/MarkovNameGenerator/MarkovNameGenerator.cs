using System.Collections.Generic;
using System.Linq;

namespace UniversalNameGenerator.BusinessLogic.Generators.MarkovNameGenerator
{
    public class MarkovNameGenerator : AbstractNameGenerator
    {
        MarkovGenerator generator;

        public MarkovNameGenerator(List<List<string>> data, int order, float prior)
            : base(data)
        {
            List<string> mergedDataLists = data.SelectMany(x => x).ToList();

            generator = new MarkovGenerator(mergedDataLists, order, prior);
        }

        public MarkovNameGenerator(List<string> data, int order, float prior)
            : base(new List<List<string>> { data })
        {
            generator = new MarkovGenerator(data, order, prior);
        }

        protected override string GenerationAlogrithm()
        {
            string name = string.Empty;

            name = generator.Generate();
            name = name.Replace("#", string.Empty);

            if (IsNameValid(name) && !generator.Data.Contains(name))
            {
                return name;
            }

            return null;
        }
    }
}
