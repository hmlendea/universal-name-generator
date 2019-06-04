using System.Collections.Generic;

using NuciDAL.DataObjects;

namespace UniversalNameGenerator.DataAccess.DataObjects
{
    public sealed class WordEntity : EntityBase
    {
        public ICollection<string> Values { get; set; }

        public WordEntity()
        {
            Values = new List<string>();
        }
    }
}
