using tabuleiro;
using xadrez;
namespace JogoXadrez;

public class Tela
{
    public static void ImprimirPartida(PartidaDeXadrez partida)
    {
        ImprimirTabuleiro(partida.Tab);
        Console.WriteLine();
        ImprimirPecasCapturadas(partida);
        Console.WriteLine();

        Console.WriteLine($"Turno: {partida.Turno}");
        Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");

    }
    
    
    public static void ImprimirTabuleiro(Tabuleiro tab)
    {
        for (int i = 0; i < tab.Linha; i++)
        {
            Console.Write($"{8 - i} ");
            for (int j = 0; j < tab.Colunas; j++)
            {
                ImprimirPeca(tab.Peca(i, j));
            }
            
            Console.WriteLine();
        }
        
        Console.WriteLine("  A B C D E F G H");
    }
    
    public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
    {
        ConsoleColor fundoOriginal = Console.BackgroundColor;
        ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

        for (int i = 0; i < tab.Linha; i++) {
            Console.Write(8 - i + " ");
            for (int j = 0; j < tab.Colunas; j++) {
                if (posicoesPossiveis[i, j]) {
                    Console.BackgroundColor = fundoAlterado;
                }
                else {
                    Console.BackgroundColor = fundoOriginal;
                }
                ImprimirPeca(tab.Peca(i, j));
                Console.BackgroundColor = fundoOriginal;
            }
            Console.WriteLine();
        }
        Console.WriteLine("  A B C D E F G H");
        Console.BackgroundColor = fundoOriginal;
    }

    public static void ImprimirPeca(Peca peca)
    {
        if (peca == null)
        {
            Console.Write("- ");
        }
        else
        {
            if (peca.Cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);

                Console.ForegroundColor = aux;
            }
            Console.Write(" ");
        }
    }

    public static PosicaoXadrez LerPosicaoXadrez()
    {
        string s = Console.ReadLine();

        char coluna = s[0];
        int linha = int.Parse(s[1] + "");
        return new PosicaoXadrez(coluna, linha);
    }

    public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
    {
        Console.WriteLine("Peça capturadas: ");
        Console.Write("Brancas: ");
        ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
        Console.WriteLine();
        Console.Write("Pretas: ");
        ConsoleColor aux = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
        Console.ForegroundColor = aux;
        Console.WriteLine();
    }

    public static void ImprimirConjunto(HashSet<Peca> conjunto)
    {
        Console.Write("[");
        foreach (Peca peca in conjunto)
        {
            Console.Write($"{peca} ");
        }

        Console.Write("]");
    }
}