using System;
using Autofac;
using TagsCloudApp.Factories;

namespace TagsCloudApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FileReader>().As<IInputStream>();
            builder.RegisterType<FileSaver>().As<IOutputStream>();

            builder.RegisterType<WordsProcessor>().As<IWordsProcessor>();
            builder.RegisterType<WordsEvaluator>().As<IWordsEvaluator>();

            builder.RegisterType<ColorGiver>().As<IColorGiver>();
            builder.RegisterType<CloudVisualizer>().As<ICloudVisualizer>();
            builder.RegisterType<SpiralFactory>().As<ICurveFactory>();

            builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>();

            builder.RegisterType<VisulizerSettingsFactory>().As<IVisulizerSettingsFactory>();
            builder.RegisterType<FileReaderFactory>().As<IInputStreamFactory>();
            builder.RegisterType<FileSaverFactory>().As<IOutputStreamFactory>();

            builder.RegisterType<UserInterface>();

            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                var options = new Options();
                if (!CommandLine.Parser.Default.ParseArguments(args, options))
                    return;
                var ui = scope.Resolve<UserInterface>();
                ui.Work(options);
            }
        }
    }
}
