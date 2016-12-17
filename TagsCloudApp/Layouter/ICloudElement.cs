using System.Drawing;

namespace TagsCloudApp
{
    public interface ICloudElement<T>
    {
        Rectangle Border { get; }
        T Content { get; }
        Color Color { get; }
    }
}
