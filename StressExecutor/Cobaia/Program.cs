using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Cobaia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var i = 0;
            while (true)
            {
                Console.WriteLine($"running {i++}");
                Thread.Sleep(1000);
            }

        }
    }
}
