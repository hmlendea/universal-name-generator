using System.Collections.Generic;

using UniversalNameGenerator.Models;
using UniversalNameGenerator.Models.Enumerations;

namespace UniversalNameGenerator.Service
{
    public interface INameGeneratorService
    {
        IEnumerable<string> GenerateNames(string schema, int amount, string filterlist, WordCasing casing);

        IEnumerable<GenerationSchema> GetSchemas();
    }
}
