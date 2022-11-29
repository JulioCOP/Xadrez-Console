using System;
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
                    Console.Clear();
                    Tela.imprimirTabuleiro(partidaDeXadrez.tab);

                    Console.Write("Informe a posição de origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();

                    bool[,] posicoesPossiveis = partidaDeXadrez.tab.peca(origem).movimentosPossiveis(); // A partir da posição de origem informada pelo usuário, será acessada a peça, para
                                                                                                       // que possa ser verificada as suas regras de locomoção no tabuleiro

                    Console.Clear();
                    Tela.imprimirTabuleiro(partidaDeXadrez.tab, posicoesPossiveis);

                    Console.Write("Informe a posição destino: ");
                    Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                    partidaDeXadrez.executaMovimento(origem, destino);
                    
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
