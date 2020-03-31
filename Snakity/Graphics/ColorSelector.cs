using System;
using System.Linq;

namespace Snakity.Graphics
{
    internal static class ColorSelector
    {
        private static readonly char[] WallChars =
        {
            SpecialChars.Wall.Up, SpecialChars.Wall.Down, SpecialChars.Wall.Left,
            SpecialChars.Wall.Right, SpecialChars.Wall.DownLeft, SpecialChars.Wall.DownRight,
            SpecialChars.Wall.LeftRight, SpecialChars.Wall.UpDown, SpecialChars.Wall.UpLeft,
            SpecialChars.Wall.UpRight, SpecialChars.Wall.DownLeftRight, SpecialChars.Wall.UpDownLeft,
            SpecialChars.Wall.UpDownRight, SpecialChars.Wall.UpLeftRight, SpecialChars.Wall.UpDownLeftRight,
            '#'
        };

        private static readonly char[] PlayerChars =
        {
            SpecialChars.Player.Up, SpecialChars.Player.Down, SpecialChars.Player.Left,
            SpecialChars.Player.Right, SpecialChars.Player.DownLeft, SpecialChars.Player.DownRight,
            SpecialChars.Player.LeftRight, SpecialChars.Player.UpDown, SpecialChars.Player.UpLeft,
            SpecialChars.Player.UpRight, SpecialChars.Player.DownLeftRight, SpecialChars.Player.UpDownLeft,
            SpecialChars.Player.UpDownRight, SpecialChars.Player.UpLeftRight, SpecialChars.Player.UpDownLeftRight,
            'P'
        };

        public static ConsoleColor Get(char c)
        {
            if (PlayerChars.Contains(c))
                return ConsoleColor.Green;
            if (WallChars.Contains(c) || c == '#')
                return ConsoleColor.Gray;
            return c switch
            {
                SpecialChars.Enemy => ConsoleColor.Yellow,
                _ => ConsoleColor.White
            };
        }
    }
}