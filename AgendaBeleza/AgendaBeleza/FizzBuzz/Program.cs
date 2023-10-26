namespace FizzBuzz
{
    internal class Program
    {
        static void Main(string[] args)
        {

            for (int i = 0; i<=100; i++)
            {
                
                if(EhDivisivel(i, 15))
                {
                    Console.WriteLine("Fizz Buzz");
                }
                else if (EhDivisivel(i ,5))
                {
                    Console.WriteLine("Buzz");
                }
                else if (EhDivisivel(i , 3))
                {
                    Console.WriteLine("Fizz");
                }
                else
                {
                    Console.WriteLine(i);
                }
                
            }

            Console.ReadLine();
        }

        public static bool EhDivisivel(double x, double y)
        {
            double q = x / y;

            int vizinhoDeBaixo = (int)q;
            int vizinhoDeCima = vizinhoDeBaixo+1;

            return !(q > vizinhoDeBaixo && q < vizinhoDeCima);
        }
    }
}