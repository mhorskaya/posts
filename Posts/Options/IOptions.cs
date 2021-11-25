namespace Posts.Options
{
    internal interface IOptions
    {
        string Query { get; }
        string OutputFilePath { get; }
        bool Format { get; }
    }
}