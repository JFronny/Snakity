using System;
using System.Linq;
using Snakity.Graphics;

namespace Snakity
{
    public static class CharArrayLoader
    {
        public static Tuple<char[,], bool[,]> LoadLevel(string level, bool smooth)
        {
            char[,] content = Load(level);
            int width = content.GetLength(0);
            int height = content.GetLength(1);
            bool[,] spawn = new bool[width, height];
            bool[,] walls = new bool[width, height];
            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                switch (content[x, y])
                {
                    case ' ':
                        content[x, y] = SpecialChars.Space;
                        break;
                    case '.':
                        content[x, y] = SpecialChars.Space;
                        spawn[x, y] = true;
                        break;
                    case '#':
                        walls[x, y] = true;
                        char selected;
                        //Determine adjacent blocks
                        bool up = y > 0 && (walls[x, y - 1] || content[x, y - 1] == '#');
                        bool down = y < height - 1 && (walls[x, y + 1] || content[x, y + 1] == '#');
                        bool left = x > 0 && (walls[x - 1, y] || content[x - 1, y] == '#');
                        bool right = x < width - 1 && (walls[x + 1, y] || content[x + 1, y] == '#');
                        //figure out char
                        if (smooth)
                            if (up)
                            {
                                if (down)
                                {
                                    if (left)
                                        selected = right
                                            ? SpecialChars.Wall.UpDownLeftRight
                                            : SpecialChars.Wall.UpDownLeft;
                                    else
                                        selected = right ? SpecialChars.Wall.UpDownRight : SpecialChars.Wall.UpDown;
                                }
                                else
                                {
                                    if (left)
                                        selected = right ? SpecialChars.Wall.UpLeftRight : SpecialChars.Wall.UpLeft;
                                    else
                                        selected = right ? SpecialChars.Wall.UpRight : SpecialChars.Wall.Up;
                                }
                            }
                            else
                            {
                                if (down)
                                {
                                    if (left)
                                        selected = right ? SpecialChars.Wall.DownLeftRight : SpecialChars.Wall.DownLeft;
                                    else
                                        selected = right ? SpecialChars.Wall.DownRight : SpecialChars.Wall.Down;
                                }
                                else
                                {
                                    if (left)
                                        selected = right ? SpecialChars.Wall.LeftRight : SpecialChars.Wall.Left;
                                    else
                                        selected = right ? SpecialChars.Wall.Right : '#';
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

        public static char[,] Load(string text)
        {
            string[] levelArr = text.Split('\n');
            int width = levelArr.Select(s => s.Length).OrderBy(s => s).Last();
            int height = levelArr.Length;
            char[,] tmp = new char[width, height];
            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                tmp[x, y] = SpecialChars.Space;
            for (int i = 0; i < levelArr.Length; i++)
            {
                string s = levelArr[i];
                for (int j = 0; j < s.Length; j++) tmp[j, i] = s[j];
            }

            return tmp;
        }
    }
}