using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tabuleiro
{
    internal class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; } 

        public Posicao()
        {

        }
        public Posicao (int linha, int coluna)
        {
            this.Linha = linha;
            this.Coluna = coluna;
        }

        public override string ToString() // Retornar um valor como string no programa principal
        {
            return Linha + ", " + Coluna;
        }
    }
}
