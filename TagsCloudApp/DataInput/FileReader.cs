using System.Collections.Generic;
using System.IO;

namespace TagsCloudApp
{
    public class FileReader : IInputStream
    {
        private readonly string filename;

        public FileReader(string filename)
        {
            this.filename = filename;
        }

        public Result<IEnumerable<string>> GetData()
        {
            return Result.Of(() => File.ReadLines(filename));
        }
    }
}
