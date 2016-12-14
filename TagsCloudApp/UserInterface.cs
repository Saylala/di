
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudApp
{
	public class UserInterface
	{
		private readonly IFileReader reader;
		private readonly IWordsProcessor processor;
		private readonly CloudVisualizer visualizer;
		private readonly IColorGiver colorGiver;
		private readonly IVisulizerSettingsFactory settingsFactory;
		private readonly IFileSaver saver;
		private readonly IWordsEvaluator wordsEvaluator;

		public UserInterface(IFileReader reader, IWordsProcessor processor, CloudVisualizer visualizer,
			IColorGiver colorGiver, IWordsEvaluator wordsEvaluator, IVisulizerSettingsFactory settingsFactory, IFileSaver saver)
		{
			this.reader = reader;
			this.processor = processor;
			this.visualizer = visualizer;
			this.colorGiver = colorGiver;
			this.settingsFactory = settingsFactory;
			this.saver = saver;
			this.wordsEvaluator = wordsEvaluator;
		}

		public void Work(Options args)
		{
			var inputPath = args.InputFile;
			var outputPath = args.OutputFile;

			var settings = GetSettings(args);

			var words = PrepareWords(inputPath);
			var coloredWords = colorGiver.GiveColors(words);
			var evaluatedWords = wordsEvaluator.Evaluate(words, settings.ImageSize);

			var image = visualizer.Visualise(coloredWords, evaluatedWords, settings);
			saver.Save(image, outputPath);
		}

		private Dictionary<string, int> PrepareWords(string filename)
		{
			var rawWords = reader.ReadFile(filename);
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
