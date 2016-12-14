using Autofac;
using TagsCloudApp.Factories;

namespace TagsCloudApp
{
	public class Program
    {
	    static void Main(string[] args)
	    {
		    var builder = new ContainerBuilder();
		    builder.RegisterType<FileReader>().As<IFileReader>();
			builder.RegisterType<FileSaver>().As<IFileSaver>();

			builder.RegisterType<WordsProcessor>().As<IWordsProcessor>();
			builder.RegisterType<WordsEvaluator>().As<IWordsEvaluator>();

			builder.RegisterType<ColorGiver>().As<IColorGiver>();
			builder.RegisterType<CloudVisualizer>();
			builder.RegisterType<SpiralFactory>().As<ICurveFactory>();
			builder.RegisterType<CloudLayouter>();

			builder.RegisterType<CloudLayouterFactory>().As<ICloudLayouterFactory>();
			builder.RegisterType<VisulizerSettingsFactory>().As<IVisulizerSettingsFactory>();

		    builder.RegisterType<UserInterface>();

		    var container = builder.Build();
			using (var scope = container.BeginLifetimeScope())
			{
				var options = new Options();
				if (!CommandLine.Parser.Default.ParseArguments(args, options)) return;
				var ui = scope.Resolve<UserInterface>();
				ui.Work(options);
			}
		}
    }
}
