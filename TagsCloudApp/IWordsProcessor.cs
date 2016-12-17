using System.Collections.Generic;

namespace TagsCloudApp
{
    public interface IWordsProcessor
    {
        IEnumerable<string> TransformWords(IEnumerable<string> words);
        Dictionary<string, int> BuildFrequencyDictionary(IEnumerable<string> words);
    }
}
