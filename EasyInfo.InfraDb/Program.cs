using System;
using System.Diagnostics;

namespace EasyInfo.InfraDb
{
    class Program
    {
        static void Main(string[] args)
        {
            var stop = new Stopwatch { };
            stop.Start();
            Defrag.Execute();
            stop.Stop();
            Console.WriteLine(stop.Elapsed);
            var ds = Defrag.Config;
            Console.Read();
        }
    }
}