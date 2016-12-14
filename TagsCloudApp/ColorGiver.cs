using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudApp
{
	public class ColorGiver : IColorGiver
	{
		public Dictionary<string, Color> GiveColors(Dictionary<string, int> words)
		{
			var colorsDictionary = new Dictionary<string, Color>();
			foreach (var pair in words)
				colorsDictionary[pair.Key] = Color.Aqua;
			return colorsDictionary;
		}
	}
}
