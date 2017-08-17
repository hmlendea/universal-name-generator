using System;
using System.Collections.Generic;

namespace UniversalNameGenerator.BusinessLogic.Generators
{
    /// <summary>
    /// Random name generator that mixes words from different lists
    /// </summary>
    public class RandomMixerNameGenerator : AbstractNameGenerator
    {
        readonly List<List<string>> wordLists;
        readonly string separator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomMixerNameGenerator"/> class.
        /// </summary>
        /// <param name="separator">Separator.</param>
        /// <param name="wordLists">Word lists.</param>
        public RandomMixerNameGenerator(string separator, List<List<string>> wordLists)
        {
            this.wordLists = wordLists;
            this.separator = separator;
        }

        /// <summary>
        /// Generates a name.
        /// </summary>
        /// <returns>The name.</returns>
        public override string GenerateName()
        {
            string name = string.Empty;

            DateTime startTime = DateTime.Now;

            while (DateTime.Now < startTime.AddMilliseconds(MaxProcessingTimePerWord) &&
                   !IsNameValid(name))
            {
                name = string.Empty;

                wordLists.ForEach(wl => name += wl[random.Next(wl.Count)] + separator);
            }

            return name;
        }
    }
}
