namespace GeneralTests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //https://0.30000000000000004.com/

            Console.WriteLine("Hello, World!");

            float fa = 0.1f;
            float fb = 0.2f;
            float fr = fa + fb;
            Console.WriteLine(fr);

            double da = 0.1d;
            double db = 0.2d;
            Console.WriteLine(da + db);


            decimal ma = 0.1m;
            decimal mb = 0.2m;
            Console.WriteLine(ma + mb);

            /*
            decimal dividend = Decimal.One;
            decimal divisor = 3.0m;
            Console.WriteLine(dividend / divisor * divisor);

            float dividendf = 1.0f;
            float divisorf = 3.0f;
            Console.WriteLine(dividendf / divisorf * divisorf);



            double dividendd = 1.0d;
            double divisord = 3.0d;
            Console.WriteLine(dividendd / divisord * divisord);

            */

            Console.ReadKey();
        }
    }
}