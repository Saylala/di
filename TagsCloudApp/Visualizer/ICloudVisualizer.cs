using System.Drawing;

namespace TagsCloudApp
{
    public interface ICloudVisualizer
    {
        Bitmap Visualize<T>(Cloud<T> coloredCloud, VisualizerSettings settings);
    }
}
