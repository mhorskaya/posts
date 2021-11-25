using Posts.Models;
using System.Collections.Generic;
using System.Linq;

namespace Posts.Filterer
{
    public class Filterer : IFilterer<Post>
    {
        public IEnumerable<Post> Filter(IEnumerable<Post> data, IEnumerable<int> ids)
        {
            return data.Where(x => ids.Contains(x.Id));
        }
    }
}