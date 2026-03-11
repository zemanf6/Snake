namespace Snake;

/// <summary>
/// Represents a position within the game area.
/// </summary>
/// <param name="X">The X-axis coordinate.</param>
/// <param name="Y">The Y-axis coordinate.</param>
internal readonly record struct Position(int X, int Y)
{
    /// <summary>
    /// Returns a new position translated by the specified offsets.
    /// </summary>
    /// <param name="deltaX">The offset on the X axis.</param>
    /// <param name="deltaY">The offset on the Y axis.</param>
    /// <returns>A new translated position.</returns>
    public Position Translate(int deltaX, int deltaY)
    {
        return new Position(X + deltaX, Y + deltaY);
    }
}