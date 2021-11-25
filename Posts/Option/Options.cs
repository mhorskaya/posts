using CommandLine;

namespace Posts.Option
{
    public class Options : IOptions
    {
        [Option('q', "query", Required = true, HelpText = "The query text (all | <comma_separated_numbers>)")]
        public string Query { get; set; }

        [Option('o', "output", HelpText = "Writes output to file if specified.")]
        public string OutputFilePath { get; set; }

        [Option('f', "format", Default = false, HelpText = "Formats JSON output.")]
        public bool Format { get; set; }
    }
}