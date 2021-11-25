using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posts.Consumer
{
    internal interface IConsumer<T>
    {
        Task<IEnumerable<T>> ConsumeAsync();
    }
}