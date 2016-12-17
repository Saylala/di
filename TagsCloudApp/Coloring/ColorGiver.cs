using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudApp.Layouter;

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
            var coloredElements = new List<ICloudElement<T>>();
            foreach (var element in elements)
            {
                var coloredElement = new CloudElement<T>(element.Border, element.Content, GetRandomColor());
                coloredElements.Add(coloredElement);
            }
            return new Cloud<T>(coloredElements);
        }

        private Color GetRandomColor()
        {
            var number = rnd.Next(0, colors.Count);
            return colors[number];
        }
    }
}
