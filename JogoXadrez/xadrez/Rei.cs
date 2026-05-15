using tabuleiro;
namespace xadrez;

public class Rei : Peca
{
    private PartidaDeXadrez Partida { get; set; }
    public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(cor, tab)
    {
        Partida = partida;
    }

    public override string ToString()
    {
        return "R";
    }

    private bool PodeMover(Posicao pos)
    {
        Peca p = Tab.Peca(pos);
        return p == null || p.Cor != Cor;
    }
    
    public override bool[,] MovimentosPossiveis()
    {
        bool[,] mat = new bool[Tab.Linha, Tab.Colunas];

        Posicao pos = new Posicao(0, 0);
        
        // acima
        pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
        if (Tab.PosicaoValida(pos) && PodeMover(pos)) {
            mat[pos.Linha, pos.Coluna] = true;
        }
        // ne
        pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
        if (Tab.PosicaoValida(pos) && PodeMover(pos)) {
            mat[pos.Linha, pos.Coluna] = true;
        }
        // direita
        pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
        if (Tab.PosicaoValida(pos) && PodeMover(pos)) {
            mat[pos.Linha, pos.Coluna] = true;
        }
        // se
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
        if (Tab.PosicaoValida(pos) && PodeMover(pos)) {
            mat[pos.Linha, pos.Coluna] = true;
        }
        // abaixo
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
        if (Tab.PosicaoValida(pos) && PodeMover(pos)) {
            mat[pos.Linha, pos.Coluna] = true;
        }
        // so
        pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
        if (Tab.PosicaoValida(pos) && PodeMover(pos)) {
            mat[pos.Linha, pos.Coluna] = true;
        }
        // esquerda
        pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
        if (Tab.PosicaoValida(pos) && PodeMover(pos)) {
            mat[pos.Linha, pos.Coluna] = true;
        }
        // no
        pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
        if (Tab.PosicaoValida(pos) && PodeMover(pos)) {
            mat[pos.Linha, pos.Coluna] = true;
        }
        
        // #Jogada especial roque
        if (QtdMovimentos == 0 & !Partida.Xeque)
        {
            //#Jogada especial roque pequeno
            Posicao PosT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
            if (TesteTorreParaRoque(PosT1))
            {
                Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);

                if (Tab.Peca(p1) == null && Tab.Peca(p2) == null)
                {
                    mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                }
            }
            
            //#Jogada especial Roque Grande
            Posicao PosT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
            if (TesteTorreParaRoque(PosT2))
            {
                Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);

                if (Tab.Peca(p1) == null && Tab.Peca(p2) == null && Tab.Peca(p3) == null)
                {
                    mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                }
            }
        }

        return mat;
    }

    private bool TesteTorreParaRoque(Posicao pos)
    {
        Peca p = Tab.Peca(pos);
        return p != null && p is Torre && p.Cor == Cor && p.QtdMovimentos == 0;
    }
}