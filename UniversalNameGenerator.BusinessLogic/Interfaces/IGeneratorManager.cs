using System.Collections.Generic;

namespace UniversalNameGenerator.BusinessLogic.Interfaces
{
    public interface IGeneratorManager
    {
        IEnumerable<string> GenerateNames(string languageId, string categoryId, int amount);
    }
}
