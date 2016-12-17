using System.Collections.Generic;
using System.Linq;

namespace TagsCloudApp
{
    public class WordsProcessor : IWordsProcessor
    {
        public IEnumerable<string> TransformWords(IEnumerable<string> words)
        {
            return words.Select(x => x.ToLower());
        }

        public Dictionary<string, int> BuildFrequencyDictionary(IEnumerable<string> words)
        {
            var dictionary = new Dictionary<string, int>();
            foreach (var word in words)
                dictionary[word] = dictionary.ContainsKey(word) ? dictionary[word]+1 : 1;
            return dictionary;
        }
    }
}
