using System.Collections.Generic;

namespace TagsCloudApp
{
    public interface IInputStream
    {
        IEnumerable<string> GetData();
    }
}
