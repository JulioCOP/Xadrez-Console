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
            Tabuleiro tab = new Tabuleiro(8, 8);
            // Ao instanciar uma matriz, por padrão, o programa jogar valor nulo para todas as linhas e colunas, em caso de não ser informado nenhum valor.


            tab.inserirPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
            tab.inserirPeca(new Rei(tab, Cor.Preta), new Posicao(1, 3));
            tab.inserirPeca(new Peao(tab, Cor.Preta), new Posicao(6, 2));

            // Imprimir tabuleiro na tela 
            Tela.imprimirTabuleiro(tab);

            Console.ReadLine();
        }
    }
}
