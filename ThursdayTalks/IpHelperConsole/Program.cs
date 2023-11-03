namespace IpHelperConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            int x = 0;
            int y = x++;

            Console.WriteLine($"X {x} | Y {y}");

            bool logico = true;
            int z = logico ? x++ : x--;

            Console.WriteLine($"X {x} | Y {y} | Z {z}");
        }
    }
}