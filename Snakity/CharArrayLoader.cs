using System;
using System.Linq;
using Snakity.Graphics;

namespace Snakity
{
    public static class CharArrayLoader
    {
        public static Tuple<char[,], bool[,]> LoadLevel(string level)
        {
            char[,] content = Load(level);
            int width = content.GetLength(0);
            int height = content.GetLength(1);
            bool[,] spawn = new bool[width, height];
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