﻿namespace FizzBuzz
{
    internal class Program
    {
        static void Main(string[] args)
        {

            for (int i = 0; i<=100; i++)
            {

                FizzBuzzFactory.Obter(i).EscreveResult(i);
                
            }

            Console.ReadLine();
        }

    }
}