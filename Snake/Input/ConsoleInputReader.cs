using System.Diagnostics;

namespace Snake;

/// <summary>
/// Reads the snake movement direction from the console.
/// </summary>
internal sealed class ConsoleInputReader : IInputReader
{
    private readonly TimeSpan _tickDuration;

    /// <summary>
    /// Initializes a new instance of the console input reader.
    /// </summary>
    /// <param name="tickMilliseconds">The duration of a single tick in milliseconds.</param>
    public ConsoleInputReader(int tickMilliseconds)
    {
        if (tickMilliseconds <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(tickMilliseconds), "Tick duration must be greater than 0.");
        }

        _tickDuration = TimeSpan.FromMilliseconds(tickMilliseconds);
    }

    /// <summary>
    /// Reads user input during a single game tick and returns the resulting direction.
    /// </summary>
    /// <param name="currentDirection">The current snake movement direction.</param>
    /// <returns>The direction used in the next game step.</returns>
    public Direction ReadDirectionForTick(Direction currentDirection)
    {
        var stopwatch = Stopwatch.StartNew();
        Direction nextDirection = currentDirection;
        bool directionChangedThisTick = false;

        while (stopwatch.Elapsed < _tickDuration)
        {
            if (!Console.KeyAvailable)
            {
                Thread.Sleep(1);
                continue;
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

            if (directionChangedThisTick)
            {
                continue;
            }

            Direction? candidateDirection = MapKeyToDirection(keyInfo.Key);
            if (!candidateDirection.HasValue)
            {
                continue;
            }

            if (currentDirection.IsOppositeTo(candidateDirection.Value))
            {
                continue;
            }

            nextDirection = candidateDirection.Value;
            directionChangedThisTick = true;
        }

        return nextDirection;
    }

    /// <summary>
    /// Maps an arrow key to its corresponding movement direction.
    /// </summary>
    /// <param name="key">The pressed key.</param>
    /// <returns>The corresponding direction, or <see langword="null"/> if the key is not supported.</returns>
    private static Direction? MapKeyToDirection(ConsoleKey key)
    {
        return key switch
        {
            ConsoleKey.UpArrow => Direction.Up,
            ConsoleKey.DownArrow => Direction.Down,
            ConsoleKey.LeftArrow => Direction.Left,
            ConsoleKey.RightArrow => Direction.Right,
            _ => null
        };
    }
}