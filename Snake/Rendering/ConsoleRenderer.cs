namespace Snake;

/// <summary>
/// Renders the game to the console.
/// </summary>
internal sealed class ConsoleRenderer : IRenderer
{
    private const char Block = '■';

    private readonly int _screenWidth;
    private readonly int _screenHeight;

    /// <summary>
    /// Initializes a new instance of the console renderer.
    /// </summary>
    /// <param name="screenWidth">The width of the game area.</param>
    /// <param name="screenHeight">The height of the game area.</param>
    public ConsoleRenderer(int screenWidth, int screenHeight)
    {
        _screenWidth = screenWidth;
        _screenHeight = screenHeight;
    }

    /// <summary>
    /// Renders the border, the snake, and the food.
    /// </summary>
    /// <param name="head">The position of the snake's head.</param>
    /// <param name="bodySegments">The positions of the snake's body segments.</param>
    /// <param name="food">The position of the food.</param>
    public void Render(Position head, IReadOnlyCollection<Position> bodySegments, Position food)
    {
        Console.Clear();

        DrawBorder();
        DrawBody(bodySegments);
        DrawHead(head);
        DrawFood(food);

        Console.ResetColor();
    }

    /// <summary>
    /// Displays the final message after the game ends.
    /// </summary>
    /// <param name="snakeLength">The final length of the snake.</param>
    public void ShowGameOver(int snakeLength)
    {
        Console.ResetColor();

        int x = Math.Max(1, _screenWidth / 5);
        int y = _screenHeight / 2;

        Console.SetCursorPosition(x, y);
        Console.Write($"Game over, score: {snakeLength}");
    }

    private void DrawBorder()
    {
        Console.ForegroundColor = ConsoleColor.White;

        for (int x = 0; x < _screenWidth; x++)
        {
            Console.SetCursorPosition(x, 0);
            Console.Write(Block);

            Console.SetCursorPosition(x, _screenHeight - 1);
            Console.Write(Block);
        }

        for (int y = 0; y < _screenHeight; y++)
        {
            Console.SetCursorPosition(0, y);
            Console.Write(Block);

            Console.SetCursorPosition(_screenWidth - 1, y);
            Console.Write(Block);
        }
    }

    private static void DrawBody(IEnumerable<Position> bodySegments)
    {
        Console.ForegroundColor = ConsoleColor.Green;

        foreach (Position segment in bodySegments)
        {
            Console.SetCursorPosition(segment.X, segment.Y);
            Console.Write(Block);
        }
    }

    private static void DrawHead(Position head)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(head.X, head.Y);
        Console.Write(Block);
    }

    private static void DrawFood(Position food)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(food.X, food.Y);
        Console.Write(Block);
    }
}