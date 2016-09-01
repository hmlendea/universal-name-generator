using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversalNameGenerator.Models
{
    /// <summary>
    /// Category.
    /// </summary>
    public class Category : EntityBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [MaxLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the wordlists.
        /// </summary>
        /// <value>The wordlists.</value>
        [MaxLength(20)]
        public List<string> Wordlists { get; set; }

        [MaxLength(60)]
        public string GenerationSchema { get; set; }
    }
}