using System.Drawing;

namespace TagsCloudApp
{
	public interface IVisulizerSettingsFactory
	{
		VisualizerSettings Create(Color backgroundColor, Size imageSize, FontFamily font);
	}
}
