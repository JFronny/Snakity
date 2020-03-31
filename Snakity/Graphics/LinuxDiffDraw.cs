using System;

namespace Snakity.Graphics
{
    public class DiffDraw
    {
        private static char[,] Screen { get; set; } = new char[0, 0];
        public static int Width => Screen.GetLength(0);
        public static int Height => Screen.GetLength(1);

        public static void Draw()
        {
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            int width = Width;
            int height = Height;
            for (int y = 0; y < height; y++)
            {
                string line = "";
                for (int x = 0; x < width; x++)
                {
                    char tmp1 = Screen[x, y];
                    line += tmp1 == '\0' ? ' ' : tmp1;
                }
                Console.WriteLine(line);
                Console.CursorLeft = 0;
            }
        }

        public static char Get(Point p) => Get(p.X, p.Y);

        public static char Get(int x, int y) => Screen[x, y];

        public static void Set(Point p, char c) => Set(p.X, p.Y, c);

        public static void Set(int x, int y, char c) => Screen[x, y] = c;

        public static void Clear() => Clear(Width, Height);

        public static void Clear(int width, int height) => Screen = new char[width, height];

        public static void Clear(char[,] content) => Screen = content;
    }
}