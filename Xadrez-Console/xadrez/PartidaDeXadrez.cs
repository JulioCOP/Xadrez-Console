﻿using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; } // acesso apenas de leitura
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas; // Conjuntos para todas as peças do tabuleiro e para as peças que forem capturadas
        // Conjunto são estruturas nas quais podemos fazer buscas rápidas e que não permitem repetição de elementos.
        // HashSet -  comando que dá categorias para cada elemento - ao buscar algo epecífico o elemento vai direto para ele (ex: sorvete no supermercado)
        public bool xeque {  get; private set; }

        public Peca vulneravelEnPassant { get; private set; } // Quando algum jogador mexer o peão pela primeira vez no jogo e avançar duas casas
                                                             // esse movimento realizado, será armazenado.
                                                            // Assim no turno seguinte o jogador estára sob condição de realizar/ receber essa jogada


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            // Necessário instanciar os conjuntos antes das peças serem inseridas no tabuleiro
            xeque = false;
            vulneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();  
            inserirPecas();
        }
        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            // executaMovimento -> realizar jogada
            p.incrementarQtdDeMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.inserirPeca(p, destino);
            if(pecaCapturada != null) // se houver uma peça na casa em que foi movimentada, ou seja se a peça foi caputada será adicionado no metodo de "pecasCaptuadas
            {
                capturadas.Add(pecaCapturada);
            }
            // #JOGADA ESPECIAL ROQUE PEQUENO
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem .coluna + 3);
                Posicao destinoTorre =  new Posicao(origem.linha,origem.coluna + 1);
                Peca torre = tab.retirarPeca(origemTorre);
                torre.incrementarQtdDeMovimentos();
                tab.inserirPeca(torre, destinoTorre);
            }

            // #JOGADA ESPECIAL ROQUE GRANDE
            if (p is Rei && destino.coluna == origem.coluna -2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna -1 );
                Peca torre = tab.retirarPeca(origemTorre);
                torre.incrementarQtdDeMovimentos();
                tab.inserirPeca(torre, destinoTorre);
            }

            // #JOGADA ESPECIAL ENPASSANT
            if(p is Peao)
            {
                if(origem.coluna != destino.coluna && pecaCapturada == null) // Peça mexeu na diagonal e não capturou ninguemENPASSANT
                {
                    Posicao posP;
                    if(p.cor== Cor.Branca)
                    {
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao (destino.linha-1, destino.coluna);
                    }
                    pecaCapturada = tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
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
                capturadas.Remove(pecaCapturada);
            }
            tab.inserirPeca(p,origem); 

            // Como verificar a jogada especial:

            if(p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna + 1);
                Peca torre = tab.retirarPeca(destinoTorre);
                torre.decrementarQtdDeMovimentos();
                tab.inserirPeca(torre, origemTorre);
            }
            if(p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoTorre = new Posicao(origem.linha, origem.coluna - 1);
                Peca torre = tab.retirarPeca(destinoTorre);
                torre.decrementarQtdDeMovimentos();
                tab.inserirPeca(torre, origemTorre);
            }
            // #JOGADA ESPECIAL ENPASSANT
            if (p is Peao)
            {
                if(origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao = tab.retirarPeca(destino);
                    Posicao posP;
                    if(p.cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.coluna);
                    }
                    tab.inserirPeca(peao,posP);
                }
            }
        }


        public void realizaJogada(Posicao origem, Posicao destino) // Regra do xadrez: não pode colocar o seu próprio Rei em xeque
        {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você NÃO PODE se colocar em XEQUE!");
            }
            Peca p = tab.peca(destino); // Qual foi a peça movida ?!

            // #JOGADA ESPECIAL PROMOCAO
            if(p is Peao)
            {
                if((p.cor == Cor.Branca && destino.linha==0) || (p.cor == Cor.Preta && destino.linha == 7))
                {
                    p = tab.retirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tab, p.cor);
                    tab.inserirPeca(dama, destino);
                    pecas.Add(dama);
                }
            }


            if (estaEmXeque(adversario(jogadorAtual)))  
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (testeXequeMate(adversario(jogadorAtual)))
            {
                terminada=true; // Realizada a joga e se o adversário está em xeque mate - FIM DE JOGO
            }
            else
            {
                turno++;
                mudaJogador();
            }

            // #JOGADA ESPECIAL ENPASSANT
            if (p is Peao && (destino.linha == origem.linha -2 || destino.linha == origem.linha + 2))
            // se a peça que fo movida é um peão e andoou duas linhas a mais ou a menos,ou seja,se esta peça andou pela primeira vez.
            {
                vulneravelEnPassant = p; // A o jogador está vulneravél para receber a jogada especial 
            }
            else
            {
                vulneravelEnPassant = null;
            }
            
            
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
            if (!tab.peca(origem).movimentoPossivel(destino))
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

        public HashSet<Peca> pecasCapturadas(Cor cor) // método para ver quais cores das peças que foram capturadas
        {
            HashSet<Peca> aux = new HashSet<Peca>(); //conjunto temporário
            foreach(Peca x in capturadas) // Conjunto peca, pecorre todas as peças da classe pecasCapturadas
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
            aux.ExceptWith(pecasCapturadas(cor)); //Retirar todas as peças capturadas da mesma cor, tendo-se as peças que ainda estão em jogo
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
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public bool testeXequeMate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false; // Se o Rei da cor do jogador não estiver em Xeque Mate, então o retorno é falso ( O rei não esta em xeque mate)
            }
            foreach(Peca x in pecasEmJogo(cor)) // função que -  ao mover uma peça do tabuleiro, será possível remover o xeque mate 
            {
                bool[,] mat = x.movimentosPossiveis();
                for(int i = 0; i < tab.linhas; i++) 
                {
                    for (int j=0;j< tab.colunas; j++) // matriz para cada movimento 
                    {
                        if (mat[i, j]) // Posição possível para peça X
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i,j);
                            Peca pecaCapturada =  executaMovimento(origem, destino); // É realizado o movimento
                            bool testeXeque = estaEmXeque(cor); // Testa se após o movimento o Rei está em xeque
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false; // Se não está mais em xeque mate, o movimento de xeque é retirado, por isso a função retorna o falso
                            }
                        }
                    }
                }
            }
            return true; // Se após realizar o movimento de teste e verificar todas as peças em caso de ser possível sair do xeque

                            // É retornado o valor True, ou seja, FIM DE JOGO - XEQUE MATE
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.inserirPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }
        private void inserirPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this)); //autoreferência para a própria classe
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca,this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta,this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));

            // Além de estar instaciado e mostrar as peças na tela terminal, agora para as peças informadas acima, ela ficam guardadas
            // no conjunto chamado peças.
        }


    }
}
