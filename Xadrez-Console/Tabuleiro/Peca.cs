﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace Xadrez_Console.tabuleiro
{
    internal class Peca
        // classe peça é uma classe genérica, ou seja, uma peça qualquer 
    {
        public Posicao posicao { get; set; }
        // Peça precisar estar em um determinado lugar no tabuleiro
        public Cor cor { get; protected set; }

        public int qtdMovimentos { get; protected set; }

        // {get; protected set;} -> Acessada por outras classes, porem só pode ser alterada por ela mesmo ou por subclasses.

        public Tabuleiro tab { get; protected set; }

        public Peca()
        {

        }
        public Peca( Cor cor, Tabuleiro tab)
        {
            this.posicao = null;
            this.cor = cor;
            this.qtdMovimentos = 0; // Não é um argumento para o contrutor, pois no inicio do jogo, as peças possuem um total de 0 movimentos, ou seja, ela não sofreu nenhuma alteração ainda.
            this.tab = tab;
        }

        public Peca(Tabuleiro tab, Cor cor)
        {
            this.tab = tab;
            this.cor = cor;
        }
    }
}

// Na Camada Tabuleiro, o tabuleiro possui uma classe com varias peças 

// Porem deve-se criar uma nova camada, chamada jogo de xadrez, ao qual eu terei as minhas peças nomeadas (rei, bispo, cavalo, torre, dama, peão)

// Nessas camada de jogo de xadrez, as classes de cada peça são heranças da classe Peças da camada tabuleiro