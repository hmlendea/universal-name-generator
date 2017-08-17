using System;
using System.Collections.Generic;

using UniversalNameGenerator.Infrastructure.Extensions;

namespace UniversalNameGenerator.BusinessLogic.Generators.MarkovNameGenerator
{
    public class MarkovNameGenerator
    {
        MarkovGenerator generator;

        public MarkovNameGenerator(List<string> data, int order, float prior)
        {
            generator = new MarkovGenerator(data, order, prior);
        }

        public string GenerateName(int minLength, int maxLength, string startsWith, string endsWith, string includes, string excludes)
        {
            string name = string.Empty;

            name = generator.Generate();
            name = name.Replace("#", string.Empty);

            if (name.Length <= minLength || name.Length >= maxLength)
            {
                return null;
            }
            
            if ((!string.IsNullOrEmpty(startsWith) && !name.StartsWith(startsWith)) ||
                (!string.IsNullOrEmpty(endsWith) && !name.EndsWith(endsWith)))
            {
                return null;
            }

            if ((!string.IsNullOrEmpty(includes) && !name.Contains(includes)) ||
                (!string.IsNullOrEmpty(excludes) && name.Contains(excludes)))
            {
                return null;
            }

            return name;
        }

        public IEnumerable<string> GenerateNames(int count, int minLength, int maxLength, string startsWith, string endsWith, string includes, string excludes, TimeSpan maxTimePerName)
        {
            List<string> names = new List<string>();

            DateTime startTime = DateTime.Now;
            DateTime currentTime = DateTime.Now;
            
            while(names.Count < count && currentTime > startTime.Add(maxTimePerName.Multiply(count)))
            {
                string name = GenerateName(minLength, maxLength, startsWith, endsWith, includes, excludes);

                if (name != null)
                {
                    names.Add(name);
                }

                currentTime = DateTime.Now;
            }

            return names;
        }
    }
}
