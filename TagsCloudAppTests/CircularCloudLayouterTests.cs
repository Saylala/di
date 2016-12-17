using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using NUnit.Framework;
using TagsCloudApp;
using TagsCloudApp.Factories;

namespace TagsCloudAppTests
{
    [TestFixture]
    class CircularCloudLayouterTests
    {
        private readonly Size size = new Size(1000, 1000);
        private CircularCloudLayouter circularCloudLayouter;

        [SetUp]
        public void SetUp()
        {
            var factory = new SpiralFactory();
            circularCloudLayouter = new CircularCloudLayouter(factory);
        }

        [TestCase(10, 10)]
        [TestCase(100, 50)]
        [TestCase(20, 30)]
        [TestCase(500, 600)]
        public void FirstRectangle_PlacedInsideCloud_IsPlacedInCenter(int width, int height)
        {
            var elements = new Dictionary<int, Size> { { 0, new Size(width, height) } };

            var cloud = circularCloudLayouter.CreateCloud(elements, size);

            Assert.AreEqual(1, cloud.Elements.Count);
            Assert.AreEqual(new Point(500, 500), cloud.Elements.First().Border.GetCenter());
        }

        [TestCase(1001, 1)]
        [TestCase(100, 1050)]
        [TestCase(20, 3000)]
        [TestCase(5000, 600)]
        [TestCase(10000, 10000)]
        public void Layouter_TryPlaceRectangleBiggerThanBorder_ReturnsEmptyRectangle(int width, int height)
        {
            var elements = new Dictionary<int, Size> { { 0, new Size(width, height) } };

            Assert.Throws<ArgumentException>(() => circularCloudLayouter.CreateCloud(elements, size));
        }


        [TestCase(12, 11)]
        [TestCase(130, 18)]
        [TestCase(27, 38)]
        [TestCase(300, 1)]
        [TestCase(280, 300)]
        public void TwoRectanlges_PlacedInsideCloud_DoNotIntersect(int width, int height)
        {
            var elements = new Dictionary<int, Size>
            {
                { 0, new Size(width, height) },
                { 1, new Size(width, height) }
            };

            var cloud = circularCloudLayouter.CreateCloud(elements, size);
            var firstRectangle = cloud.Elements[0].Border;
            var secondRectangle = cloud.Elements[1].Border;

            Assert.False(firstRectangle.IntersectsWith(secondRectangle));
        }

        [TestCase(5, 4)]
        [TestCase(125, 75)]
        [TestCase(20, 40)]
        [TestCase(400, 300)]
        public void TwoRectangles_PlacedInsideCloud_DoNotHaveSameCenters(int width, int height)
        {

            var elements = new Dictionary<int, Size>
            {
                { 0, new Size(width, height) },
                { 1, new Size(width, height) }
            };

            var cloud = circularCloudLayouter.CreateCloud(elements, size);
            var firstRectangle = cloud.Elements[0].Border;
            var secondRectangle = cloud.Elements[1].Border;

            Assert.False(firstRectangle.GetCenter().Equals(secondRectangle.GetCenter()));
        }

        [TestCase(1, 1, 8)]
        [TestCase(200, 51, 6)]
        [TestCase(278, 3, 17)]
        [TestCase(12, 40, 38)]
        [TestCase(55, 60, 87)]
        public void Rectangles_PlacedInsideCloud_DoNotIntersectWithEachOther(int width, int height, int count)
        {
            var elements = CreateLayout(count, width, height);
            var cloud = circularCloudLayouter.CreateCloud(elements, size);

            foreach (var pair in cloud.Elements)
            {
                var otherPairs = cloud.Elements.Where(x => !x.Equals(pair));
                foreach (var otherPair in otherPairs)
                    Assert.IsFalse(pair.Border.IntersectsWith(otherPair.Border));
            }
        }

        [TestCase(1, 1, 1)]
        [TestCase(5, 227, 155)]
        [TestCase(10, 100, 70)]
        [TestCase(50, 50, 20)]
        [TestCase(100, 20, 25)]
        [TestCase(250, 12, 9)]
        [TestCase(500, 3, 4)]
        [TestCase(1000, 1, 2)]
        public void Cloud_FormedWithSameRectangles_IsCircleWithTolerance(int rectanglesCount, int width, int height)
        {
            var elements = CreateLayout(rectanglesCount, width, height);
            const double spacingCoefficent = 1.5;
            const double tolerance = 0.15;

            var cloud = circularCloudLayouter.CreateCloud(elements, size);

            var rectangleArray = new Rectangle[rectanglesCount];

            for (var i = 0; i < rectanglesCount; i++)
                rectangleArray[i] = cloud.Elements[i].Border;

            var outerRectanglesCount = GetRectanglesOutsideCircle(rectangleArray, spacingCoefficent);
            var outerRectanglesCoefficent = outerRectanglesCount / rectangleArray.Length;

            Assert.Less(outerRectanglesCoefficent, tolerance);
        }

        [TestCase(1, 1, 1)]
        [TestCase(15, 127, 155)]
        [TestCase(10, 100, 70)]
        [TestCase(50, 50, 20)]
        [TestCase(100, 20, 25)]
        [TestCase(250, 12, 9)]
        [TestCase(500, 3, 4)]
        [TestCase(1000, 1, 2)]
        public void Cloud_FormedWithDifferentRectangles_IsCircleWithTolerance(int rectanglesCount, int maxWidth, int maxHeight)
        {
            var elements = CreateLayoutWithRandomSizes(rectanglesCount, maxWidth, maxHeight);
            const double spacingCoefficent = 1.5;
            const double tolerance = 0.15;

            var cloud = circularCloudLayouter.CreateCloud(elements, size);

            var rectangleArray = new Rectangle[rectanglesCount];

            for (var i = 0; i < rectanglesCount; i++)
                rectangleArray[i] = cloud.Elements[i].Border;

            var outerRectanglesCount = GetRectanglesOutsideCircle(rectangleArray, spacingCoefficent);
            var outerRectanglesCoefficent = outerRectanglesCount / rectangleArray.Length;

            Assert.Less(outerRectanglesCoefficent, tolerance);
        }

        [Test]
        [Repeat(100)]
        public void Cloud_FormedWithDifferentRectangles_IsCircleWithTolerance_Repeating()
        {
            const int rectanglesCount = 50;
            const int maxWidth = 50;
            const int maxHeight = 50;
            Cloud_FormedWithDifferentRectangles_IsCircleWithTolerance(rectanglesCount, maxWidth, maxHeight);
        }

        private Dictionary<int, Size> CreateLayout(int count, int width, int height)
        {
            var layout = new Dictionary<int, Size>();
            for (var i = 0; i < count; i++)
                layout[i] = new Size(width, height);
            return layout;
        }

        private Dictionary<int, Size> CreateLayoutWithRandomSizes(int count, int maxWidth, int maxHeight)
        {
            var layout = new Dictionary<int, Size>();
            const int minValue = 1;
            var rnd = new Random();
            for (var i = 0; i < count; i++)
                layout[i] = new Size(rnd.Next(minValue, maxWidth), rnd.Next(minValue, maxHeight));
            return layout;
        }

        private static bool IsInsideCircle(Point point, Point circleCenter, int radius)
        {
            var distance = Math.Sqrt(Math.Pow(point.X - circleCenter.X, 2) + Math.Pow(point.Y - circleCenter.Y, 2));
            return distance < radius;
        }

        private static double GetRectanglesOutsideCircle(Rectangle[] rectangles, double spacingCoefficent)
        {
            var rectanglesArea = rectangles.Sum(x => x.Width * x.Height);
            var circleArea = rectanglesArea * spacingCoefficent;
            var radius = (int)Math.Ceiling(Math.Sqrt(circleArea / Math.PI));
            var circleCenter = rectangles[0].GetCenter();
            double outerRectanglesCount = rectangles.Count(x => !IsInsideCircle(x.GetCenter(), circleCenter, radius));
            return outerRectanglesCount;
        }
    }
}
