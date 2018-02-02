using System.Collections.Generic;

using UniversalNameGenerator.Common.Extensions;
using UniversalNameGenerator.Models;

namespace UniversalNameGenerator.BusinessLogic.NameGenerators.Randomiser
{
    /// <summary>
    /// Random name generator that mixes words from different lists
    /// </summary>
    public class RandomiserNameGenerator : NameGenerator
    {
        readonly string separator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomiserNameGenerator"/> class.
        /// </summary>
        /// <param name="separator">Separator.</param>
        /// <param name="wordlists">Word lists.</param>
        public RandomiserNameGenerator(string separator, List<List<Word>> wordlists)
            : base (wordlists)
        {
            Wordlists = wordlists;
            OnlyNewNames = false;

            this.separator = separator;
        }

        /// <summary>
        /// Generates a name.
        /// </summary>
        /// <returns>The name.</returns>
        protected override string GenerationAlogrithm()
        {
            string name = string.Empty;

            Wordlists.ForEach(wl => name += wl.GetRandomElement() + separator);

            return name;
        }
    }
}
