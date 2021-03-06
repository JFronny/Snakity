﻿using System;
using System.Drawing;
using CC_Functions.Commandline.TUI;
using Snakity.Graphics;

namespace Snakity.Loop
{
    public static class Input
    {
        private static DateTime _lastCheck = DateTime.Now;
        private static TimeSpan _delay = new TimeSpan(4000000);
        private static bool _shouldIncrease;

        public static bool R;
        public static bool Esc;

        public static void Get()
        {
            Point headDelta = new Point(0, 0);
            bool pause = false;
            while (Console.KeyAvailable)
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.K:
                    case ConsoleKey.NumPad8:
                        if (headDelta.Y == 0)
                            headDelta.Y = -1;
                        else
                            headDelta.Y = 0;

                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.H:
                    case ConsoleKey.NumPad4:
                        headDelta.X = headDelta.X switch
                        {
                            0 => -1,
                            _ => 0
                        };
                        break;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.J:
                    case ConsoleKey.NumPad2:
                        headDelta.Y = headDelta.Y switch
                        {
                            0 => 1,
                            _ => 0
                        };
                        break;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.L:
                    case ConsoleKey.NumPad6:
                        headDelta.X = headDelta.X switch
                        {
                            0 => 1,
                            _ => 0
                        };
                        break;
                    case ConsoleKey.R:
                    case ConsoleKey.OemPlus:
                        R = true;
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.End:
                    case ConsoleKey.OemMinus:
                        Esc = true;
                        break;
                    case ConsoleKey.P:
                        pause = true;
                        break;
                }

            if (pause)
                Console.ReadKey();

            if (headDelta != new Point(0, 0) && (Renderer.Player.Count < 2 ||
                                                 headDelta != Renderer.Player[1].Item1.m(Renderer.Player[0].Item1)))
                Renderer.Player[0] = new Tuple<Point, Point>(Renderer.Player[0].Item1, headDelta);
        }

        public static bool Move(bool increaseScore)
        {
            DateTime tmp = DateTime.Now;
            if (increaseScore) _shouldIncrease = true;
            while (tmp - _lastCheck > _delay)
            {
                _lastCheck += _delay;
                Point head = Renderer.Player[0].Item1;
                Point headDelta = Renderer.Player[0].Item2;
                if (headDelta.Y == -1 && (head.Y == 0 || !CheckPoint(head.p(new Point(0, -1)))))
                    return true;
                if (headDelta.X == -1 && (head.X == 0 || !CheckPoint(head.p(new Point(-1, 0)))))
                    return true;
                if (headDelta.Y == 1 && (head.Y >= DiffDraw.Height - 1 || !CheckPoint(head.p(new Point(0, 1)))))
                    return true;
                if (headDelta.X == 1 && (head.X >= DiffDraw.Width - 1 || !CheckPoint(head.p(new Point(1, 0)))))
                    return true;
                Renderer.Player.Insert(0, new Tuple<Point, Point>(Renderer.Player[0].Item1.p(headDelta), headDelta));
                if (_shouldIncrease)
                {
                    _shouldIncrease = false;
                    _delay = _delay.Subtract(new TimeSpan((int) (200 * _delay.TotalMilliseconds)));
                }
                else
                {
                    Renderer.Player.RemoveAt(Renderer.Player.Count - 1);
                }
            }

            return false;
        }

        public static void Reset()
        {
            _delay = new TimeSpan(3000000);
            _lastCheck = DateTime.Now;;
        }
        
        private static Point p(this Point pt, Point pt2) => new Point(pt.X + pt2.X, pt.Y + pt2.Y);
        private static Point m(this Point pt, Point pt2) => new Point(pt.X - pt2.X, pt.Y - pt2.Y);

        private static bool CheckPoint(Point point) =>
            DiffDraw.Get(point) == CC_Functions.Misc.SpecialChars.Empty || DiffDraw.Get(point) == SpecialChars.Enemy;
    }
}