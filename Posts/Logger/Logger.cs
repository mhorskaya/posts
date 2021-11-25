using System;

namespace Posts.Logger
{
    internal class Logger : ILogger
    {
        public void Info(string message)
        {
            Console.WriteLine($"[{DateTime.Now:HH.mm.ss.fff}] [INFO] {message}");
        }

        public void Error(string message)
        {
            Console.WriteLine($"[{DateTime.Now:HH.mm.ss.fff}] [ERROR] {message}");
        }
    }
}