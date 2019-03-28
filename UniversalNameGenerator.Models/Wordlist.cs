using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UniversalNameGenerator.Models
{
    public sealed class Wordlist : IEnumerable<Word>
    {
        Dictionary<string, Word> words;
        
        public IEnumerable<Word> Values => words.Values;

        public Wordlist()
        {
            words = new Dictionary<string, Word>();
        }

        public Wordlist(IEnumerable<Word> words)
        {
            this.words = words.ToDictionary(x => x.Id, x => x);
        }

        public void Add(Word word)
            => words.Add(word.Id, word);

        public Word Get(string id)
            => words[id]; 

        public IEnumerator<Word> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
