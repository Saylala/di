using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudApp
{
    public interface IWordsEvaluator
    {
        Result<Dictionary<string, Size>> Evaluate(Dictionary<string, int> words, VisualizerSettings settings);
    }
}
