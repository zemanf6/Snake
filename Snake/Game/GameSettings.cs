namespace Snake;

/// <summary>
/// Stores the basic game configuration.
/// </summary>
internal sealed class GameSettings
{
    /// <summary>
    /// Initializes a new instance of the game settings.
    /// </summary>
    /// <param name="screenWidth">The width of the game area in characters.</param>
    /// <param name="screenHeight">The height of the game area in characters.</param>
    /// <param name="tickMilliseconds">The duration of a single game tick in milliseconds.</param>
    /// <param name="initialSnakeLength">The initial length of the snake.</param>
    public GameSettings(
        int screenWidth,
        int screenHeight,
        int tickMilliseconds,
        int initialSnakeLength)
    {
        if (screenWidth < 10)
        {
            throw new ArgumentOutOfRangeException(nameof(screenWidth), "Screen width must be at least 10.");
        }

        if (screenHeight < 10)
        {
            throw new ArgumentOutOfRangeException(nameof(screenHeight), "Screen height must be at least 10.");
        }

        if (tickMilliseconds <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tickMilliseconds), "Tick duration must be greater than 0.");
        }

        if (initialSnakeLength < 2)
        {
            throw new ArgumentOutOfRangeException(nameof(initialSnakeLength), "Initial snake length must be at least 2.");
        }

        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;
        TickMilliseconds = tickMilliseconds;
        InitialSnakeLength = initialSnakeLength;
    }

    /// <summary>
    /// Gets the width of the game area.
    /// </summary>
    public int ScreenWidth { get; }

    /// <summary>
    /// Gets the height of the game area.
    /// </summary>
    public int ScreenHeight { get; }

    /// <summary>
    /// Gets the duration of a single game tick.
    /// </summary>
    public int TickMilliseconds { get; }

    /// <summary>
    /// Gets the initial length of the snake.
    /// </summary>
    public int InitialSnakeLength { get; }
}