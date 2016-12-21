using System.Collections.Generic;

namespace TagsCloudApp
{
    public interface IInputStream
    {
        Result<IEnumerable<string>> GetData();
    }
}
