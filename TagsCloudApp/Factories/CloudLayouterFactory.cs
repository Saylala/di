using System.Drawing;
using TagsCloudApp.Factories;

namespace TagsCloudApp
{
	public class CloudLayouterFactory : ICloudLayouterFactory
	{
		private readonly ICurveFactory factory;

		public CloudLayouterFactory(ICurveFactory factory)
		{
			this.factory = factory;
		}
		public CloudLayouter Create(Point center)
		{
			return new CloudLayouter(center, factory);
		}
	}
}
