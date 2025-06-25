namespace UniversalNameGenerator.Models
{
    public enum WordCasing
    {
        Original,

        /// <summary>
        /// Every letter is lowercase.
        /// </summary>
        Lower,

        /// <summary>
        /// Every letter is uppercase.
        /// </summary>
        Upper,

        /// <summary>
        /// The first words starts with an uppercase letter, and the rest are lowercase.
        /// </summary>
        Title,

        /// <summary>
        /// Each word starts with an uppercase letter, and the rest are lowercase.
        /// </summary>
        Sentence
    }
}
