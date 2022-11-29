using tabuleiro;
using Xadrez_Console.tabuleiro;

namespace xadrez
{
    class Peao : Peca

    {
        public Peao(Tabuleiro tab, Cor cor) : base(tab, cor)

        {

        }
        public override string ToString()
        {
            return "P";
        }
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor; //this.cor peça adversária 
        }
        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];
            return mat;
        }
    }
}
