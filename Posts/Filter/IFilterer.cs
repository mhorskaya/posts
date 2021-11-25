using System.Collections.Generic;

namespace Posts.Filter
{
    public interface IFilterer<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> data, IEnumerable<int> ids);
    }
}