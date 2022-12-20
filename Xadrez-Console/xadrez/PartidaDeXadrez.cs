using System;
using System.Data;
using tabuleiro;
using Xadrez_Console.tabuleiro;
using System.Collections.Generic;

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; } // acesso apenas de leitura
        private HashSet<Peca> pecas;
        private HashSet<Peca> pecasCapturadas; // Conjuntos para todas as peças do tabuleiro e para as peças que forem capturadas
        // Conjunto são estruturs nas quais podemos fazer buscas rápidas e que não permitem repetição de elementos.
        // HashSet -  comando que dá categorias para cada elemento - ao buscar algo epecífico o elemento vai direto para ele (ex: sorvete no supermercado)
        public bool xeque {  get; private set; }    



        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            // Necessário instanciar os conjuntos antes das peças serem inseridas no tabuleiro
            xeque = false;
            pecas = new HashSet<Peca>();
            pecasCapturadas = new HashSet<Peca>();  
            inserirPecas();
        }
        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            // executarMovimento -> realizar jogada
            p.incrementarQtdDeMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.inserirPeca(p, destino);
            if(pecaCapturada != null) // se houver uma peça na casa em que foi movimentada, ou seja se a peça foi caputada será adicionado no metodo de "pecasCaptuadas
            {
                pecasCapturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }
        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQtdDeMovimentos();
            if (pecaCapturada != null)
            {
                tab.inserirPeca(pecaCapturada, destino);
                pecasCapturadas.Remove(pecaCapturada);
            }
            tab.inserirPeca(p,origem);  
        }


        public void realizaJogada(Posicao origem, Posicao destino) // Regra do xadrez: não pode colocar o seu próprio Rei em xeque
        {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você NÃO PODE se colocar em XEQUE!");
            }

            var validXeque = estaEmXeque(adversario(jogadorAtual));
            if (validXeque)
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            turno++;
            mudaJogador();
        }


        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null) // Se peça que esta no tabuleiro, na posição de origem, estiver na posição nula(sem possibilidade de movimentar, posição errada)
            {
                throw new TabuleiroException("Não existe peça nesta posição de origem escolhida pelo usuário!");
            }
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça escolhida para a jogada não é a sua! ");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida! ");
            }
        }
        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> coresDePecasCapturadas(Cor cor) // método para ver quais cores das peças que foram capturadas
        {
            HashSet<Peca> aux = new HashSet<Peca>(); //conjunto temporário
            foreach(Peca x in pecasCapturadas) // Conjunto peca, pecorre todas as peças da classe pecasCapturadas
            {
                if (x.cor == cor)
                {
                    aux.Add(x); // É adicionado ao conjunto as peças com as cores correspondetes a sua captura
                }
            }
            return aux;
        }
       

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>(); //conjunto temporário
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(coresDePecasCapturadas(cor)); //Retirar todas as peças capturadas da mesma cor, tendo-se as peças que ainda estão em jogo
            return aux;
        }

        private Cor adversario(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }
        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            // Peca é superclasse enquanto o Rei é um subclasse desta superclasse, para que elas se comuniquem é necessário o uso do comando "is"
            {
                if (x is Rei) // se a variavel x é uma instância da  subclasse rei, retorna x (a própria peça) daquela mesma cor
                {
                    return x; 
                }

            } // operação criada para retornar um rei de uma determinada cor
            return null;
        }
        // Método para demonstrar todos as possibiidades de movimento de TODAS as peças do tabuleiro

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei (cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro! ");

            }
            // analise para cada movimento possíveis das matrizes de peças
            foreach (Peca x in pecasEmJogo(adversario(cor)))
            {
                bool[,] mat = x.movimentosPossiveis(); // matriz de movimentos possíveis
                if (mat[R.posicao.Linha, R.posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.inserirPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }
        private void inserirPecas()
        {
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));

            // Além de estar instaciado e mostrar as peças na tela terminal, agora para as peças informadas acima, ela ficam guardadas
            // no conjunto chamado peças.
        }


    }
}
