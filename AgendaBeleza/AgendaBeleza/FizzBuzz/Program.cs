namespace FizzBuzz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int nenhum = 0;
            for (int i = 0; i<=100; i++)
            {
                /*
                if(i % 15 == 0)
                {
                    Console.WriteLine("Fizz Buzz");
                }
                else if (i % 5 == 0)
                {
                    Console.WriteLine("Buzz");
                }
                else if (i % 3 == 0)
                {
                    Console.WriteLine("Fizz");
                }
                else
                {
                    Console.WriteLine(i);
                }
                */

                switch(i % 15)
                {
                    case 0:
                        Console.WriteLine("Fizz Buzz");
                        break;
                    case 3:
                    case 6:
                    case 9:
                    case 12:
                        Console.WriteLine("Fizz");
                        break;
                    case 5:
                    case 10:
                        Console.WriteLine("Buzz");
                        break;
                    default:
                        nenhum++;
                        Console.WriteLine(i);
                        break;
                }
            }
            Console.WriteLine($"nenhum: {nenhum}");
            Console.ReadLine();
        }
    }
}