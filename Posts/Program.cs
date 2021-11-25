using System;
using System.Net.Http;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Posts.Configuration;
using Posts.Consume;
using Posts.Filter;
using Posts.Logging;
using Posts.Model;
using Posts.Option;
using Posts.Process;
using Posts.Write;

namespace Posts
{
    public class Program
    {
        private const int InvalidCommandLineArgumentExitCode = -1;

        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args).WithParsed(options =>
            {
                IQueryOptions queryOptions;

                try
                {
                    queryOptions = QueryOptions.GetQueryOptions(options);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    Environment.ExitCode = InvalidCommandLineArgumentExitCode;
                    return;
                }

                BuildServiceProvider(options, queryOptions).GetService<Processor>()?.ProcessAsync().GetAwaiter().GetResult();
            });
        }

        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder().AddJsonFile("appSettings.json", false).Build();
        }

        private static IServiceProvider BuildServiceProvider(IOptions options, IQueryOptions queryOptions)
        {
            IServiceCollection collection = new ServiceCollection();

            collection.AddSingleton(options);
            collection.AddSingleton(queryOptions);
            collection.AddSingleton<IConfig>(BuildConfiguration().Get<Config>());
            collection.AddTransient<ILogger, Logger>();
            collection.AddTransient<IConsumer<Post>, Consumer>();
            collection.AddTransient<IFilterer<Post>, Filterer>();
            collection.AddSingleton<HttpClient>();
            collection.AddSingleton<Processor>();

            if (string.IsNullOrEmpty(options.OutputFilePath))
            {
                collection.AddSingleton<IWriter<string>, ConsoleWriter>();
            }
            else
            {
                collection.AddSingleton<IWriter<string>, FileWriter>();
            }

            return collection.BuildServiceProvider();
        }
    }
}