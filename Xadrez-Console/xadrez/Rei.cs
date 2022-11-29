using tabuleiro;
using Xadrez_Console.tabuleiro;
using xadrez;
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
    }
    
}
