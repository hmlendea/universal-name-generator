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
    }
}