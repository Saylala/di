using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudApp.Factories;

namespace TagsCloudApp
{
    public class UserInterface
    {
        private readonly IVisualizerSettingsFactory settingsFactory;
        private readonly IInputStreamFactory readerFactory;
        private readonly IWordsProcessor processor;
        private readonly IWordsEvaluator wordsEvaluator;
        private readonly ICloudLayouter layouter;
        private readonly IColorGiverFactory colorGiverFactory;
        private readonly ICloudVisualizer visualizer;
        private readonly IOutputStreamFactory saverFactory;


        public UserInterface(IVisualizerSettingsFactory settingsFactory, IInputStreamFactory readerFactory, IWordsProcessor processor,
            IWordsEvaluator wordsEvaluator, ICloudLayouter layouter, IColorGiverFactory colorGiverFactory,
            ICloudVisualizer visualizer, IOutputStreamFactory saverFactory)
        {
            this.settingsFactory = settingsFactory;
            this.readerFactory = readerFactory;
            this.processor = processor;
            this.wordsEvaluator = wordsEvaluator;
            this.layouter = layouter;
            this.colorGiverFactory = colorGiverFactory;
            this.visualizer = visualizer;
            this.saverFactory = saverFactory;
        }

        public void Work(Options args)
        {
            var settings = GetSettings(args);
            if (!settings.IsSuccess)
            {
                Console.WriteLine(settings.Error);
                return;
            }

            var words = GetWords(args);
            if (!words.IsSuccess)
            {
                Console.WriteLine(words.Error);
                return;
            }

            var preraredWords = PrepareWords(words.Value);
            var evaluatedWords = wordsEvaluator.Evaluate(preraredWords, settings.Value);
            if (!evaluatedWords.IsSuccess)
            {
                Console.WriteLine(evaluatedWords.Error);
                return;
            }

            var cloud = layouter.CreateCloud(evaluatedWords.Value, settings.Value.ImageSize);
            if (!cloud.IsSuccess)
            {
                Console.WriteLine(cloud.Error);
                return;
            }

            var colorGiver = colorGiverFactory.Create(args);
            var coloredCloud = colorGiver.GiveColors(cloud.Value);

            var image = visualizer.Visualize(coloredCloud, settings.Value);
            var saver = saverFactory.Create(args);
            saver.SaveData(image);
        }

        private Result<IEnumerable<string>> GetWords(Options args)
        {
            var reader = readerFactory.Create(args);

            return reader.GetData();
        }

        private Dictionary<string, int> PrepareWords(IEnumerable<string> words)
        {
            var filteredWords = processor.TransformWords(words);
            return processor.BuildFrequencyDictionary(filteredWords);
        }

        private Result<VisualizerSettings> GetSettings(Options args)
        {
            var color = GetColor(args.BackgroundColor);
            if (!color.IsSuccess)
                return Result.Fail<VisualizerSettings>(color.Error);

            var size = GetSize(args.ImageWidth, args.ImageHeight);
            if (!size.IsSuccess)
                return Result.Fail<VisualizerSettings>(size.Error);

            var font = Result.Of(() => new FontFamily(args.Font));
            if (!font.IsSuccess)
                return Result.Fail<VisualizerSettings>(font.Error);
            return Result.Of(() => settingsFactory.Create(color.Value, size.Value, font.Value));
        }

        private Result<Color> GetColor(string colorName)
        {
            var color = Color.FromName(colorName);
            if (color.IsKnownColor)
                return Result.Ok(color);
            return Result.Fail<Color>("Invalid color");
        }

        private Result<Size> GetSize(int width, int height)
        {
            if (width <= 0)
                return Result.Fail<Size>("Width must be higher than 0");
            if (height <= 0)
                return Result.Fail<Size>("Height must be higher than 0");
            return Result.Ok(new Size(width, height));
        }
    }
}
