using System;

namespace Snake
{
    internal sealed class Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
    }
}
