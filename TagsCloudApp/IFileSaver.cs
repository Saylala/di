using System.Drawing;

namespace TagsCloudApp
{
	public interface IFileSaver
	{
		void Save(Image image, string path);
	}
}
