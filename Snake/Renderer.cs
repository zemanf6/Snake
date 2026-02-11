using System;
using System.Collections.Generic;

namespace Snake
{
    internal sealed class Renderer
    {
        private const char Block = '■';

        private readonly int _screenWidth;
        private readonly int _screenHeight;

        public Renderer(int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
        }

        public void RenderFrame(
            Position head,
            List<Position> bodySegments,
            Position food,
            out bool selfCollision)
        {
            Console.Clear();
            DrawBorder();

            selfCollision = DrawBodyAndDetectCollision(bodySegments, head);

            DrawHead(head);
            DrawFood(food);
        }

        public void ShowGameOver(int snakeLength)
        {
            Console.ResetColor();
            Console.SetCursorPosition(_screenWidth / 5, _screenHeight / 2);
            Console.WriteLine("Game over, Score: " + snakeLength);
            Console.SetCursorPosition(_screenWidth / 5, _screenHeight / 2 + 1);
        }

        private void DrawBorder()
        {
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

        private bool DrawBodyAndDetectCollision(List<Position> bodySegments, Position head)
        {
            bool collision = false;
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < bodySegments.Count; i++)
            {
                Position segment = bodySegments[i];
                Console.SetCursorPosition(segment.X, segment.Y);
                Console.Write(Block);

                if (segment.X == head.X && segment.Y == head.Y)
                {
                    collision = true;
                }
            }

            return collision;
        }

        private void DrawHead(Position head)
        {
            Console.SetCursorPosition(head.X, head.Y);
            Console.ForegroundColor = head.Color;
            Console.Write(Block);
        }

        private void DrawFood(Position food)
        {
            Console.SetCursorPosition(food.X, food.Y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(Block);
        }
    }
}
