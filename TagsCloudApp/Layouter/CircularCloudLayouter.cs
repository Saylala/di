using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using TagsCloudApp.Factories;
using TagsCloudApp.Layouter;

namespace TagsCloudApp
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly ICurveFactory factory;
        public CircularCloudLayouter(ICurveFactory factory)
        {
            this.factory = factory;
        }

        public Cloud<T> CreateCloud<T>(Dictionary<T, Size> elements, Size size)
        {
            var placedElements = new List<ICloudElement<T>>();
            var placedRectangles = new List<Rectangle>();
            var border = new Rectangle(new Point(0, 0), size);
            var curve = factory.Create(getCenter(border));
            foreach (var element in elements)
            {
                var rectangle = PutNextRectangle(curve, border, element.Value, placedRectangles);
                placedRectangles.Add(rectangle);
                placedElements.Add(new CloudElement<T>(rectangle, element.Key));
            }
            return new Cloud<T>(placedElements);
        }

        private Rectangle PutNextRectangle(ICurve curve, Rectangle border, Size rectangleSize, List<Rectangle> placedRectangles)
        {
            var currentPoint = GetNextPoint(curve, border, rectangleSize, placedRectangles);
            var upperLeftPoint = new Point(currentPoint.X - rectangleSize.Width / 2,
                currentPoint.Y - rectangleSize.Height / 2);
            var rectangle = new Rectangle(upperLeftPoint, rectangleSize);
            return rectangle;
        }

        private bool CanPutRectangle(Point center, Size rectangleSize, List<Rectangle> placedRectangles, Rectangle border)
        {
            var upperLeftPoint = new Point(center.X - rectangleSize.Width / 2, center.Y - rectangleSize.Height / 2);
            var rectangle = new Rectangle(upperLeftPoint, rectangleSize);
            return placedRectangles.All(x => !x.IntersectsWith(rectangle)) && rectangle.IsInside(border);
        }

        private Point GetNextPoint(ICurve curve, Rectangle border, Size rectangleSize, List<Rectangle> placedRectangles)
        {
            var point = getCenter(border);
            while (!CanPutRectangle(point, rectangleSize, placedRectangles, border))
            {
                point = curve.GetNextPoint();
                if (!point.IsInside(border))
                    throw new ArgumentException("Cannot put rectangle of this size");
            }
            return point;
        }

        private Point getCenter(Rectangle border)
        {
            var x = border.X + border.Width / 2;
            var y = border.Y + border.Height / 2;
            return new Point(x, y);
        }
    }
}
