using tabuleiro;

namespace xadrez
{
    class Torre : Peca
  
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
        
        {

        }
        public override string ToString()
        {
            return "T";
        }

        // metodo em que somente a classe Torre poderá usar para testar se a casa esta livre ou se tem alguma peça 
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
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos)) // Enquanto tiver casa livre ou peça adversária, a torre pode ser movida
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) // se houver peça adversária, ou sja, tab.peça(casa do tabuleiro) for diferente de nula e se tiverpreenchida pora lguma peça 
                {
                    break; // A condição enquanto, para movimentação da peça Torre, para no mesmo instante
                }
                pos.linha = pos.linha - 1;

            }
            // posição abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos)) // Enquanto tiver casa livre ou peça adversária, a torre pode ser movida
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) // se houver peça adversária, ou sja, tab.peça(casa do tabuleiro) for diferente de nula e se tiverpreenchida pora lguma peça 
                {
                    break; // A condição enquanto, para movimentação da peça Torre, para no mesmo instante
                }
                pos.linha = pos.linha + 1;

            }
            // posição direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) // Enquanto tiver casa livre ou peça adversária, a torre pode ser movida
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) // se houver peça adversária, ou sja, tab.peça(casa do tabuleiro) for diferente de nula e se tiverpreenchida pora lguma peça 
                {
                    break; // A condição enquanto, para movimentação da peça Torre, para no mesmo instante
                }
                pos.coluna = pos.coluna + 1;
            }
            // posição esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) // Enquanto tiver casa livre ou peça adversária, a torre pode ser movida
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) // se houver peça adversária, ou sja, tab.peça(casa do tabuleiro) for diferente de nula e se tiverpreenchida pora lguma peça 
                {
                    break; // A condição enquanto, para movimentação da peça Torre, para no mesmo instante
                }
                pos.coluna = pos.coluna - 1;
            }
            return mat;
        }
    }
}
