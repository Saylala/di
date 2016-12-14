using System.Collections.Generic;

namespace TagsCloudApp
{
	public interface IFileReader
	{
		IEnumerable<string> ReadFile(string filename);
	}
}
