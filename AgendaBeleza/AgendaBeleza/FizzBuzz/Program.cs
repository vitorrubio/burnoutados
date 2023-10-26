namespace FizzBuzz
{
    internal class Program
    {
        static void Main(string[] args)
        {

            for (int i = 0; i<=100; i++)
            {
                
                if(Math.IEEERemainder (i, 15) == 0)
                {
                    Console.WriteLine("Fizz Buzz");
                }
                else if (Math.IEEERemainder(i ,5) == 0)
                {
                    Console.WriteLine("Buzz");
                }
                else if (Math.IEEERemainder(i , 3) == 0)
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

        //public static bool EhDivisivel(double x, double y)
        //{
        //    double q = x / y;

        //    int vizinhoDeBaixo = (int)q;
        //    int vizinhoDeCima = vizinhoDeBaixo+1;

        //    return !(q > vizinhoDeBaixo && q < vizinhoDeCima);
        //}
    }
}