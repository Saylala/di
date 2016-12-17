using System.Collections.Generic;
using System.Drawing;
using Autofac;
using FakeItEasy;
using NUnit.Framework;
using TagsCloudApp;
using TagsCloudApp.Factories;

namespace TagsCloudAppTests
{
    [TestFixture]
    class UserInterfaceTests
    {
        private IWordsProcessor processor;
        private IWordsEvaluator evaluator;
        private ICloudVisualizer visualizer;
        private ICloudLayouter layouter;
        private IVisualizerSettingsFactory visualizerSettingsFactory;
        private IInputStreamFactory inputFactory;
        private IColorGiverFactory colorGiverFactory;
        private IOutputStreamFactory fileSaverFactory;


        private ContainerBuilder SetUp(ContainerBuilder builder)
        {
            processor = A.Fake<IWordsProcessor>();
            builder.RegisterInstance(processor).As<IWordsProcessor>();
            evaluator = A.Fake<IWordsEvaluator>();
            builder.RegisterInstance(evaluator).As<IWordsEvaluator>();

            visualizer = A.Fake<ICloudVisualizer>();
            builder.RegisterInstance(visualizer).As<ICloudVisualizer>();

            layouter = A.Fake<ICloudLayouter>();
            builder.RegisterInstance(layouter).As<ICloudLayouter>();

            visualizerSettingsFactory = A.Fake<IVisualizerSettingsFactory>();
            builder.RegisterInstance(visualizerSettingsFactory).As<IVisualizerSettingsFactory>();
            inputFactory = A.Fake<IInputStreamFactory>();
            builder.RegisterInstance(inputFactory).As<IInputStreamFactory>();
            colorGiverFactory = A.Fake<IColorGiverFactory>();
            builder.RegisterInstance(colorGiverFactory).As<IColorGiverFactory>();
            fileSaverFactory = A.Fake<IOutputStreamFactory>();
            builder.RegisterInstance(fileSaverFactory).As<IOutputStreamFactory>();

            builder.RegisterType<UserInterface>();

            return builder;
        }

        [Test]
        public void Interface_ShouldCall_ImportantParts()
        {
            var container = SetUp(new ContainerBuilder()).Build();

            var options = new Options
            {
                InputFile = "",
                OutputFile = "output.png",
                ImageWidth = 500,
                ImageHeight = 500,
                Font = "Arial",
                BackgroundColor = "Black",
                Colors = null
            };

            using (var scope = container.BeginLifetimeScope())
            {
                var ui = scope.Resolve<UserInterface>();
                ui.Work(options);
            }

            A.CallTo(() => visualizerSettingsFactory.Create(Color.Black, new Size(500, 500), new FontFamily("Arial")))
                .MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => inputFactory.Create(options)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => processor.TransformWords(A<IEnumerable<string>>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => processor.BuildFrequencyDictionary(A<IEnumerable<string>>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => evaluator.Evaluate(A<Dictionary<string, int>>.Ignored, A<VisualizerSettings>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => layouter.CreateCloud(A<Dictionary<string, Size>>.Ignored, A<Size>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => colorGiverFactory.Create(options)).MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => visualizer.Visualize(A<Cloud<string>>.Ignored, A<VisualizerSettings>.Ignored))
                .MustHaveHappened(Repeated.Exactly.Once);
            A.CallTo(() => fileSaverFactory.Create(options)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
