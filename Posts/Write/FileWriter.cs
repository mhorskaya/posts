using System;
using System.IO;
using Posts.Logging;
using Posts.Option;

namespace Posts.Write
{
    public class FileWriter : IWriter<string>
    {
        private readonly IOptions _options;
        private readonly ILogger _logger;

        public FileWriter(IOptions options, ILogger logger)
        {
            _options = options;
            _logger = logger;
        }

        public void Write(string data)
        {
            try
            {
                File.WriteAllText(_options.OutputFilePath, data);
            }
            catch (UnauthorizedAccessException e)
            {
                _logger.Error(e.Message);
            }
        }
    }
}