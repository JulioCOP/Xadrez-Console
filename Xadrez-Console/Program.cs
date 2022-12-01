﻿using System;
using tabuleiro;
using Xadrez_Console.tabuleiro;
using xadrez;

namespace Xadrez_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partidaDeXadrez = new PartidaDeXadrez();    
                // Ao instanciar uma matriz, por padrão, o programa jogar valor nulo para todas as linhas e colunas, em caso de não ser informado nenhum valor.
                while (!partidaDeXadrez.terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirTabuleiro(partidaDeXadrez.tab);
                        Console.WriteLine();
                        Console.WriteLine("Turno: " + partidaDeXadrez.turno);
                        Console.WriteLine("Aguardando jogada da peça: " + partidaDeXadrez.jogadorAtual);

                        Console.WriteLine();
                        Console.Write("Informe a peça e sua posição de origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partidaDeXadrez.validarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partidaDeXadrez.tab.peca(origem).movimentosPossiveis(); // A partir da posição de origem informada pelo usuário, será acessada a peça, para
                                                                                                            // que possa ser verificada as suas regras de locomoção no tabuleiro

                        Console.Clear();
                        Tela.imprimirTabuleiro(partidaDeXadrez.tab, posicoesPossiveis);

                        Console.Write("Informe a posição destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                        partidaDeXadrez.realizaJogada(origem, destino); // Método para o usuário fazer uma jogada
                    }
                    catch(TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                    
                }
                // Imprimir tabuleiro na tela 
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            

            Console.ReadLine();
        }
    }
}
