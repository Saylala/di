using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudApp
{
	class WordsEvaluator : IWordsEvaluator
	{
		public Dictionary<string, int> Evaluate(Dictionary<string, int> words, Size imageSize)
		{
			var fontSizeDictionary = new Dictionary<string, int>();
			foreach (var pair in words)
				fontSizeDictionary[pair.Key] = imageSize.Width * pair.Value / (int) Math.Pow(words.Count, 1.5);
			return fontSizeDictionary;
		}
	}
}
