using System.Collections.Generic;

using UniversalNameGenerator.Models;

namespace UniversalNameGenerator.BusinessLogic.GenerationManagers.Interfaces
{
    public interface IGeneratorSchemaManager
    {
        IEnumerable<GenerationSchema> GetAll();
    }
}
