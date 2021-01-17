using System;
using System.Threading;
using Logic;

namespace UIConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        static void Run()
        {
            var game = new Game();
            game.CreateGrid(25);

            while (true)
            {
                Console.Clear();

                game.NextGeneration();
                Console.Write(game.ShowGrid());
                Console.WriteLine($"Genreation: {game.GenerationCount}\n");

                Thread.Sleep(1000);
            }
        }
    }
}
