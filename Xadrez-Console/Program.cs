using System;
using tabuleiro;
using Xadrez_Console.tabuleiro;

namespace Xadrez_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            // Imprimir tabuleiro na tela 
            Tela.imprimirTabuleiro(tab);

            Console.ReadLine();
        }
    }
}
