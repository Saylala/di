using System.Drawing;

namespace TagsCloudApp
{
    public static class PointExtensions
    {
        public static bool IsInside(this Point point, Rectangle rectangle)
        {
            return rectangle.Left < point.X && point.X < rectangle.Right &&
                   rectangle.Top < point.Y && point.Y < rectangle.Bottom;
        }
    }
}
