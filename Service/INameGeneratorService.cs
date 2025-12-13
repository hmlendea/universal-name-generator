using System.Collections.Generic;

using NuciGenerators.Text.Models;

namespace UniversalNameGenerator.Service
{
    public interface INameGeneratorService
    {
        IEnumerable<string> GenerateNames(string schema, int amount, string filterlist, WordCase casing);

        IEnumerable<GenerationSchema> GetSchemas();
    }
}
