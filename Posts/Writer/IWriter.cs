namespace Posts.Writer
{
    internal interface IWriter<T>
    {
        void Write(T data);
    }
}