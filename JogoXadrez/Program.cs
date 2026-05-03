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
                PosicaoXadrez pos = new PosicaoXadrez('a', 1);
                PosicaoXadrez pos2 = new PosicaoXadrez('c', 7);

                Console.WriteLine(pos.ToPosicao());
                Console.WriteLine(pos2.ToPosicao());
                Console.WriteLine(pos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

