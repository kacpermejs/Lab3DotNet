using System;
using System.Threading;
namespace ConsoleApp1
{
    class Program
    {
        /*static void Main(string[] args)
        {
            int n = 10;
            Thread[] threads = new Thread[n];
            for (int i = 0; i < n; i++)
            {
                threads[i] = new Thread(Welcome);
                threads[i].Name = String.Format("Thread: {0}", i);
            }
            foreach (Thread x in threads)
                x.Start();
            ;
            Console.Read();
        }*/
        /*static void Main(string[] args)
        {
            int n = 10;
            Test t = new Test();
            Thread[] threads = new Thread[n];
            for (int i = 0; i < n; i++)
            {
                threads[i] = new Thread(t.Welcome);
                threads[i].Name = String.Format("Thread: {0}", i);
            }
            foreach (Thread x in threads)
                x.Start();
            Console.Read();
        }
        public class Test
        {
            public void Welcome()
            {
                lock (this)
                {
                    Console.WriteLine("Hallo " + Thread.CurrentThread.Name + "!");
                    for (int i = 0; i < 10; i++)
                        Console.Write(i + " ");
                    Console.WriteLine("");
                }
            }
        }
*/
        static void Main(string[] args)
        {
            int n = 10;
            Test test = new Test();
            var watch = System.Diagnostics.Stopwatch.StartNew();
            Thread[] threads = new Thread[n];
            for (int i = 0; i < n; i++)
            {
                var temp = i;
                threads[i] = new Thread(() => test.set(temp));
            }
            for (int i = 0; i < n; i++)
            {
                threads[i].Start();
            }
            for (int i = 0; i < n; i++)
                threads[i].Join();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(elapsedMs + " ms");
            foreach (double x in test.array)
                Console.WriteLine(x + " ");
            Console.Read();
        }
        public class Test
        {
            public double[] array = new double[10];
            public void set(int i)
            {
                Thread.Sleep(500);
                double x = Math.Pow((int)i, 2.0f);
                array[(int)i] = x;
            }
        }
        static void Welcome()
        {
            Console.WriteLine("Hallo " + Thread.CurrentThread.Name + "!");
        }
    }
}
