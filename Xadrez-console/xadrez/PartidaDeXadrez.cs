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
        public bool Xeque { get; set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            jogadorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            Xeque = false;
            ColocarPecas();
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tabuleiro.RetirarPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                Capturadas.Add(pecaCapturada);
            }

            //JogadaEspecial roque pequeno
            if(p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RetirarPeca(origemT);
                T.IncrementarQteMovimentos();
                Tabuleiro.ColocarPeca(T, destinoT);
            }

            //JogadaEspecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna -1);
                Peca T = Tabuleiro.RetirarPeca(origemT);
                T.IncrementarQteMovimentos();
                Tabuleiro.ColocarPeca(T, destinoT);
            }
            return pecaCapturada;
        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);

            if (EstahEmXeque(jogadorAtual))
            {
                DesfazerJogada(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }
            if (EstahEmXeque(Adversaria( jogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (TesteXequeMate(Adversaria(jogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

        }

        private void DesfazerJogada(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tabuleiro.RetirarPeca(destino);
            p.DecrementarQteMovimentos();
            if(pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }

            //JogadaEspecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RetirarPeca(destinoT);
                T.DecrementarQteMovimentos();
                Tabuleiro.ColocarPeca(T, origemT);
            }

            //JogadaEspecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RetirarPeca(destinoT);
                T.DecrementarQteMovimentos();
                Tabuleiro.ColocarPeca(T, origemT);
            }

            Tabuleiro.ColocarPeca(p, origem);
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
            if (!Tabuleiro.Peca(origem).MovimentoPossivel(destino))
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

        private Cor Adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach(var peca in PecasEmJogo(cor))
            {
                if(peca is Rei)
                {
                    return peca;
                }
            }
            return null;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstahEmXeque(cor))
            {
                return false;
            }
            foreach(var peca in PecasEmJogo(cor))
            {
                bool[,] mat = peca.MovimentosPossiveis();
                for (int i= 0; i < Tabuleiro.Linhas; i++)
                {
                    for(int j = 0; j<Tabuleiro.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool testeXeque = EstahEmXeque(cor);
                            DesfazerJogada(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }

                    }
                }
            }
            return true;
        }

        public bool EstahEmXeque(Cor cor)
        {
            Peca r = Rei(cor);
            if (r == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro");
            }
            foreach(var x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if(mat[r.Posicao.Linha, r.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            //Brancas
            ColocarNovaPeca('a', 1, new Torre(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('b', 1, new Cavalo(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('c', 1, new Bispo(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('d', 1, new Dama(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('e', 1, new Rei(Cor.Branca, Tabuleiro, this));
            ColocarNovaPeca('f', 1, new Bispo(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('g', 1, new Cavalo(Cor.Branca, Tabuleiro));            
            ColocarNovaPeca('h', 1, new Torre(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('a', 2, new Peao(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('b', 2, new Peao(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('c', 2, new Peao(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('d', 2, new Peao(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('e', 2, new Peao(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('f', 2, new Peao(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('g', 2, new Peao(Cor.Branca, Tabuleiro));
            ColocarNovaPeca('h', 2, new Peao(Cor.Branca, Tabuleiro));            
            
            //Pretas
            ColocarNovaPeca('a', 8, new Torre(Cor.Preta, Tabuleiro));            
            ColocarNovaPeca('b', 8, new Cavalo(Cor.Preta, Tabuleiro));            
            ColocarNovaPeca('c', 8, new Bispo(Cor.Preta, Tabuleiro));            
            ColocarNovaPeca('d', 8, new Dama(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('e', 8, new Rei(Cor.Preta, Tabuleiro, this));
            ColocarNovaPeca('f', 8, new Bispo(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('g', 8, new Cavalo(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('h', 8, new Torre(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('a', 7, new Peao(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('b', 7, new Peao(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('c', 7, new Peao(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('d', 7, new Peao(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('e', 7, new Peao(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('f', 7, new Peao(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('g', 7, new Peao(Cor.Preta, Tabuleiro));
            ColocarNovaPeca('h', 7, new Peao(Cor.Preta, Tabuleiro));
        }
    }
}
