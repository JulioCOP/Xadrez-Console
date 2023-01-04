using tabuleiro;
namespace xadrez
{
    class Rei : Peca
    // Classe Rei é uma subclasse da classe Peca, ou seja, a classe Rei herda da classe Peca
    {
        public Rei( Tabuleiro tab, Cor cor) : base(tab, cor)
                                            // repassando da classe tabuleiro para a classe peca
        {

        }
        public override string ToString()
        {
            return "R";
        }

        // metodo em que somente a classe Rei, poderá usar 
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor; //this.cor peça adversária 
        }
        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);
            // posição acima
            pos.definirValores(pos.Linha - 1, pos.Coluna);
            if(tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;  
            }
            // posição nordeste
            pos.definirValores(pos.Linha - 1, pos.Coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // posição direita
            pos.definirValores(pos.Linha, pos.Coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // posição sudeste
            pos.definirValores(pos.Linha + 1, pos.Coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // posição abaixo
            pos.definirValores(pos.Linha +1, pos.Coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // posicao sudoeste
            pos.definirValores(pos.Linha + 1, pos.Coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // posição esquerda
            pos.definirValores(pos.Linha, pos.Coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            // posição noroeste
            pos.definirValores(pos.Linha - 1, pos.Coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            return mat;
        }

    }

}
