using System;
using tabuleiro;
using xadrez;

namespace Xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tabuleiro = new Tabuleiro(8, 8);

                tabuleiro.ColocarPeca(new Rei(Cor.Preta, tabuleiro), new Posicao(0, 0));
                tabuleiro.ColocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(3, 4));
                tabuleiro.ColocarPeca(new Torre(Cor.Preta, tabuleiro), new Posicao(0, 2));

                tabuleiro.ColocarPeca(new Torre(Cor.Branca, tabuleiro), new Posicao(7, 0));
                tabuleiro.ColocarPeca(new Torre(Cor.Branca, tabuleiro), new Posicao(7, 7));
                Tela.ImprimirTabuleiro(tabuleiro);
                Console.ReadLine();
            }
            catch(TabuleiroExcepetion e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
