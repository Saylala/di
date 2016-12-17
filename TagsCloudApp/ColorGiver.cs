using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudApp
{
    public class ColorGiver : IColorGiver
    {
        private readonly List<Color> colors;
        private readonly Random rnd;

        public ColorGiver(List<Color> colors)
        {
            this.colors = colors;
            rnd = new Random();
        }

        public Cloud<T> GiveColors<T>(Cloud<T> cloud)
        {
            var elements = cloud.Elements;
            var coloredElements = new Dictionary<T, Color>();
            foreach (var element in elements)
                coloredElements[element.Key] = GetRandomColor();
            return cloud.PaintCloud(coloredElements);
        }

        private Color GetRandomColor()
        {
            var number = rnd.Next(0, colors.Count);
            return colors[number];
        }
    }
}
