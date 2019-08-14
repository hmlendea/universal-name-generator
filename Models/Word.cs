using System.Collections.Generic;

namespace UniversalNameGenerator.Models
{
    public sealed class Word
    {
        public string Id { get; set; }

        public IEnumerable<string> Values { get; set; }
    }
}
