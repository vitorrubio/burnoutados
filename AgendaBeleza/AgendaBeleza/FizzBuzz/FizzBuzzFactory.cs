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
            

            Func<ICalculadoraFizzBuzz> fizz = () =>
            {
                var dic = new Dictionary<bool, ICalculadoraFizzBuzz>
                {
                    { true, _strategies["fizz"]},
                    { false, _strategies["num"]}
                };

                return dic[i % 3 == 0];
            };

            Func<ICalculadoraFizzBuzz> buzz = () =>
            {
                var dic = new Dictionary<bool, ICalculadoraFizzBuzz>
                {
                    { true, _strategies["buzz"]},
                    { false, fizz()}
                };

                return dic[i % 5 == 0];
            };

            Func<ICalculadoraFizzBuzz> fizzbuzz = () =>
            {
                var dic = new Dictionary<bool, ICalculadoraFizzBuzz>
                {
                    { true, _strategies["fizzbuzz"]},
                    { false, buzz()}
                };

                return dic[i % 15 == 0];
            };

            return fizzbuzz();

        }
    }
}
