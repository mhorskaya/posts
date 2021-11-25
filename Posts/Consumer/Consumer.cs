using Newtonsoft.Json;
using Posts.Config;
using Posts.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Posts.Consumer
{
    internal class Consumer : IConsumer<Post>
    {
        private readonly HttpClient _client;
        private readonly IConfig _config;

        public Consumer(HttpClient client, IConfig config)
        {
            _client = client;
            _config = config;
        }

        public async Task<IEnumerable<Post>> ConsumeAsync()
        {
            var response = await _client.GetStringAsync(_config.EndpointUrl);

            return JsonConvert.DeserializeObject<IEnumerable<Post>>(response);
        }
    }
}