using System.Drawing;

namespace TagsCloudApp
{
	public class VisulizerSettingsFactory : IVisulizerSettingsFactory
	{
		public VisualizerSettings Create(Color backgroundColor, Size imageSize, FontFamily font)
		{
			return new VisualizerSettings(backgroundColor, imageSize, font);
		}
	}
}
