using System.Collections.Generic;

using UniversalNameGenerator.BusinessLogic.Interfaces;
using UniversalNameGenerator.DataAccess.Repositories;
using UniversalNameGenerator.Models;

namespace UniversalNameGenerator.BusinessLogic
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
        /// <param name="language">Language.</param>
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
            RepositoryXml<GenerationSchema> repository = new RepositoryXml<GenerationSchema>(repositoryFilePath);

            return repository.GetAll();
        }
    }
}
