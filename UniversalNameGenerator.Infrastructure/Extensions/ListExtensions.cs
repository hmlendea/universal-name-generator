using System.Collections.Generic;
using System.Linq;

namespace UniversalNameGenerator.Infrastructure.Extensions
{
    /// <summary>
    /// List extensions.
    /// </summary>
    public static class ListExtensions
    {
        public static T Pop<T>(this IList<T> source)
        {
            int index = source.Count - 1;

            T element = source.ElementAt(index);
            source.RemoveAt(index);

            return element;
        }
    }
}
