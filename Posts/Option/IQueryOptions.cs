using System.Collections.Generic;

namespace Posts.Option
{
    public interface IQueryOptions
    {
        bool QueryAll { get; }
        IEnumerable<int> Ids { get; }
    }
}