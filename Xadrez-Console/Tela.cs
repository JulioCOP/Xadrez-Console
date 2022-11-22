using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez_Console.tabuleiro;

namespace Xadrez_Console
{
    internal class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j=0; j<tab.colunas; j++)
                {
                    // Imprimir peça que está na posição J
                    if (tab.peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                       Console.Write(tab.peca(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }

        }
    }
}
