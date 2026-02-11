using System;
using System.Collections.Generic;

namespace Snake
{
    internal sealed class SnakeGame
    {
        private readonly int _screenWidth;
        private readonly int _screenHeight;

        private readonly Random _random = new Random();
        private readonly Renderer _renderer;
        private readonly InputReader _input;

        private int _snakeLength = 5;
        private bool _isGameOver;

        private Position _head;
        private Direction _direction = Direction.Right;
        private readonly List<Position> _bodySegments = new List<Position>();
        private Position _food;

        public SnakeGame(int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;

            _renderer = new Renderer(_screenWidth, _screenHeight);
            _input = new InputReader();

            _head = new Position(_screenWidth / 2, _screenHeight / 2)
            {
                Color = ConsoleColor.Red
            };

            _food = RandomInnerPosition();
        }

        public void Run()
        {
            RunGameLoop();
            _renderer.ShowGameOver(_snakeLength);
        }

        private void RunGameLoop()
        {
            while (true)
            {
                _renderer.RenderFrame(_head, _bodySegments, _food, out bool selfCollision);

                if (IsWallCollision(_head) || selfCollision)
                {
                    _isGameOver = true;
                }

                if (_isGameOver)
                    break;

                if (TryEatFood())
                {
                    _snakeLength++;
                }

                _direction = _input.ReadDirectionForTick(_direction);

                AdvanceSnake();

                TrimBody();
            }
        }

        private bool TryEatFood()
        {
            if (_food.X != _head.X || _food.Y != _head.Y)
                return false;

            _food = RandomInnerPosition();
            return true;
        }

        private void AdvanceSnake()
        {
            _bodySegments.Add(new Position(_head.X, _head.Y));

            switch (_direction)
            {
                case Direction.Up:
                    _head.Y--;
                    break;
                case Direction.Down:
                    _head.Y++;
                    break;
                case Direction.Left:
                    _head.X--;
                    break;
                case Direction.Right:
                    _head.X++;
                    break;
            }
        }

        private void TrimBody()
        {
            if (_bodySegments.Count > _snakeLength)
            {
                _bodySegments.RemoveAt(0);
            }
        }

        private bool IsWallCollision(Position head)
        {
            return head.X == 0 ||
                   head.X == _screenWidth - 1 ||
                   head.Y == 0 ||
                   head.Y == _screenHeight - 1;
        }

        private Position RandomInnerPosition()
        {
            return new Position(
                _random.Next(1, _screenWidth - 2),
                _random.Next(1, _screenHeight - 2));
        }
    }
}
