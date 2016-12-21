using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudApp
{
    public interface ICloudLayouter
    {
        Result<Cloud<T>> CreateCloud<T>(Dictionary<T, Size> elements, Size size);
    }
}
