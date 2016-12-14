using System.Drawing;

namespace TagsCloudApp.Factories
{
	public class SpiralFactory: ICurveFactory
	{
		public ICurve Create(Point startPoint)
		{
			return new Spiral(startPoint);
		}
	}
}
