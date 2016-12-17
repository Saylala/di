using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace TagsCloudApp
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input file")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = false, DefaultValue = "output.png", HelpText = "Output file")]
        public string OutputFile { get; set; }

        [Option('w', "width", Required = false, DefaultValue = 500, HelpText = "Image width")]
        public int ImageWidth { get; set; }

        [Option('h', "height", Required = false, DefaultValue = 500, HelpText = "Image height")]
        public int ImageHeight { get; set; }

        [Option('f', "font", Required = false, DefaultValue = "Arial", HelpText = "Font for words")]
        public string Font { get; set; }

        [Option('b', "background", Required = false, DefaultValue = "Black", HelpText = "Background color")]
        public string BackgroundColor { get; set; }

        [Option('c', "colors", Required = false, DefaultValue = null, HelpText = "Available colors for words")]
        public IEnumerable<string> Colors { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
            {
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };
            help.AddPreOptionsLine("Usage: TagsCloudApp.exe -i %input filename%");
            help.AddOptions(this);
            return help;
        }
    }
}
