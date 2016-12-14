using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudApp
{
	public interface IColorGiver
	{
		Dictionary<string, Color> GiveColors(Dictionary<string, int> words);
	}
}
