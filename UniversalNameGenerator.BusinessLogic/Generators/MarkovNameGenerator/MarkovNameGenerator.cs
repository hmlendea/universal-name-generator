using System.Collections.Generic;

namespace UniversalNameGenerator.BusinessLogic.Generators.MarkovNameGenerator
{
    public class MarkovNameGenerator : AbstractNameGenerator
    {
        MarkovGenerator generator;

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
