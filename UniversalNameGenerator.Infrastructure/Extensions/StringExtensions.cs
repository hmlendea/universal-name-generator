using System.Linq;

namespace UniversalNameGenerator.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Gets the duplicated elements.
        /// </summary>
        /// <param name="source">The collection.</param>
        /// <returns>The duplicated elements.</returns>
        public static string ToTitleCase(this string source)
        {
            char[] chars = source.ToLower().ToCharArray();

            for (int i = 0; i < chars.Count(); i++)
            {
                if (i == 0 || chars[i - 1] == ' ')
                {
                    chars[i] = char.ToUpper(chars[i]);
                }
            }

            return new string(chars);
        }

        public static string Repeat(this string source, int count)
        {
            string result = string.Empty;

            for (int i = 0; i < count; i++)
            {
                result += source;
            }

            return result;
        }
    }
}
