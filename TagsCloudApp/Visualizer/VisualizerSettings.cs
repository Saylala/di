using System.Drawing;

namespace TagsCloudApp
{
    public class VisualizerSettings
    {
        public readonly Color BackgroundColor;
        public readonly Size ImageSize;
        public readonly FontFamily Font;

        public VisualizerSettings(Color backgroundColor, Size imageSize, FontFamily font)
        {
            BackgroundColor = backgroundColor;
            ImageSize = imageSize;
            Font = font;
        }
    }
}
