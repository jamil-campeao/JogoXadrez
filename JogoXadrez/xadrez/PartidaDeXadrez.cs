using System.Collections.Generic;
using System.Xml;
using tabuleiro;
namespace xadrez;

public class PartidaDeXadrez
{
    public Tabuleiro Tab { get; private set; }
    public int Turno { get; private set; }
    public Cor JogadorAtual { get; private set; }
    public bool Terminada { get; set; }
    private HashSet<Peca> Pecas { get; set; }
    private HashSet<Peca> Capturadas { get; set; }
    public bool Xeque { get; private set; }

    public PartidaDeXadrez()
    {
        Tab = new Tabuleiro(8, 8);
        Turno = 1;
        JogadorAtual = Cor.Branca;
        Terminada = false;
        Pecas = new HashSet<Peca>();
        Capturadas = new HashSet<Peca>();
        Xeque = false;
        ColocarPecas();
    }

    public Peca ExecutaMovimento(Posicao origem, Posicao destino)
    {
        Peca p = Tab.RetirarPeca(origem);
        p.IncrementarQtdeMovimentos();
        
        Peca pecaCapturada = Tab.RetirarPeca(destino);
        Tab.ColocarPeca(p, destino);

        if (pecaCapturada != null)
        {
            Capturadas.Add(pecaCapturada);
        }

        return pecaCapturada;
    }

    public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
        Peca p = Tab.RetirarPeca(destino);
        p.DecrementarQtdeMovimentos();

        if (pecaCapturada != null)
        {
            Tab.ColocarPeca(pecaCapturada, destino);
            Capturadas.Remove(pecaCapturada);
        }
        Tab.ColocarPeca(p, origem);
    }

    public void RealizaJogada(Posicao origem, Posicao destino)
    {
        Peca pecaCapturada = ExecutaMovimento(origem, destino);

        if (EstaEmXeque(JogadorAtual))
        {
            DesfazMovimento(origem, destino, pecaCapturada);
            throw new TabuleiroException("Você não pode se colocar em xeque!");
        }

        if (EstaEmXeque(Adversaria(JogadorAtual)))
        {
            Xeque = true;
        }
        else
        {
            Xeque = false;
        }
        
        Turno++;
        MudaJogador();
    }

    public void ValidarPosicaoDeOrigem(Posicao pos)
    {
        if (Tab.Peca(pos) == null)
        {
            throw new TabuleiroException("Não existe peça na posição de origem escolhida");
        }

        if (JogadorAtual != Tab.Peca(pos).Cor)
        {
            throw new TabuleiroException("A peça de origem escolhida não é sua!");
        }

        if (!Tab.Peca(pos).ExisteMovimentosPossiveis())
        {
            throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
        }
   }

    public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
    {
        if (!Tab.Peca(origem).PodeMoverPara(destino))
        {
            throw new TabuleiroException("Posição de destino inválida!");
        }
    }

    private void MudaJogador()
    {
        JogadorAtual = (JogadorAtual == Cor.Branca) ? Cor.Preta : Cor.Branca;
    }

    public HashSet<Peca> PecasCapturadas(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca>();

        foreach (Peca peca in Capturadas)
        {
            if (peca.Cor == cor)
            {
                aux.Add(peca);
            }
        }

        return aux;
    }

    public HashSet<Peca> PecasEmJogo(Cor cor)
    {
        HashSet<Peca> aux = new HashSet<Peca>();

        foreach (Peca peca in Pecas)
        {
            if (peca.Cor == cor)
            {
                aux.Add(peca);
            }
        }

       aux.ExceptWith(PecasCapturadas(cor));
       return aux;

    }

    private Cor Adversaria(Cor cor)
    {
        return (cor == Cor.Branca) ? Cor.Preta : Cor.Branca;
    }

    private Peca Rei(Cor cor)
    {
        foreach (Peca peca in PecasEmJogo(cor))
        {
            if (peca is Rei)
            {
                return peca;
            }
        }

        return null;
    }

    public bool EstaEmXeque(Cor cor)
    {
        Peca R = Rei(cor);
        if (R == null)
        {
            throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro!");
        }
        
        foreach (Peca peca in PecasEmJogo(Adversaria(cor)))
        {
            bool[,] mat = peca.MovimentosPossiveis();
            if (mat[R.Posicao.Linha, R.Posicao.Coluna])
            {
                return true;
            }
        }

        return false;
    }

    public void ColocarNovaPeca(char coluna, int linha, Peca peca)
    {
        Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
        Pecas.Add(peca);
    }

    private void ColocarPecas()
    {
        ColocarNovaPeca('c', 1, new Torre(Tab, Cor.Branca));
        ColocarNovaPeca('c', 2, new Torre(Tab, Cor.Branca));
        ColocarNovaPeca('d', 2, new Torre(Tab, Cor.Branca));
        ColocarNovaPeca('e', 2, new Torre(Tab, Cor.Branca));
        ColocarNovaPeca('e', 1, new Torre(Tab, Cor.Branca));
        ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Branca));
        
        
        ColocarNovaPeca('c', 7, new Torre(Tab, Cor.Preta));
        ColocarNovaPeca('c', 8, new Torre(Tab, Cor.Preta));
        ColocarNovaPeca('d', 7, new Torre(Tab, Cor.Preta));
        ColocarNovaPeca('e', 7, new Torre(Tab, Cor.Preta));
        ColocarNovaPeca('e', 8, new Torre(Tab, Cor.Preta));
        ColocarNovaPeca('d', 8, new Rei(Tab, Cor.Preta));
    }
}