using System.Collections.Generic;
using System.Collections.Immutable;

namespace TagsCloudApp
{
    public class Cloud<T>
    {
        public ImmutableList<ICloudElement<T>> Elements;
        public Cloud(List<ICloudElement<T>> elements)
        {
            Elements = elements.ToImmutableList();
        }
    }
}
