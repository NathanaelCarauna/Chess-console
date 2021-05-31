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
                PosicaoXadrez pos = new PosicaoXadrez('c', 1);

                Console.WriteLine(pos);

                Console.WriteLine(pos.ToPosicao());
                //Tabuleiro tabuleiro = new Tabuleiro(8, 8);

                //tabuleiro.ColocarPeca(new Rei(Cor.Branca, tabuleiro), new Posicao(0, 0));
                //tabuleiro.ColocarPeca(new Torre(Cor.Branca, tabuleiro), new Posicao(3, 4));
                //tabuleiro.ColocarPeca(new Torre(Cor.Branca, tabuleiro), new Posicao(0, 9));

                //Tela.ImprimirTabuleiro(tabuleiro);
            }
            catch(TabuleiroExcepetion e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
