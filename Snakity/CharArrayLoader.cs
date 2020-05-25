using System;
using System.Linq;
using Snakity.Graphics;
using CC_Functions.Misc;
using SpecialChars = CC_Functions.Misc.SpecialChars;

namespace Snakity
{
    public static class CharArrayLoader
    {
        public static Tuple<char[,], bool[,]> LoadLevel(string level, bool smooth)
        {
            char[,] content = level.ToNdArray2D();
            int width = content.GetLength(0);
            int height = content.GetLength(1);
            bool[,] spawn = new bool[width, height];
            bool[,] walls = new bool[width, height];
            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                switch (content[x, y])
                {
                    case ' ':
                        content[x, y] = SpecialChars.Empty;
                        break;
                    case '.':
                        content[x, y] = SpecialChars.Empty;
                        spawn[x, y] = true;
                        break;
                    case '#':
                        walls[x, y] = true;
                        char selected;
                        //Determine adjacent blocks
                        /*bool up = y > 0 && (walls[x, y - 1] || content[x, y - 1] == '#');
                        bool down = y < height - 1 && (walls[x, y + 1] || content[x, y + 1] == '#');
                        bool left = x > 0 && (walls[x - 1, y] || content[x - 1, y] == '#');
                        bool right = x < width - 1 && (walls[x + 1, y] || content[x + 1, y] == '#');*/
                        bool left = y > 0 && (walls[x, y - 1] || content[x, y - 1] == '#');
                        bool right = y < height - 1 && (walls[x, y + 1] || content[x, y + 1] == '#');
                        bool up = x > 0 && (walls[x - 1, y] || content[x - 1, y] == '#');
                        bool down = x < width - 1 && (walls[x + 1, y] || content[x + 1, y] == '#');
                        //figure out char
                        if (smooth)
                            if (up)
                            {
                                if (down)
                                {
                                    if (left)
                                        selected = right
                                            ? SpecialChars.TwoLineSimple.UpDownLeftRight
                                            : SpecialChars.TwoLineSimple.UpDownLeft;
                                    else
                                        selected = right ? SpecialChars.TwoLineSimple.UpDownRight : SpecialChars.TwoLineSimple.UpDown;
                                }
                                else
                                {
                                    if (left)
                                        selected = right ? SpecialChars.TwoLineSimple.UpLeftRight : SpecialChars.TwoLineSimple.UpLeft;
                                    else
                                        selected = right ? SpecialChars.TwoLineSimple.UpRight : SpecialChars.TwoLineSimple.Up;
                                }
                            }
                            else
                            {
                                if (down)
                                {
                                    if (left)
                                        selected = right ? SpecialChars.TwoLineSimple.DownLeftRight : SpecialChars.TwoLineSimple.DownLeft;
                                    else
                                        selected = right ? SpecialChars.TwoLineSimple.DownRight : SpecialChars.TwoLineSimple.Down;
                                }
                                else
                                {
                                    if (left)
                                        selected = right ? SpecialChars.TwoLineSimple.LeftRight : SpecialChars.TwoLineSimple.Left;
                                    else
                                        selected = right ? SpecialChars.TwoLineSimple.Right : '#';
                                }
                            }
                        else
                            selected = '#';
                        //yay
                        content[x, y] = selected;
                        break;
                }

            return new Tuple<char[,], bool[,]>(content, spawn);
        }
    }
}