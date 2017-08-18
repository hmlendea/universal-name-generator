using System.Collections.Generic;

namespace UniversalNameGenerator.BusinessLogic.Generators
{
    /// <summary>
    /// Random name generator that mixes words from different lists
    /// </summary>
    public class RandomMixerNameGenerator : AbstractNameGenerator
    {
        readonly string separator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomMixerNameGenerator"/> class.
        /// </summary>
        /// <param name="separator">Separator.</param>
        /// <param name="wordlists">Word lists.</param>
        public RandomMixerNameGenerator(string separator, List<List<string>> wordlists)
            : base (wordlists)
        {
            Wordlists = wordlists;
            this.separator = separator;
        }

        /// <summary>
        /// Generates a name.
        /// </summary>
        /// <returns>The name.</returns>
        protected override string GenerationAlogrithm()
        {
            string name = string.Empty;

            Wordlists.ForEach(wl => name += wl[random.Next(wl.Count)] + separator);

            return name;
        }
    }
}
