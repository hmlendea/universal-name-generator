using NuciDAL.DataObjects;
using NuciExtensions;

namespace UniversalNameGenerator.DataAccess.DataObjects
{
    public class GenerationSchemaEntity : EntityBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the generation schema.
        /// </summary>
        /// <value>The generation schema.</value>
        public string Schema { get; set; }

        /// <summary>
        /// Gets or sets the path to the filterlist.
        /// </summary>
        /// <value>The filterlist path.</value>
        public string FilterlistPath { get; set; }

        public string WordCase { get; set; }

        public GenerationSchemaEntity()
            => WordCase = NuciGenerators.Text.Models.WordCase.Title.GetDisplayName();
    }
}
