using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversalNameGenerator.Models
{
    /// <summary>
    /// Category.
    /// </summary>
    public class GenerationSchema : EntityBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [MaxLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path to the wordlists.
        /// </summary>
        /// <value>The wordlists paths.</value>
        [MaxLength(20)]
        public List<string> Wordlists { get; set; }

        /// <summary>
        /// Gets or sets the generation schema.
        /// </summary>
        /// <value>The generation schema.</value>
        [MaxLength(60)]
        public string Schema { get; set; }

        /// <summary>
        /// Gets or sets the path to the filterlist.
        /// </summary>
        /// <value>The filterlist path.</value>
        public string Filterlist { get; set; }
    }
}
