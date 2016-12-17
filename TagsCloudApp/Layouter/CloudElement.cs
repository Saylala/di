using System.Drawing;

namespace TagsCloudApp.Layouter
{
    class CloudElement<T> : ICloudElement<T>
    {
        public Rectangle Border { get; }
        public Color Color { get; }

        public T Content { get; }

        public CloudElement(Rectangle rectangle, T content)
        {
            Border = rectangle;
            Content = content;
        }

        public CloudElement(Rectangle rectangle, T content, Color color)
        {
            Border = rectangle;
            Content = content;
            Color = color;
        }
    }
}
