using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeFormatter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime? agoraUtc = DateTime.UtcNow;
            Console.WriteLine($"Agora Sem Culture - {agoraUtc?.ToString("yyyy-MM-dd, hh:mm:ss tt")}");
            Console.WriteLine($"Agora Com Culture - {agoraUtc?.ToString("yyyy-MM-dd, hh:mm:ss tt", new System.Globalization.CultureInfo("en-US"))}");
            Console.WriteLine($"Agora Com Invariant - {agoraUtc?.ToString("yyyy-MM-dd, hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture)}");

            Console.ReadLine();
        }
    }
}
