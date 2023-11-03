namespace FizzBuzz
{
    internal class Program
    {
        private static Dictionary<bool, Func<string, string>   > _dicFizz = new Dictionary<bool, Func<string, string>>()
        {
            { true, s =>  s = (s ?? "") + "Fizz"},
            { false, s => s }
        };

        private static Dictionary<bool, Func<string, string>> _dicBuzz = new Dictionary<bool, Func<string, string>>()
        {
            { true, s =>  s = (s ?? "") + "Buzz"},
            { false, s => s }
        };

        static void Main(string[] args)
        {
            
            for(int i = 1; i <= 100; i++)
            {
                string? saida = null;

                saida = _dicFizz[EhDivisivel(i, 3)](_dicBuzz[EhDivisivel(i, 5)](saida));

                Console.WriteLine(saida ?? i.ToString());
            }


            Console.ReadLine();
        }

        public static bool EhDivisivel(int a, int b)
        {
            return a % b == 0;

            //return Math.IEEERemainder(a, b) == 0;

            //return (a / b) * b == a;

            //var resultado = (double)a / b;
            //int vizinhoTraz = (int)Math.Floor(resultado);
            //int vizinhoFrente = vizinhoTraz + 1;

            //return !(resultado > vizinhoTraz && resultado < vizinhoFrente);
        }
    }


}


/*
 * 
 * 
 * solução 1
 * 
 * 
 *         static void Main(string[] args)
        {
            
            for(int i = 1; i <= 100; i++)
            {
                if(i % 15 == 0)
                {
                    Console.WriteLine("Fizz Buzz");
                    continue;
                }

                if (i % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                    continue;
                }

                if (i % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                    continue;
                }

                Console.WriteLine(i);
            }


            Console.ReadLine();
        }

*/


/*
 * 
 *         static void Main(string[] args)
        {
            
            for(int i = 1; i <= 100; i++)
            {
                string? saida = null;

                if (i % 3 == 0)
                {
                    saida = (saida ?? "") + "Fizz";
                }

                if (i % 5 == 0)
                {
                    saida = (saida ?? "") +  "Buzz";
                }


                Console.WriteLine(saida ?? i.ToString());
            }


            Console.ReadLine();
        }

*/