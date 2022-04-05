using System;
using System.Threading;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(Welcome);
            thread.Start();
            Console.Read();
        }
        static void Welcome()
        {
            Console.WriteLine("Hallo!");
        }
    }
}
