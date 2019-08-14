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
    public class WordRepository : IWordRepository
    {
        readonly Dictionary<string, WordEntity> words;
        readonly string fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="WordRepository"/> class.
        /// </summary>
        /// <param name="fileName">File name.</param>
        public WordRepository(string fileName)
        {
            words = new Dictionary<string, WordEntity>();

            this.fileName = fileName;
        }

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
            using (StreamReader reader = File.OpenText(fileName))
            {
                string line = string.Empty;

                while ((line = reader.ReadLine()) != null)
                {
                    WordEntity word = GetWordFromLine(line);

                    if (words.ContainsKey(word.Id))
                    {
                        words[word.Id].Values.Add(word.Values.First());
                    }
                    else
                    {
                        words.Add(word.Id, word);
                    }
                }
            }
        }

        WordEntity GetWordFromLine(string line)
        {
            string processedLine = UncommentLine(line);
            int separatorIndex = processedLine.IndexOf('_');

            WordEntity word = new WordEntity();

            if (separatorIndex > 0)
            {
                word.Id = processedLine.Substring(separatorIndex + 1);
                word.Values = new List<string> { processedLine.Substring(0, separatorIndex) };
            }
            else
            {
                word.Id = processedLine;
                word.Values = new List<string> { processedLine };
            }

            return word;
        }

        string UncommentLine(string line)
        {
            int commentIndex = line.IndexOf('#');

            if (commentIndex > 0)
            {
                line = line.Substring(0, commentIndex);
                line = line.TrimEnd();
            }

            return line;
        }
    }
}
