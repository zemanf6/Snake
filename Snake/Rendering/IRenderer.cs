namespace Snake;

/// <summary>
/// Defines rendering of the current game state.
/// </summary>
internal interface IRenderer
{
    /// <summary>
    /// Renders the current game scene.
    /// </summary>
    /// <param name="head">The position of the snake's head.</param>
    /// <param name="bodySegments">The positions of the individual body segments.</param>
    /// <param name="food">The position of the food.</param>
    void Render(Position head, IReadOnlyCollection<Position> bodySegments, Position food);

    /// <summary>
    /// Displays the game over screen.
    /// </summary>
    /// <param name="snakeLength">The final length of the snake.</param>
    void ShowGameOver(int snakeLength);
}