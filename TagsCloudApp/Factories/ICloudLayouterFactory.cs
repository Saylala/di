using System.Drawing;

namespace TagsCloudApp
{
	public interface ICloudLayouterFactory
	{
		CloudLayouter Create(Point center);
	}
}
