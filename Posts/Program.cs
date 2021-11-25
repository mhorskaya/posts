using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Posts.Config;
using Posts.Consumer;
using Posts.Filterer;
using Posts.Logger;
using Posts.Models;
using Posts.Options;
using Posts.Writer;
using System;
using System.Net.Http;

namespace Posts
{
    internal class Program
    {
        private const int InvalidCommandLineArgumentExitCode = -1;

        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options.Options>(args).WithParsed(options =>
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

                BuildServiceProvider(options, queryOptions).GetService<Processor.Processor>()?.ProcessAsync().GetAwaiter().GetResult();
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
            collection.AddSingleton<IConfig>(BuildConfiguration().Get<Config.Config>());
            collection.AddTransient<ILogger, Logger.Logger>();
            collection.AddTransient<IConsumer<Post>, Consumer.Consumer>();
            collection.AddTransient<IFilterer<Post>, Filterer.Filterer>();
            collection.AddSingleton<HttpClient>();
            collection.AddSingleton<Processor.Processor>();

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