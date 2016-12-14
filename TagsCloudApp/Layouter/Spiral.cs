using System;
using System.Drawing;


namespace TagsCloudApp
{
	public class Spiral : ICurve
    {
        private const double Step = 0.01;
        private const double Frequency = 50;
        private readonly Point startPoint;
        private int currentNumber;
        public Spiral(Point startPoint)
        {
            this.startPoint = startPoint;
            currentNumber = 1;
        }

        public Point GetNextPoint()
        {
            var phi = Frequency * currentNumber;
            var r = Step * currentNumber;
            var x = r * Math.Cos(phi);
            var y = r * Math.Sin(phi);
            currentNumber++;
            return new Point(startPoint.X + (int)x, startPoint.Y + (int)y);
        }
    }
}
