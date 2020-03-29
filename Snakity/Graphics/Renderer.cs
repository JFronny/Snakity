using System;
using System.Collections.Generic;

namespace Snakity.Graphics
{
    public static class Renderer
    {
        public static readonly List<Tuple<Point,Point>> Player = new List<Tuple<Point,Point>>();
        public static readonly List<Point> Enemies = new List<Point>();
        public static readonly List<Label> Labels = new List<Label>();

        public static void Render()
        {
            foreach (Label label in Labels) label.Render();
            foreach (Point point in Enemies) DiffDraw.Set(point, SpecialChars.Enemy);
            foreach (Tuple<Point,Point> point in Player) DiffDraw.Set(point.Item1, SpecialChars.Player);
        }
    }
}