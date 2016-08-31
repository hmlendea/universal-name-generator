using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversalNameGenerator.Models
{
    /// <summary>
    /// Language.
    /// </summary>
    public class Language : EntityBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [MaxLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        [MaxLength(20)]
        public List<string> Categories { get; set; }
    }
}