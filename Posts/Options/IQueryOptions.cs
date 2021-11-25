using System.Collections.Generic;

namespace Posts.Options
{
    internal interface IQueryOptions
    {
        bool QueryAll { get; }
        IEnumerable<int> Ids { get; }
    }
}