using System.Drawing;

namespace TagsCloudApp.Factories
{
    public interface ICurveFactory
    {
        ICurve Create(Point startPoint);
    }
}
