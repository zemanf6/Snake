namespace Snake;

/// <summary>
/// Represents the main game logic of the console-based Snake game.
/// </summary>
internal sealed class SnakeGame
{
    private readonly GameSettings _settings;
    private readonly IRenderer _renderer;
    private readonly IInputReader _inputReader;
    private readonly Random _random;
    private readonly List<Position> _bodySegments = [];

    private Position _head;
    private Position _food;
    private Direction _direction = Direction.Right;
    private int _snakeLength;

    /// <summary>
    /// Initializes a new instance of the game.
    /// </summary>
    /// <param name="settings">The game configuration.</param>
    /// <param name="renderer">The service responsible for rendering the current game state.</param>
    /// <param name="inputReader">The service responsible for reading user input.</param>
    /// <param name="random">The random number generator.</param>
    public SnakeGame(
        GameSettings settings,
        IRenderer renderer,
        IInputReader inputReader,
        Random random)
    {
        ArgumentNullException.ThrowIfNull(settings);
        ArgumentNullException.ThrowIfNull(renderer);
        ArgumentNullException.ThrowIfNull(inputReader);
        ArgumentNullException.ThrowIfNull(random);

        _settings = settings;
        _renderer = renderer;
        _inputReader = inputReader;
        _random = random;

        _snakeLength = _settings.InitialSnakeLength;
        _head = new Position(_settings.ScreenWidth / 2, _settings.ScreenHeight / 2);

        InitializeSnake();
        _food = GenerateFoodPosition();
    }

    /// <summary>
    /// Starts the main game loop.
    /// </summary>
    public void Run()
    {
        _renderer.Render(_head, _bodySegments, _food);

        while (true)
        {
            _direction = _inputReader.ReadDirectionForTick(_direction);

            Position nextHead = GetNextHeadPosition();
            bool growsOnThisMove = nextHead == _food;

            if (IsWallCollision(nextHead) || IsSelfCollision(nextHead, growsOnThisMove))
            {
                break;
            }

            MoveSnake(nextHead, growsOnThisMove);
            _renderer.Render(_head, _bodySegments, _food);
        }

        _renderer.ShowGameOver(_snakeLength);
    }

    private void InitializeSnake()
    {
        for (int offset = _snakeLength - 1; offset >= 1; offset--)
        {
            _bodySegments.Add(new Position(_head.X - offset, _head.Y));
        }
    }

    private Position GetNextHeadPosition()
    {
        Position offset = _direction.ToOffset();
        return _head.Translate(offset.X, offset.Y);
    }

    private void MoveSnake(Position nextHead, bool growsOnThisMove)
    {
        _bodySegments.Add(_head);
        _head = nextHead;

        if (growsOnThisMove)
        {
            _snakeLength++;
            _food = GenerateFoodPosition();
        }

        TrimBodyToCurrentLength();
    }

    private void TrimBodyToCurrentLength()
    {
        while (_bodySegments.Count > _snakeLength - 1)
        {
            _bodySegments.RemoveAt(0);
        }
    }

    private bool IsWallCollision(Position position)
    {
        return position.X <= 0 ||
               position.X >= _settings.ScreenWidth - 1 ||
               position.Y <= 0 ||
               position.Y >= _settings.ScreenHeight - 1;
    }

    private bool IsSelfCollision(Position nextHead, bool growsOnThisMove)
    {
        int firstSegmentIndexToCheck = growsOnThisMove ? 0 : 1;

        if (firstSegmentIndexToCheck >= _bodySegments.Count)
        {
            return false;
        }

        for (int i = firstSegmentIndexToCheck; i < _bodySegments.Count; i++)
        {
            if (_bodySegments[i] == nextHead)
            {
                return true;
            }
        }

        return false;
    }

    private Position GenerateFoodPosition()
    {
        Position candidate;

        do
        {
            candidate = new Position(
                _random.Next(1, _settings.ScreenWidth - 1),
                _random.Next(1, _settings.ScreenHeight - 1));
        }
        while (candidate == _head || _bodySegments.Contains(candidate));

        return candidate;
    }
}