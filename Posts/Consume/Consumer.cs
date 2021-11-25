using Newtonsoft.Json;
using Posts.Configuration;
using Posts.Model;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Posts.Consume
{
    public class Consumer : IConsumer<Post>
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
            var response = await _client.GetAsync(_config.EndpointUrl);
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Post>>(body);
        }
    }
}