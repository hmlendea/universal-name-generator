using System.Collections.Generic;
using System.IO;
using System.Linq;

using UniversalNameGenerator.DataAccess.DataObjects;
using UniversalNameGenerator.DataAccess.Repositories.Interfaces;

namespace UniversalNameGenerator.DataAccess.Repositories
{
    /// <summary>
    /// Word repository implementation.
    /// </summary>
    /// <param name="fileName">File name.</param>
    public class WordRepository(string fileName) : IWordRepository
    {
        readonly Dictionary<string, WordEntity> words = [];
        readonly string fileName = fileName;

        /// <summary>
        /// Gets all the generation words.
        /// </summary>
        /// <returns>The generation words</returns>
        public IEnumerable<WordEntity> GetAll()
        {
            LoadContent();

            return words.Values;
        }

        void LoadContent()
        {
            using StreamReader reader = File.OpenText(fileName);
            string line = string.Empty;

            while ((line = reader.ReadLine()) is not null)
            {
                WordEntity word = GetWordFromLine(line);

                if (words.TryGetValue(word.Id, out WordEntity value))
                {
                    value.Values.Add(word.Values.First());
                }
                else
                {
                    words.Add(word.Id, word);
                }
            }
        }

        static WordEntity GetWordFromLine(string line)
        {
            string processedLine = UncommentLine(line);
            int separatorIndex = processedLine.IndexOf('_');

            WordEntity word = new();

            if (separatorIndex > 0)
            {
                word.Id = processedLine[(separatorIndex + 1)..];
                word.Values = [processedLine[..separatorIndex]];
            }
            else
            {
                word.Id = processedLine;
                word.Values = [processedLine];
            }

            return word;
        }

        static string UncommentLine(string line)
        {
            int commentIndex = line.IndexOf('#');

            if (commentIndex > 0)
            {
                line = line[..commentIndex];
                line = line.TrimEnd();
            }

            return line;
        }
    }
}
