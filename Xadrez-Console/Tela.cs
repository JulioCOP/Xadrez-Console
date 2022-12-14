using System;
using System.Collections.Generic;
using Xadrez_Console.tabuleiro;
using xadrez;


namespace Xadrez_Console
{
     class Tela
    {
        public static void imprimirPartida(PartidaDeXadrez partida)
        {
            imprimirTabuleiro(partida.tab);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);
            Console.WriteLine("Turno: " + partida.turno);
            Console.WriteLine("Aguardando jogada da peça: " + partida.jogadorAtual);
            if (partida.xeque)
            {
                Console.WriteLine("VOCÊ RECEBEU UM XEQUE!!!");
            }
        }

        public static void imprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine();
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.coresDePecasCapturadas(Cor.Branca));
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            imprimirConjunto(partida.coresDePecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }
        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach(Peca x in conjunto)
            {
                Console.Write(x + " ");
            }
            Console.WriteLine("]");
        }
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j=0; j<tab.colunas; j++)
                {
                    imprimirPeca(tab.peca(i, j)); // Imprimir peça que está na posição J
                    Console.Write(" "); 
                }
                Console.WriteLine();
            }
            Console.Write("  A  B  C  D  E  F  G  H ");

        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tab.peca(i, j)); // Imprimir peça que está na posição J
                    Console.BackgroundColor = fundoOriginal;
                    Console.Write("  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A   B   C   D   E   F   G   H  ");
            Console.BackgroundColor = fundoOriginal;

        }



        // Método para modificar as cores de impressão na tela do terminal 
        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char Coluna= s[0];
            int Linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(Coluna, Linha);

        }
        public static void imprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
