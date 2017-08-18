using System.Collections.Generic;

namespace UniversalNameGenerator.BusinessLogic.Generators.Interfaces
{
    /// <summary>
    /// Name Generator interface.
    /// </summary>
    public interface INameGenerator
    {
        /// <summary>
        /// Gets or sets the minimum length of the name.
        /// </summary>
        /// <value>The minimum length of the name.</value>
        int MinNameLength { get; set; }

        /// <summary>
        /// Gets or sets the maximum length of the name.
        /// </summary>
        /// <value>The maximum length of the name.</value>
        int MaxNameLength { get; set; }

        /// <summary>
        /// Gets or sets the maximum processing time.
        /// </summary>
        /// <value>The maximum processing time in milliseconds.</value>
        int MaxProcessingTimePerWord { get; set; }

        /// <summary>
        /// Gets or sets the excluded strings.
        /// </summary>
        /// <value>The excluded strings.</value>
        List<string> ExcludedStrings { get; set; }

        /// <summary>
        /// Gets or sets the included strings.
        /// </summary>
        /// <value>The included strings.</value>
        List<string> IncludedStrings { get; set; }

        /// <summary>
        /// Gets or sets the string that all generated names must start with.
        /// </summary>
        /// <value>The string start filter.</value>
        string StartsWithFilter { get; set; }

        /// <summary>
        /// Gets or sets the string that all generated names must end with.
        /// </summary>
        /// <value>The string end filter.</value>
        string EndsWithFilter { get; set; }

        /// <summary>
        /// Gets the used words.
        /// </summary>
        /// <value>The used words.</value>
        List<string> GeneratedWords { get; }

        /// <summary>
        /// Gets the word lists.
        /// </summary>
        /// <value>The word lists.</value>
        List<List<string>> Wordlists { get; }
        
        /// <summary>
        /// Generates names.
        /// </summary>
        /// <returns>The names.</returns>
        /// <param name="maximumCount">Maximum count.</param>
        IEnumerable<string> Generate(int maximumCount);

        /// <summary>
        /// Reset the list of used names.
        /// </summary>
        void Reset();
    }
}
