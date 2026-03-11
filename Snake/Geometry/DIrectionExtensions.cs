namespace Snake;

/// <summary>
/// Provides helper methods for working with movement directions.
/// </summary>
internal static class DirectionExtensions
{
    /// <summary>
    /// Converts a direction into its corresponding coordinate offset.
    /// </summary>
    /// <param name="direction">The movement direction.</param>
    /// <returns>The offset on the X and Y axes.</returns>
    public static Position ToOffset(this Direction direction)
    {
        return direction switch
        {
            Direction.Up => new Position(0, -1),
            Direction.Down => new Position(0, 1),
            Direction.Left => new Position(-1, 0),
            Direction.Right => new Position(1, 0),
            _ => new Position(0, 0)
        };
    }

    /// <summary>
    /// Determines whether two directions are opposite to each other.
    /// </summary>
    /// <param name="current">The current direction.</param>
    /// <param name="next">The next direction.</param>
    /// <returns>
    /// <see langword="true"/> if the directions are opposite; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsOppositeTo(this Direction current, Direction next)
    {
        return (current == Direction.Up && next == Direction.Down) ||
               (current == Direction.Down && next == Direction.Up) ||
               (current == Direction.Left && next == Direction.Right) ||
               (current == Direction.Right && next == Direction.Left);
    }
}