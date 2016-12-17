using System.Drawing;

namespace TagsCloudApp
{
    public interface IVisualizerSettingsFactory
    {
        VisualizerSettings Create(Color backgroundColor, Size imageSize, FontFamily font);
    }
}
