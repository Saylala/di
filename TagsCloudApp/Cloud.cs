using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;

namespace TagsCloudApp
{
    public class Cloud<T>
    {
        public ImmutableDictionary<T, Rectangle> Elements { get; private set; }
        public ImmutableDictionary<T, Color> Colors { get; private set; }

        private Cloud()
        {

        }

        public Cloud(Dictionary<T, Rectangle> elements)
        {
            Elements = elements.ToImmutableDictionary();
        }

        public Cloud<T> PaintCloud(Dictionary<T, Color> colors)
        {
            return new Cloud<T>
            {
                Elements = Elements,
                Colors = colors.ToImmutableDictionary()
            };
        }
    }
}
