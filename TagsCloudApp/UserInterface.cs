
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudApp.Factories;

namespace TagsCloudApp
{
    public class UserInterface
    {
        private readonly IVisulizerSettingsFactory settingsFactory;
        private readonly IInputStreamFactory readerFactory;
        private readonly IWordsProcessor processor;
        private readonly IWordsEvaluator wordsEvaluator;
        private readonly ICloudLayouter layouter;
        private readonly IColorGiverFactory colorGiverFactory;
        private readonly ICloudVisualizer visualizer;
        private readonly IOutputStreamFactory saverFactory;


        public UserInterface(IVisulizerSettingsFactory settingsFactory, IInputStreamFactory readerFactory, IWordsProcessor processor,
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

            var words = PrepareWords(args);
            var evaluatedWords = wordsEvaluator.Evaluate(words, settings);
            var cloud = layouter.CreateCloud(evaluatedWords, settings.ImageSize);

            var colorGiver = colorGiverFactory.Create(args);
            var coloredCloud = colorGiver.GiveColors(cloud);

            var image = visualizer.Visualize(coloredCloud, settings);
            var saver = saverFactory.Create(args);
            saver.SaveData(image);
        }

        private Dictionary<string, int> PrepareWords(Options args)
        {
            var reader = readerFactory.Create(args);
            var rawWords = reader.GetData();
            var filteredWords = processor.TransformWords(rawWords);
            return processor.BuildFrequencyDictionary(filteredWords);
        }

        private VisualizerSettings GetSettings(Options args)
        {
            var color = Color.FromName(args.BackgroundColor);
            var size = new Size(args.ImageWidth, args.ImageHeight);
            var font = new FontFamily(args.Font);
            return settingsFactory.Create(color, size, font);
        }
    }
}
