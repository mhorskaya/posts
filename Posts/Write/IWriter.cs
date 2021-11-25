namespace Posts.Write
{
    public interface IWriter<T>
    {
        void Write(T data);
    }
}