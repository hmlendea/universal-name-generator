using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversalNameGenerator.Models
{
    public class Language : EntityBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [MaxLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of categories.
        /// </summary>
        /// <value>The list of categories.</value>
        [MaxLength(20)]
        public List<string> Categories { get; set; }
    }
}