namespace Posts.Logger
{
    internal interface ILogger
    {
        void Info(string message);

        void Error(string message);
    }
}