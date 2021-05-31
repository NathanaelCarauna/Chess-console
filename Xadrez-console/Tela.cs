using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace Xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for(int i = 0; i< tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for(int j = 0; j< tabuleiro.Colunas; j++)
                {
                    if(tabuleiro.Peca(i,j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tabuleiro.Peca(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
            Console.Write("  a b c d e f g h");
        }
    }
}
