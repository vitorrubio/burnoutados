using Microsoft.AspNetCore.HttpOverrides;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressHelpers
{
    public static class IPAddressExtensions
    {
        public static UInt32 BaseAddress(byte cidr)
        {
            uint mask = uint.MaxValue - (((uint)Math.Pow(2, 32 - (uint)cidr)) - 1);
            return mask;
        }

        public static uint IpToUInt(this IPAddress ip)
        {
            var dados = ip.GetAddressBytes();
            uint valor = ((uint)dados[0] << 24) + ((uint)dados[1] << 16) + ((uint)dados[2] << 8) + ((uint)dados[3]);
            return valor;
        }

        public static IPAddress UIntToIp(uint endereco)
        {
            byte[] bytes = new byte[4];
            bytes[3] = (byte)(endereco & 0x000000FFu);
            bytes[2] = (byte)((endereco & 0x0000FF00u) >> 8);
            bytes[1] = (byte)((endereco & 0x00FF0000u) >> 16);
            bytes[0] = (byte)((endereco & 0xFF000000u) >> 24);

            var ip = new IPAddress(bytes);
            return ip;
        }

        public static UInt32 SubnetLookupKey( this IPAddress ip, UInt32 mask)
        {
            return (ip.IpToUInt() & mask);
        }

        public static UInt32 CidrBasedSubnetLookupKey(this IPAddress ip, byte cidr)
        {
            return SubnetLookupKey(ip, BaseAddress(cidr));
        }
    }

    public static class IpTreeBuilder
    {
        public static ILookup<IPAddress, IPAddress> BuildIpLookup(List<string> ips, byte cidr = 24)
        {
            
            var table = ips
                .Select(x => IPAddress.Parse(x))
                .ToLookup(x => IPAddressExtensions.UIntToIp(x.CidrBasedSubnetLookupKey(cidr)));

            return table;
            
        }


    }
}
