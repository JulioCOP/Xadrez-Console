using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace Xadrez_Console.tabuleiro
{
    internal class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        // {get;set;} - pode possuir um número maior de linhas e colunas do que o xadrez por exemplo.
        private Peca[,] pecas; // classe privativa, pois somente o tabuleiro tem acesso a elas, não permitindo que nenhuma outra classe além do tabuleiro, possa fazer alguma interação

        public Tabuleiro()
        {

        }
        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];

        }
        
        public Peca peca(int linha, int coluna)
        // Método publico que permite retornar a matriz peças, ou seja, ele pode acessar uma peça na linha - coluna ou coluna -linha
        {
            return pecas[linha, coluna];
        }
        // Operação para coseguir inserir uma peça

        public void inserirPeca(Peca p, Posicao pos)
        {
            pecas[pos.Linha, pos.Coluna] = p;
            p.posicao= pos;
        }
    }
}
