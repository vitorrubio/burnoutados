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
            Func<ICalculadoraFizzBuzz> fizzbuzz = () => _strategies["fizzbuzz"];
            Func<ICalculadoraFizzBuzz> fizz = () => _strategies["fizz"];
            Func<ICalculadoraFizzBuzz> buzz = () => _strategies["buzz"];
            Func<ICalculadoraFizzBuzz> num = () => _strategies["num"];



            var a = new Dictionary<bool, Func<ICalculadoraFizzBuzz>>
            {
                {true,  fizzbuzz},
                {false, () => 
                    {
                       var b = new Dictionary<bool, Func< ICalculadoraFizzBuzz>>
                       {
                           {true, buzz },
                           {false, () =>
                               {
                                  var c = new Dictionary<bool, Func< ICalculadoraFizzBuzz>>
                                  {
                                      {true, fizz },
                                      {false, num }
                                  };

                                   return c[i % 3 == 0].Invoke();
                               }
                           }
                       };

                       return b[i % 5 == 0].Invoke();
                    } 
                },
            };



            return a[i % 15 == 0].Invoke();

        }
    }
}
