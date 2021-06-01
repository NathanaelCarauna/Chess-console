using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            jogadorAtual = Cor.Branca;
            ColocarPecas();
            Terminada = false;
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tabuleiro.RetirarPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(p, destino);
        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            ExecutarMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tabuleiro.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida");
            }
            if (jogadorAtual != Tabuleiro.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça escolhida não é sua");
            }
            if (!Tabuleiro.Peca(pos).ExistemMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça escolhida");
            }
        }

        private void MudaJogador()
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

        private void ColocarPecas()
        {
            Tabuleiro.ColocarPeca(new Rei(Cor.Branca, Tabuleiro), new PosicaoXadrez('d', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('e', 1).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('e', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('d', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('c', 2).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Branca, Tabuleiro), new PosicaoXadrez('c', 1).ToPosicao());

            Tabuleiro.ColocarPeca(new Rei(Cor.Preta, Tabuleiro), new PosicaoXadrez('d', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('e', 8).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('e', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('d', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('c', 7).ToPosicao());
            Tabuleiro.ColocarPeca(new Torre(Cor.Preta, Tabuleiro), new PosicaoXadrez('c', 8).ToPosicao());
        }
    }
}
