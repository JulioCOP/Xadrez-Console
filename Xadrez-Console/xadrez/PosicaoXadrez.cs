using System;
using tabuleiro;
// Classe PosicaoXadrez é posição das peças no contexto do xadrez

namespace xadrez
{
    class PosicaoXadrez
    {
        public char coluna { get; set; }
        public int linha { get; set; }
        
        public PosicaoXadrez()
        {

        }
        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }
        // método para converter a posição do xadrez, para posição interna da matriz ( A8 -> [0,0] A7->[1,0] B8->[0,2]
        public Posicao toPosicao()
            // toPosicao tem a mesma função do toString, ou seja, transformar as posições do xadrez para a posição da matriz no código
        {
            return new Posicao(8-linha, coluna - 'a');
        }
        public override string ToString()
        {
            return "" + coluna + linha;
        }
    }
}
