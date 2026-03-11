namespace Snake;

/// <summary>
/// Defines reading the snake movement direction during a single game tick.
/// </summary>
internal interface IInputReader
{
    /// <summary>
    /// Reads the movement direction for the next tick.
    /// </summary>
    /// <param name="currentDirection">The current snake movement direction.</param>
    /// <returns>The new or unchanged movement direction.</returns>
    Direction ReadDirectionForTick(Direction currentDirection);
}