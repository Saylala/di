using System.Drawing;


namespace TagsCloudApp
{
    public class CloudVisualizer : ICloudVisualizer
    {
        private void DrawWord(string word, Color color, Rectangle border, FontFamily family, Graphics graphics)
        {
            var brush = new SolidBrush(color);
            var size = PickUpFontSize(word, family, border);
            var font = new Font(family, size);
            graphics.DrawString(word, font, brush, border);
        }

        public Bitmap Visualize<T>(Cloud<T> coloredCloud, VisualizerSettings settings)
        {
            var image = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                graphics.Clear(settings.BackgroundColor);
                foreach (var element in coloredCloud.Elements)
                {
                    var color = element.Color;
                    DrawWord(element.Content.ToString(), color, element.Border, settings.Font, graphics);
                }

                graphics.Save();
            }
            return image;
        }

        private int PickUpFontSize(string text, FontFamily family, Rectangle border)
        {
            var fits = false;
            var size = border.Width;
            using (var image = new Bitmap(1, 1))
            {
                using (var g = Graphics.FromImage(image))
                {
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    while (!fits)
                    {
                        var font = new Font(family, size);
                        var stringSize = g.MeasureString(text, font);
                        fits = stringSize.Width < border.Width;
                        size -= 1;
                    }
                }
            }
            return size;
        }
    }
}
