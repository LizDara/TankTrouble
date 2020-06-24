using Grafica.Window;

namespace Grafica
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(900, 650, "Game"))
            {
                game.Run(60.0);
            }
        }
    }
}
