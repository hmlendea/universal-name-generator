using System.Collections.Generic;

using UniversalNameGenerator.BusinessLogic.GenerationManagers.Interfaces;
using UniversalNameGenerator.BusinessLogic.Mapping;
using UniversalNameGenerator.DataAccess.Repositories;
using UniversalNameGenerator.DataAccess.Repositories.Interfaces;
using UniversalNameGenerator.Models;

namespace UniversalNameGenerator.BusinessLogic.GenerationManagers
{
    /// <summary>
    /// Category controller.
    /// </summary>
    public class GeneratorSchemaManager : IGeneratorSchemaManager
    {
        readonly string repositoryFilePath;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="GeneratorSchemaManager"/> class.
        /// </summary>
        public GeneratorSchemaManager()
        {
            repositoryFilePath = "GenerationSchemas.xml";
        }

        /// <summary>
        /// Gets all generation schemas.
        /// </summary>
        /// <returns>The generation schemas.</returns>
        public IEnumerable<GenerationSchema> GetAll()
        {
            IGenerationSchemaRepository repository = new GenerationSchemaRepository(repositoryFilePath);

            return repository.GetAll().ToDomainModels();
        }
    }
}
