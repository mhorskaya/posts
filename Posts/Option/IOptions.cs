namespace Posts.Option
{
    public interface IOptions
    {
        string Query { get; }
        string OutputFilePath { get; }
        bool Format { get; }
    }
}