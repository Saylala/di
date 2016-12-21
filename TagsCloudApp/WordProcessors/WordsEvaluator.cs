﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudApp
{
    public class WordsEvaluator : IWordsEvaluator
    {
        public Result<Dictionary<string, Size>> Evaluate(Dictionary<string, int> words, VisualizerSettings settings)
        {
            var evaluatedWords = new Dictionary<string, Size>();
            var lengths = words.Sum(x => x.Key.Length);
            foreach (var pair in words)
            {
                var area = settings.ImageSize.Width*settings.ImageSize.Height*0.5;
                var fontSize = (float) Math.Sqrt(area / (lengths * 2) * pair.Value);
                if (fontSize < 1)
                    return Result.Fail<Dictionary<string, Size>>($"Cannot create cloud of this size {settings.ImageSize.Width}, {settings.ImageSize.Height}");
                evaluatedWords[pair.Key] = EvaluateString(pair.Key, new Font(settings.Font, fontSize, FontStyle.Regular, GraphicsUnit.Pixel));
            }
            return Result.Ok(evaluatedWords);
        }

        private Size EvaluateString(string word, Font font)
        {
            using (var image = new Bitmap(1, 1))
            {
                using (var g = Graphics.FromImage(image))
                {
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    return g.MeasureString(word, font).ToSize();
                }
            }
        }
    }
}
