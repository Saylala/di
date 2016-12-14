using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloudApp
{
	class FileSaver : IFileSaver
	{
		public void Save(Image image, string path)
		{
			image.Save(path, ImageFormat.Png);
		}
	}
}
