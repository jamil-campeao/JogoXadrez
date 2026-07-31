using tabuleiro;
namespace xadrez;

public class PosicaoXadrez
{
    public char Coluna { get; set; }
    public int Linha { get; set; }

    public PosicaoXadrez(char coluna, int linha)
    {
        Linha = linha;
        Coluna = char.ToUpper(coluna);
    }

    public Posicao ToPosicao()
    {
        return new Posicao(8 - Linha, Coluna - 'A');
    }

    public override string ToString()
    {
        return $"{Coluna}{Linha}";
    }
}