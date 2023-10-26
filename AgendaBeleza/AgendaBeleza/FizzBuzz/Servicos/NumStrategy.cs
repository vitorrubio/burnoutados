using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FizzBuzz.Contratos;

namespace FizzBuzz.Servicos
{
    internal class NumStrategy : ICalculadoraFizzBuzz
    {

        public void EscreveResult(int i)
        {
            Console.WriteLine($"{i} - {GetType().Name}");
        }
    }
}
