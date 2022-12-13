using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_Console.information
{
    internal class anotation
    {
        // Os possíveis movimentos de uma peça... Ex: 
            // Bispo: diagonal
            // Torre: movimentos verticais e horizontais
        
        // Para o jogo de xadrez, imginando um tabuleiro real, temos que: 
            //Entrada: Uma data peça em uma posição no tabuleiro 
            // saída: Matriz que informa quais as posições possíveis de saída (matriz booleana) 
                        // VERDADEIRO: movimento possível
                        // FALSO: não é possível realizar o movimento 


        // Quando um rei estará em chque ?
            // Quando pelo menos uma peça adversária possui um movimneto possível para a casa deste rei, ou seja,  quando o rei entra em possibilidade de ser derrubado
    }
}
