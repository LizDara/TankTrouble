using Grafica.Window;

namespace Grafica
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(700, 600, "Game"))
            {
                game.Run(60.0);
            }
        }
    }
}
