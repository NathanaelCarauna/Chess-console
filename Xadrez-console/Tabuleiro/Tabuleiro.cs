using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] _pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            _pecas = new Peca[linhas, colunas];
        }

        public Peca Peca(int linha, int coluna)
        {
            return _pecas[linha, coluna];
        }

        public Peca Peca(Posicao pos)
        {
            return _pecas[pos.Linha, pos.Coluna];
        }

        public void ColocarPeca(Peca p, Posicao pos)
        {
            _pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;           
        }
    }
}
