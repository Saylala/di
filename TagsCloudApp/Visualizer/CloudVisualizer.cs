using System.Collections.Generic;
using System.Drawing;


namespace TagsCloudApp
{
    public class CloudVisualizer
    {
	    private readonly ICloudLayouterFactory layouterFactory;
	    private CloudLayouter layouter;

		public CloudVisualizer(ICloudLayouterFactory layouterFactory)
        {
			this.layouterFactory = layouterFactory;
        }

        private void DrawWord(string word, Color color, FontFamily font, int fontSize, Graphics graphics)
        {
	        var wordWithBorder = $" {word} ";
			var stringFont = new Font(font, fontSize);
	        var size = graphics.MeasureString(wordWithBorder, stringFont).ToSize();
	        var rectangle = layouter.PutNextRectangle(size);
			var brush = new SolidBrush(color);
			graphics.DrawString(word, stringFont, brush, rectangle);
        }

        public Bitmap Visualise(Dictionary<string, Color> coloredWords, Dictionary<string, int> evaluatedWords, VisualizerSettings settings)
        {
            var image = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            var graphics = Graphics.FromImage(image);

			var center = new Point(settings.ImageSize.Width / 2, settings.ImageSize.Height / 2);
			layouter = layouterFactory.Create(center);


			graphics.Clear(settings.BackgroundColor);
			foreach (var pair in coloredWords)
				DrawWord(pair.Key, pair.Value, settings.Font, evaluatedWords[pair.Key], graphics);
			graphics.Save();

	        return image;
        }
    }
}
