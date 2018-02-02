using System.Collections.Generic;
using System.IO;

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
                    int separatorIndex = line.IndexOf('_');

                    string id = string.Empty;
                    string value = string.Empty;

                    if (separatorIndex > 0)
                    {
                        value = line.Substring(0, separatorIndex);
                        id = line.Substring(separatorIndex + 1);
                    }
                    else
                    {
                        id = line;
                        value = line;
                    }

                    if (words.ContainsKey(id))
                    {
                        words[id].Values.Add(value);
                    }
                    else
                    {
                        WordEntity word = new WordEntity
                        {
                            Id = id,
                            Values = new List<string> { value }
                        };

                        words.Add(word.Id, word);
                    }
                }
            }
        }
    }
}
