using Newtonsoft.Json;
using Posts.Consumer;
using Posts.Filterer;
using Posts.Logger;
using Posts.Models;
using Posts.Options;
using Posts.Writer;
using System.Threading.Tasks;

namespace Posts.Processor
{
    internal class Processor
    {
        private readonly IConsumer<Post> _consumer;
        private readonly IFilterer<Post> _filterer;
        private readonly IWriter<string> _writer;
        private readonly ILogger _logger;
        private readonly IOptions _options;
        private readonly IQueryOptions _queryOptions;

        public Processor(IConsumer<Post> consumer, IFilterer<Post> filterer, IWriter<string> writer, ILogger logger, IOptions options, IQueryOptions queryOptions)
        {
            _consumer = consumer;
            _filterer = filterer;
            _writer = writer;
            _logger = logger;
            _options = options;
            _queryOptions = queryOptions;
        }

        public async Task ProcessAsync()
        {
            _logger.Info("ConsumeAsync() call has started.");
            var posts = await _consumer.ConsumeAsync();
            _logger.Info("ConsumeAsync() call has finished.");

            if (!_queryOptions.QueryAll)
            {
                posts = _filterer.Filter(posts, _queryOptions.Ids);
                _logger.Info($"Query filter has been applied with ids: {string.Join(",", _queryOptions.Ids)}");
            }

            var jsonResult = JsonConvert.SerializeObject(posts, _options.Format ? Formatting.Indented : Formatting.None);
            _writer.Write(jsonResult);
            _logger.Info("Results have been written.");
        }
    }
}