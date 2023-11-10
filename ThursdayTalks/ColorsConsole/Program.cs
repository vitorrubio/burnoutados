using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ColorsConsole
{
    class Package
    {
        public string Company { get; set; }
        public double Weight { get; set; }
        public long TrackingNumber { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //string cor = "255,0,0,1";

            //Color color = Color.FromArgb(255, 255, 0, 0);
            //Color color2 = Color.FromKnownColor(KnownColor.Red);
            //Color color3 = Color.FromName("red");


            //Console.WriteLine(color.Name); // ffff0000

            //Console.WriteLine($"As cores são iguais? {color.Equals(color2)}");
            //Console.WriteLine($"As cores são iguais? {color3.Equals(color2)}");

            //Console.WriteLine($"As cores tem o valor {color.ToArgb()} e são iguais? {color.ToArgb().Equals(color2.ToArgb())}");
            //Console.WriteLine($"As cores tem o valor {color3.ToArgb()} e são iguais? {color3.ToArgb().Equals(color2.ToArgb())}");

            //var colorLookup = Enum.GetValues(typeof(KnownColor))
            //    .Cast<KnownColor>()
            //    .Select(Color.FromKnownColor)
            //    .ToLookup(c => c.ToArgb());


            //foreach (var namedColor in colorLookup[color.ToArgb()])
            //{
            //    Console.WriteLine(namedColor.Name);
            //}

            //Console.WriteLine(colorLookup[color.ToArgb()].FirstOrDefault().Name);


            //var coresComMaisNomes = colorLookup.Where(x => x.Count() > 1).ToList();

            //coresComMaisNomes.ForEach(x =>
            //{
            //    Console.WriteLine($"Cor indice {x.Key}");
            //    Console.WriteLine($"-----------------------------");
            //    x.ToList().ForEach(y =>
            //    {
            //        Console.WriteLine($"{y.Name} ==> A{y.A}|R{y.R}|G{y.G}|B{y.B}");
            //    });
            //});

            //ToLookupEx1();

            //192.168.0.1
            //IPAddress addr = new IPAddress(new byte[] { 192, 168, 0, 1 });
            //var enderecoBase = addr.Address & 0x00FFFFFFu;
            //Console.WriteLine(addr.Address);//16820416
            //Console.WriteLine(enderecoBase);//16820416
            //IPAddress bipBase = new IPAddress(enderecoBase);
            //Console.WriteLine(addr.ToString());
            //Console.WriteLine(bipBase.ToString());




            //for (byte i = 0;
            //    i < 255;
            //    i++)
            //{
            //    var ip = new IPAddress(new byte[] { 192, 168, 0, i });
            //    Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", ip, ip.Address.ToString("X8"), ip.Address.ToString("D10"), Convert.ToString(ip.Address, 2).PadLeft(32, '0'));
            //}




            //var ip = IPAddress.Parse("192.168.0.1");
            //Console.WriteLine(ip.ToString());
            //var binario = ip.GetAddressBytes();
            //Console.WriteLine(BitConverter.ToUInt32(binario,0));
            //Console.WriteLine(BitConverter.ToString(ip.GetAddressBytes()));

        }


















        public static void ToLookupEx1()
        {
            // Create a list of Packages.
            List<Package> packages =
                new List<Package>
                {
                    new Package { Company = "Coho Vineyard",  Weight = 25.2, TrackingNumber = 89453312L },
                    new Package { Company = "Lucerne Publishing", Weight = 18.7, TrackingNumber = 89112755L },
                    new Package { Company = "Wingtip Toys", Weight = 6.0, TrackingNumber = 299456122L },
                    new Package { Company = "Contoso Pharmaceuticals", Weight = 9.3, TrackingNumber = 670053128L },
                    new Package { Company = "Wide World Importers", Weight = 33.8, TrackingNumber = 4665518773L }
                };

            // Create a Lookup to organize the packages.
            // Use the first character of Company as the key value.
            // Select Company appended to TrackingNumber
            // as the element values of the Lookup.
            ILookup<char, string> lookup =
                packages
                .ToLookup(p => p.Company[0],
                          p => p.Company + " " + p.TrackingNumber);

            // Iterate through each IGrouping in the Lookup.
            foreach (IGrouping<char, string> packageGroup in lookup)
            {
                // Print the key value of the IGrouping.
                Console.WriteLine(packageGroup.Key);
                // Iterate through each value in the
                // IGrouping and print its value.
                foreach (string str in packageGroup)
                    Console.WriteLine("    {0}", str);
            }
        }



    }


    public class Exemplo
    {
        public string Ip { get; set; }
        public long TotalBytes { get; set; }

    }





}
