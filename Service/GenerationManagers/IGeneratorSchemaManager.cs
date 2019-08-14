using System.Collections.Generic;

using UniversalNameGenerator.Models;

namespace UniversalNameGenerator.Service.GenerationManagers
{
    public interface IGeneratorSchemaManager
    {
        IEnumerable<GenerationSchema> GetAll();
    }
}
