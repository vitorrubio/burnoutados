using IPAddressHelpers;
using System.Net;

namespace IpHelperConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {


            List<string> ips = new List<string>
            {

                "192.168.0.1",
                "192.168.0.2",

                "192.168.1.1",
                "192.168.1.2",

                "192.168.2.3",
            };

            byte cidr = 24;
            uint mascara = IPAddressExtensions.BaseAddress(cidr);
            Console.WriteLine(mascara.ToString("X8"));


            var tabela = IpTreeBuilder.BuildIpLookup(ips, 24);


            foreach (var packageGroup in tabela)
            {
                // Print the key value of the IGrouping.
                Console.WriteLine(packageGroup.Key + "/" + cidr.ToString() + $" - mostrando {packageGroup.Count()} de {256} enderecos");
                // Iterate through each value in the
                // IGrouping and print its value.
                foreach (var ip in packageGroup)
                    Console.WriteLine("\t{0}", ip);
            }

        }
    }
}