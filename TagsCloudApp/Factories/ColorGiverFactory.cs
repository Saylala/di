using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudApp.Factories
{
    public class ColorGiverFactory : IColorGiverFactory
    {
        public IColorGiver Create(Options args)
        {
            var colors = GetColors(args);
            return new ColorGiver(colors);
        }

        private List<Color> GetColors(Options args)
        {
            if (args.Colors == null)
                return new List<Color> { Color.Aqua };
            return args.Colors.Select(Color.FromName).ToList();
        }
    }
}
