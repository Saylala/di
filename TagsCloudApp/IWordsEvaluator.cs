using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudApp
{
	public interface IWordsEvaluator
	{
		Dictionary<string, int> Evaluate(Dictionary<string, int> words, Size imageSize);
	}
}
