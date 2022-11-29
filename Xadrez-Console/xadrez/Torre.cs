using tabuleiro;
using Xadrez_Console.tabuleiro;
using xadrez;

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
    }

}
