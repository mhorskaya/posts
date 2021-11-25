using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posts.Consume
{
    public interface IConsumer<T>
    {
        Task<IEnumerable<T>> ConsumeAsync();
    }
}