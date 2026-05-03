using tabuleiro;
using xadrez;
namespace JogoXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 6));
                tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 2));
                tab.ColocarPeca(new Rei(tab, Cor.Branca), new Posicao(3, 5));
            
                Tela.ImprimirTabuleiro(tab);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

