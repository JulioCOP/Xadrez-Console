using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro
{
    internal class Posicao
    {
        public int linha { get; set; }
        public int coluna { get; set; } 

        public Posicao()
        {

        }
        public Posicao (int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        public void definirValores(int linha, int coluna)
        {
            this.linha= linha;
            this.coluna =coluna;
        }
        public override string ToString() // Retornar um valor como string no programa principal
        {
            return linha 
                + ", " 
                + coluna;
        }
    }
}
