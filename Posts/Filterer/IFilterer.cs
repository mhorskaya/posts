using System.Collections.Generic;

namespace Posts.Filterer
{
    public interface IFilterer<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> data, IEnumerable<int> ids);
    }
}