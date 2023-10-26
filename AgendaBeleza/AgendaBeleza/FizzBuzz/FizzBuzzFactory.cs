using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzBuzz.Contratos;
using FizzBuzz.Servicos;

namespace FizzBuzz
{
    public static class FizzBuzzFactory
    {
        private static readonly Dictionary<string, ICalculadoraFizzBuzz> _strategies = new Dictionary<string, ICalculadoraFizzBuzz>
        {
            {"fizz", new FizzStrategy() },
            {"buzz", new BuzzStrategy() },
            {"fizzbuzz", new FizzBuzzStrategy() },
            {"num", new NumStrategy() },
        };


        public static ICalculadoraFizzBuzz Obter(int i)
        {
            if (Math.IEEERemainder(i, 15) == 0)
            {
                return _strategies["fizzbuzz"];
            }
            
            if (Math.IEEERemainder(i, 5) == 0)
            {
                return _strategies["buzz"];
            }
            
            if (Math.IEEERemainder(i, 3) == 0)
            {
                return _strategies["fizz"];
            }

            return _strategies["num"];

        }
    }
}
