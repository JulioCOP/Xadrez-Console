namespace tabuleiro
{
    class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        // {get;set;} - pode possuir um número maior de linhas e colunas do que o xadrez por exemplo.
        private Peca[,] pecas; // classe privativa, pois somente o tabuleiro tem acesso a elas, não permitindo que nenhuma outra classe além do tabuleiro, possa fazer alguma interação
        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];

        }
        // Sobrecarga para o metodo Peca 
        public Peca peca(Posicao pos)
        {
            return pecas[pos.linha, pos.coluna];
        }
        public Peca peca(int linha, int coluna)
        // Método publico que permite retornar a matriz peças, ou seja, ele pode acessar uma peça na linha - coluna ou coluna -linha
        {
            return pecas[linha, coluna];
        }
        // Operação para coseguir inserir uma peça

        // Método para realizar um teste de posição
        public bool existePeca(Posicao pos)
        {
            validarPosicao(pos); // em caso de ser uma posição invalida, o método "validarPosicao" é cortado e assim aparece a mensagem da exceção 
            return peca(pos) != null;
        }

        public void inserirPeca(Peca p, Posicao pos)
        {
            // Colocar peça aonde a posição no tabuleiro esta DISPONÍVEL
            if (existePeca(pos))
            {
                throw new TabuleiroException("JÁ EXISTE UMA PEÇA PARA ESTA POSIÇÃO!");
            }
            pecas[pos.linha, pos.coluna] = p;
            p.posicao= pos;
        }

        //Método para retirar as peças que forem "comidas", do tabuleiro

        public Peca retirarPeca(Posicao pos)
        {
            if (peca(pos) == null)
            {
                return null;
            }
            Peca aux = peca(pos);
            aux.posicao = null;
            pecas[pos.linha, pos.coluna] = null;
            return aux;
        }

        //Controle de erros -> posições de acordo com a matriz, iniciando a partir da faixa 0

        
        public bool posicaoValida(Posicao pos)
        // Método para testar se a posição da peça no tabueliro é valida
        {
            if (pos.linha <0 || pos.linha >= linhas || pos.coluna <0 || pos.coluna >= colunas)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void validarPosicao(Posicao pos)
        // Método recebe uma posição da peça e em caso de não ser válida,  - assim se a posição lançada para a peça não for valida de acordo com a matriz informada, será lançada uma excessão

        {
            if (!posicaoValida(pos))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }

    }
}
