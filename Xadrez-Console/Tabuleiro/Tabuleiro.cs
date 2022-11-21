using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_Console.tabuleiro
{
    internal class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        // {get;set;} - pode possuir um número maior de linhas e colunas do que o xadrez por exemplo.
        private Peca[,] pecas; // classe privativa, pois somente o tabuleiro tem acesso a elas.

        public Tabuleiro()
        {

        }
        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];

        }   
    }
}
