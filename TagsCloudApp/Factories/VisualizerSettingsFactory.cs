using System.Drawing;

namespace TagsCloudApp
{
    public class VisualizerSettingsFactory : IVisualizerSettingsFactory
    {
        public VisualizerSettings Create(Color backgroundColor, Size imageSize, FontFamily font)
        {
            return new VisualizerSettings(backgroundColor, imageSize, font);
        }
    }
}
