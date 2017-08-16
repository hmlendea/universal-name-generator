using System.Collections.Generic;

using UniversalNameGenerator.Models;

namespace UniversalNameGenerator.BusinessLogic.Interfaces
{
    public interface IGeneratorSchemaManager
    {
        IEnumerable<GenerationSchema> GetAll();
    }
}
