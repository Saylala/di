using System.Collections.Generic;
using System.IO;

namespace TagsCloudApp
{
	public class FileReader : IFileReader
	{
		public IEnumerable<string> ReadFile(string filename)
		{
			return File.ReadLines(filename);
		}
	}
}
