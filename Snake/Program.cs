namespace Snake;

/// <summary>
/// Entrypoint of the console-based Snake game.
/// </summary>
internal static class Program
{
    private const int ScreenWidth = 32;
    private const int ScreenHeight = 16;
    private const int TickMilliseconds = 500;
    private const int InitialSnakeLength = 5;

    /// <summary>
    /// Runs the Snake game. Initializes the game settings, renderer, and input reader, then starts the game loop.
    /// </summary>
    private static void Main()
    {
        var settings = new GameSettings(
            screenWidth: ScreenWidth,
            screenHeight: ScreenHeight,
            tickMilliseconds: TickMilliseconds,
            initialSnakeLength: InitialSnakeLength);

        Console.CursorVisible = false;
        Console.Title = "Snake";

        try
        {
            IRenderer renderer = new ConsoleRenderer(settings.ScreenWidth, settings.ScreenHeight);
            IInputReader inputReader = new ConsoleInputReader(settings.TickMilliseconds);

            var game = new SnakeGame(settings, renderer, inputReader, new Random());
            game.Run();
        }
        finally
        {
            Console.ResetColor();
            Console.CursorVisible = true;
        }
    }
}