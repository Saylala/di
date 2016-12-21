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

        public Result<Cloud<T>> CreateCloud<T>(Dictionary<T, Size> elements, Size size)
        {
            var placedElements = new List<ICloudElement<T>>();
            var placedRectangles = new List<Rectangle>();
            var border = new Rectangle(new Point(0, 0), size);
            var curve = factory.Create(GetCenter(border));
            foreach (var element in elements)
            {
                var result = PutNextRectangle(curve, border, element.Value, placedRectangles);
                if (!result.IsSuccess)
                    return Result.Fail<Cloud<T>>(result.Error);
                var rectangle = result.Value;
                placedRectangles.Add(rectangle);
                placedElements.Add(new CloudElement<T>(rectangle, element.Key));
            }
            return Result.Ok(new Cloud<T>(placedElements));
        }

        private Result<Rectangle> PutNextRectangle(ICurve curve, Rectangle border, Size rectangleSize, List<Rectangle> placedRectangles)
        {
            var result = GetNextPoint(curve, border, rectangleSize, placedRectangles);
            if (!result.IsSuccess)
                return Result.Fail<Rectangle>(result.Error);
            var currentPoint = result.Value;
            var upperLeftPoint = new Point(currentPoint.X - rectangleSize.Width / 2,
                currentPoint.Y - rectangleSize.Height / 2);
            var rectangle = new Rectangle(upperLeftPoint, rectangleSize);
            return Result.Ok(rectangle);
        }

        private bool CanPutRectangle(Point center, Size rectangleSize, List<Rectangle> placedRectangles, Rectangle border)
        {
            var upperLeftPoint = new Point(center.X - rectangleSize.Width / 2, center.Y - rectangleSize.Height / 2);
            var rectangle = new Rectangle(upperLeftPoint, rectangleSize);
            return placedRectangles.All(x => !x.IntersectsWith(rectangle)) && rectangle.IsInside(border);
        }

        private Result<Point> GetNextPoint(ICurve curve, Rectangle border, Size rectangleSize, List<Rectangle> placedRectangles)
        {
            var point = GetCenter(border);
            while (!CanPutRectangle(point, rectangleSize, placedRectangles, border))
            {
                point = curve.GetNextPoint();
                if (!point.IsInside(border))
                    return Result.Fail<Point>("Cannot put rectangle of this size");
            }
            return Result.Ok(point);
        }

        private Point GetCenter(Rectangle border)
        {
            var x = border.X + border.Width / 2;
            var y = border.Y + border.Height / 2;
            return new Point(x, y);
        }
    }
}
