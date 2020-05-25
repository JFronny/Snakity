using System;
using System.Linq;
using CC_Functions.Commandline.TUI;
using OneLineSimple = CC_Functions.Misc.SpecialChars.OneLineSimple;
using TwoLineSimple = CC_Functions.Misc.SpecialChars.TwoLineSimple;

namespace Snakity.Graphics
{
    internal static class ColorSelector
    {
        private static readonly char[] WallChars =
        {
            TwoLineSimple.Up, TwoLineSimple.Down, TwoLineSimple.Left,
            TwoLineSimple.Right, TwoLineSimple.DownLeft, TwoLineSimple.DownRight,
            TwoLineSimple.LeftRight, TwoLineSimple.UpDown, TwoLineSimple.UpLeft,
            TwoLineSimple.UpRight, TwoLineSimple.DownLeftRight, TwoLineSimple.UpDownLeft,
            TwoLineSimple.UpDownRight, TwoLineSimple.UpLeftRight, TwoLineSimple.UpDownLeftRight,
            '#'
        };

        private static readonly char[] PlayerChars =
        {
            OneLineSimple.Up, OneLineSimple.Down, OneLineSimple.Left,
            OneLineSimple.Right, OneLineSimple.DownLeft, OneLineSimple.DownRight,
            OneLineSimple.LeftRight, OneLineSimple.UpDown, OneLineSimple.UpLeft,
            OneLineSimple.UpRight, OneLineSimple.DownLeftRight, OneLineSimple.UpDownLeft,
            OneLineSimple.UpDownRight, OneLineSimple.UpLeftRight, OneLineSimple.UpDownLeftRight,
            'P'
        };

        public static Pixel Get(char c)
        {
            Pixel p = new Pixel(c);
            if (PlayerChars.Contains(c))
                p.ForeColor = ConsoleColor.Green;
            else if (WallChars.Contains(c) || c == '#')
                p.ForeColor = ConsoleColor.Gray;
            else p.ForeColor = c switch
            {
                SpecialChars.Enemy => ConsoleColor.Yellow,
                _ => ConsoleColor.White
            };
            return p;
        }

        public static Pixel[,] Get(char[,] c)
        {
            int w = c.GetLength(0);
            int h = c.GetLength(1);
            Pixel[,] output = new Pixel[w, h];
            for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
                output[x, y] = Get(c[x, y]);
            return output;
        }
    }
}