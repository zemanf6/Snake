using System;

namespace Snake
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;

            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;

            var game = new SnakeGame(screenWidth, screenHeight);
            game.Run();
        }
    }
}
