using System;
using System.ComponentModel.DataAnnotations;

namespace UniversalNameGenerator.Models
{
    /// <summary>
    /// Category.
    /// </summary>
    public class GenerationSchema : IEquatable<GenerationSchema>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        [StringLength(40, ErrorMessage = "The {0} must be between {1} and {2} characters long", MinimumLength = 3)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [MaxLength(20)]
        public string Name { get; set; }

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
        public string FilterlistPath { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="Army"/> is equal to the current <see cref="Army"/>.
        /// </summary>
        /// <param name="other">The <see cref="Army"/> to compare with the current <see cref="Army"/>.</param>
        /// <returns><c>true</c> if the specified <see cref=Army"/> is equal to the current
        /// <see cref="Army"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(GenerationSchema other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Id, other.Id) &&
                   string.Equals(Name, other.Name) &&
                   string.Equals(Schema, other.Schema) &&
                   string.Equals(FilterlistPath, other.FilterlistPath);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="Army"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="Army"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="Army"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((GenerationSchema)obj);
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="Army"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Id != null ? Id.GetHashCode() : 0) * 397) ^
                       (Name != null ? Name.GetHashCode() : 0) ^
                       (Schema != null ? Schema.GetHashCode() : 0) ^
                       (FilterlistPath != null ? FilterlistPath.GetHashCode() : 0);
            }
        }
    }
}
