namespace CodeInterviewConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] teste1 = { 1, 3, 5, 50 };
            Console.WriteLine(SomaIndicesPares(teste1));

            int[] teste2 = new int[100];
            Random rnd = new Random();

            for (int i = 0; i < teste2.Length; i++)
            {
                teste2[i] = rnd.Next(0, 100);
            }

            Console.WriteLine(SomaIndicesPares(teste2));
        }

        public static void VerificaTriangulo(double x, double y, double z)
        {
            if ((x < (y + z)) && (y < (x + z)) && (z < (y + x)))
            {
                Console.WriteLine("É um triângulo");

                if(x == y && x == z)
                {
                    Console.WriteLine("É um triângulo equilátero");
                    return;
                }

                if (x != y && x != z && y != z)
                {
                    Console.WriteLine("É um triângulo escaleno");
                    return;
                }


                Console.WriteLine("É um triângulo isósceles");
            }
            else
            {
                Console.WriteLine("Não é um triângulo");
            }
        }



        public static int SomaIndicesPares(int[] vetor)
        {
            int resultado = 0;
            for(int i =0; i < vetor.Length; i++)
            {
                if(i%2==0)
                {
                    resultado += vetor[i];
                }
            }

            return resultado;
        }


    }
}