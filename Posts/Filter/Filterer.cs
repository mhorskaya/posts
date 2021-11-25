using System.Collections.Generic;
using System.Linq;
using Posts.Model;

namespace Posts.Filter
{
    public class Filterer : IFilterer<Post>
    {
        public IEnumerable<Post> Filter(IEnumerable<Post> data, IEnumerable<int> ids)
        {
            return data.Where(x => ids.Contains(x.Id));
        }
    }
}