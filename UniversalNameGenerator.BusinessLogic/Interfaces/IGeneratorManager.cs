using System.Collections.Generic;

using UniversalNameGenerator.Models;

namespace UniversalNameGenerator.BusinessLogic.Interfaces
{
    public interface IGeneratorManager
    {
        IEnumerable<string> GenerateNames(string schema, string filterlist, int amount);
    }
}
