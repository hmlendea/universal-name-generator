using System.Collections.Generic;

namespace UniversalNameGenerator.DataAccess.DataObjects
{
    public sealed class WordEntity
    {
        public string Id { get; set; }

        public IEnumerable<string> Values { get; set; }

        public WordEntity()
        {
            Values = new List<string>();
        }
    }
}
