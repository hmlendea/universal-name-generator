using System.Collections.Generic;

using UniversalNameGenerator.Models.Enumerations;

namespace UniversalNameGenerator.BusinessLogic.Interfaces
{
    public interface IGeneratorManager
    {
        IEnumerable<string> GenerateNames(string schema, int amount, string filterlist, WordCasing casing);
    }
}
