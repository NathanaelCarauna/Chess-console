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
        public HashSet<Peca> Pecas { get; set; }
        public HashSet<Peca> Capturadas { get; set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            jogadorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tabuleiro.RetirarPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }
        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            ExecutarMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            Tabuleiro.ValidarPosicao(pos);
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

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(var peca in Capturadas)
            {
                if(peca.Cor == cor)
                {
                    aux.Add(peca);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(var peca in Pecas)
            {
                if(peca.Cor == cor)
                {
                    aux.Add(peca);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            Tabuleiro.ValidarPosicao(destino);
            if (!Tabuleiro.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Movimento inválido");
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

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('d', 1, new Rei(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('e', 1, new Torre(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('e', 2, new Torre(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('d', 2, new Torre(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('c', 2, new Torre(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('c', 1, new Torre(Cor.Branca, Tabuleiro));
            
            ColocarNovaPeca('d', 8, new Rei(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('e', 8, new Torre(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('e', 7, new Torre(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('d', 7, new Torre(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('c', 7, new Torre(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('c', 8, new Torre(Cor.Preta, Tabuleiro));
        }
    }
}
