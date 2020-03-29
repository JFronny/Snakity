using System;

namespace Snakity.Graphics
{
    static class ColorSelector
    {
        public static ConsoleColor Get(char c)
        {
            return c switch
            {
                SpecialChars.Player => ConsoleColor.Green,
                SpecialChars.Wall => ConsoleColor.Gray,
                SpecialChars.Enemy => ConsoleColor.Yellow,
                _ => ConsoleColor.White
            };
        }
    }
}
