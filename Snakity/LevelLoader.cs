using System;
using System.Linq;
using Snakity.Graphics;

namespace Snakity
{
    public static class LevelLoader
    {
        public static Tuple<char[,], bool[,]> Load(string level)
        {
            string[] levelArr = level.Replace(' ', SpecialChars.Space).Split('\n');
            int width = levelArr.Select(s => s.Length).OrderBy(s => s).Last();
            int height = levelArr.Length;
            char[,] tmp = new char[width, height];
            bool[,] spawnable = new bool[width, height];
            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                tmp[x, y] = SpecialChars.Space;
            for (int i = 0; i < levelArr.Length; i++)
            {
                string s = levelArr[i];
                for (int j = 0; j < s.Length; j++)
                {
                    tmp[j, i] = s[j] == '.' ? SpecialChars.Space : s[j];
                    spawnable[j, i] = s[j] == '.';
                }
            }
            return new Tuple<char[,], bool[,]>(tmp, spawnable);
        }
    }
}