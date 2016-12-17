using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudApp
{
    public interface ICloudLayouter
    {
        Cloud<T> CreateCloud<T>(Dictionary<T, Size> elements, Size size);
    }
}
