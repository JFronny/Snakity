using System;
using System.Windows.Forms;
using CC_Functions.W32;
using Snakity.Graphics;

namespace Snakity.Loop
{
    public static class Input
    {
        private static DateTime _lastCheck = DateTime.Now;
        public static TimeSpan Delay = new TimeSpan(3000000);
        private static bool _shouldIncrease = false;
        public static void Get()
        {
            Point headDelta = new Point(0, 0);
            if ((KeyboardReader.IsKeyDown(Keys.W) || KeyboardReader.IsKeyDown(Keys.Up)))
                headDelta.Y--;
            if ((KeyboardReader.IsKeyDown(Keys.A) || KeyboardReader.IsKeyDown(Keys.Left)))
                headDelta.X--;
            if ((KeyboardReader.IsKeyDown(Keys.S) || KeyboardReader.IsKeyDown(Keys.Down)))
                headDelta.Y++;
            if ((KeyboardReader.IsKeyDown(Keys.D) || KeyboardReader.IsKeyDown(Keys.Right)))
                headDelta.X++;
            if (headDelta != new Point(0, 0) && (Renderer.Player.Count < 2 || headDelta != Renderer.Player[1].Item1 - Renderer.Player[0].Item1))
                Renderer.Player[0] = new Tuple<Point, Point>(Renderer.Player[0].Item1, headDelta);
        }

        public static bool Move(bool increaseScore)
        {
            DateTime tmp = DateTime.Now;
            if (increaseScore) _shouldIncrease = true;
            while (tmp - _lastCheck > Delay)
            {
                _lastCheck += Delay;
                Point head = Renderer.Player[0].Item1;
                Point headDelta = Renderer.Player[0].Item2;
                if (headDelta.Y == -1 && (head.Y == 0 || !CheckPoint(head + new Point(0, -1))))
                    return true;
                if (headDelta.X == -1 && (head.X == 0 || !CheckPoint(head + new Point(-1, 0))))
                    return true;
                if (headDelta.Y == 1 && (head.Y >= DiffDraw.Height - 1 || !CheckPoint(head + new Point(0, 1))))
                    return true;
                if (headDelta.X == 1 && (head.X >= DiffDraw.Width - 1 || !CheckPoint(head + new Point(1, 0))))
                    return true;
                Renderer.Player.Insert(0, new Tuple<Point, Point>(Renderer.Player[0].Item1 + headDelta, headDelta));
                if (_shouldIncrease)
                {
                    _shouldIncrease = false;
                    Delay = Delay.Subtract(new TimeSpan((int) (800 * Delay.TotalMilliseconds)));
                }
                else
                {
                    Renderer.Player.RemoveAt(Renderer.Player.Count - 1);
                }
            }
            return false;
        }

        public static void ResetSpeed() => Delay = new TimeSpan(3000000);

        private static bool CheckPoint(Point point) =>
            DiffDraw.Get(point) == SpecialChars.Space || DiffDraw.Get(point) == SpecialChars.Enemy;
    }
}