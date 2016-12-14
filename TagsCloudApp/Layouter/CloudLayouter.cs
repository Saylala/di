﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using TagsCloudApp.Factories;

namespace TagsCloudApp
{
	public class CloudLayouter
    {
        public Point Center { get; }
        private readonly ICurve curve;
        public Rectangle CloudBorder { get; }
        private readonly List<Rectangle> rectangles = new List<Rectangle>();
        public CloudLayouter(Point center, ICurveFactory factory)
        {
            Center = center;
            curve = factory.Create(center);

            var width = Math.Abs(Center.X) * 2;
            var height = Math.Abs(Center.Y) * 2;

            CloudBorder = new Rectangle(new Point(0, 0), new Size(width, height));
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var currentPoint = GetNextPoint(rectangleSize);
            var upperLeftPoint = new Point(currentPoint.X - rectangleSize.Width / 2,
                currentPoint.Y - rectangleSize.Height / 2);
            var rectangle = new Rectangle(upperLeftPoint, rectangleSize);
            rectangles.Add(rectangle);
            return rectangle;
        }

        private bool CanPutRectangle(Point center, Size rectangleSize)
        {
            var upperLeftPoint = new Point(center.X - rectangleSize.Width / 2, center.Y - rectangleSize.Height / 2);
            var rectangle = new Rectangle(upperLeftPoint, rectangleSize);
            return rectangles.All(x => !x.IntersectsWith(rectangle)) && rectangle.IsInside(CloudBorder);
        }

        private Point GetNextPoint(Size rectangleSize)
        {
            var point = Center;
            while (!CanPutRectangle(point, rectangleSize))
            {
                point = curve.GetNextPoint();
                if (!point.IsInside(CloudBorder))
                    throw new ArgumentException("Cannot put rectangle of this size");
            }
            return point;
        }

        public Rectangle[] GetRectangles()
        {
            return rectangles.ToArray();
        }
    }
}
