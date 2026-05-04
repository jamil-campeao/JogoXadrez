namespace tabuleiro;

public abstract class Peca
{
    public Posicao Posicao { get; set; }
    public Cor Cor { get; protected set; }
    public int QtdMovimentos { get; protected set; }
    public Tabuleiro Tab { get; protected set; }

    public Peca(Cor cor, Tabuleiro tab)
    {
        Posicao = null;
        Cor = cor;
        QtdMovimentos = 0;
        Tab = tab;
    }

    public void IncrementarQtdeMovimentos()
    {
        QtdMovimentos++;
    }
    
    public void DecrementarQtdeMovimentos()
    {
        QtdMovimentos--;
    }

    public bool ExisteMovimentosPossiveis()
    {
        bool[,] mat = MovimentosPossiveis();

        for (int i = 0; i < Tab.Linha; i++)
        {
            for (int j = 0; j < Tab.Colunas; j++)
            {
                if (mat[i, j])
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool MovimentoPossivel(Posicao pos)
    {
        return MovimentosPossiveis()[pos.Linha, pos.Coluna];
    }

    public abstract bool[,] MovimentosPossiveis();
}