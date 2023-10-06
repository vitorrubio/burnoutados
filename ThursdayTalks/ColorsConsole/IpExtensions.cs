using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ColorsConsole
{
    public static class IpExtensions
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
            return (IpExtensions.IpToUInt(ip) & mask);
        }

        public static UInt32 CidrBasedSubnetLookupKey(this IPAddress ip, byte cidr)
        {
            return SubnetLookupKey(ip, BaseAddress(cidr));
        }
    }
}
