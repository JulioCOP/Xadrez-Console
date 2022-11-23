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
    }

}
