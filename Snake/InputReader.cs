using System;

namespace Snake
{
    internal sealed class InputReader
    {
        private const int TickMilliseconds = 500;

        public Direction ReadDirectionForTick(Direction direction)
        {
            DateTime tickStart = DateTime.Now;
            bool directionChangedThisTick = false;

            while (true)
            {
                DateTime now = DateTime.Now;
                if (now.Subtract(tickStart).TotalMilliseconds > TickMilliseconds)
                    break;

                if (!Console.KeyAvailable)
                    continue;

                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                if (directionChangedThisTick ||
                    !TryMapKeyToDirection(keyInfo.Key, out Direction nextDirection) ||
                    IsOpposite(direction, nextDirection))
                {
                    continue;
                }

                direction = nextDirection;
                directionChangedThisTick = true;
            }

            return direction;
        }

        private static bool TryMapKeyToDirection(ConsoleKey key, out Direction direction)
        {
            direction = key switch
            {
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                _ => default
            };

            return key is ConsoleKey.UpArrow
                or ConsoleKey.DownArrow
                or ConsoleKey.LeftArrow
                or ConsoleKey.RightArrow;
        }

        private static bool IsOpposite(Direction current, Direction next)
        {
            return (current == Direction.Up && next == Direction.Down) ||
                   (current == Direction.Down && next == Direction.Up) ||
                   (current == Direction.Left && next == Direction.Right) ||
                   (current == Direction.Right && next == Direction.Left);
        }
    }
}
