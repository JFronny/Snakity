using System;
using System.Collections.Generic;
using System.Linq;

namespace Snakity.Graphics
{
    public static class Renderer
    {
        public static readonly List<Tuple<Point, Point>> Player = new List<Tuple<Point, Point>>();
        public static readonly List<Point> Enemies = new List<Point>();
        public static readonly List<Label> Labels = new List<Label>();

        public static void Render(bool smooth)
        {
            foreach (Label label in Labels) label.Render();
            foreach (Point point in Enemies) DiffDraw.Set(point, SpecialChars.Enemy);
            for (int i = 0; i < Player.Count; i++)
            {
                (Point position, Point direction) = Player[i];
                char selected;
                if (smooth)
                {
                    if (i == 0)
                    {
                        if (Player.Count == 1)
                        {
                            if (direction == new Point(0, 1))
                                selected = SpecialChars.Player.Up;
                            else if (direction == new Point(0, -1))
                                selected = SpecialChars.Player.Down;
                            else if (direction == new Point(1, 0))
                                selected = SpecialChars.Player.Left;
                            else if (direction == new Point(-1, 0))
                                selected = SpecialChars.Player.Right;
                            else
                                selected = SpecialChars.Player.Down;
                        }
                        else
                        {
                            Point prevPosition = Player[i + 1].Item1;
                            Point tmp = new Point(prevPosition.X - position.X, prevPosition.Y - position.Y);
                            if (tmp == new Point(0, 1))
                                selected = SpecialChars.Player.Down;
                            else if (tmp == new Point(0, -1))
                                selected = SpecialChars.Player.Up;
                            else if (tmp == new Point(1, 0))
                                selected = SpecialChars.Player.Right;
                            else if (tmp == new Point(-1, 0))
                                selected = SpecialChars.Player.Left;
                            else
                                throw new ArgumentException($"Unexpected: {{X={tmp.X};Y={tmp.Y}}}");
                        }
                    }
                    else if (i == Player.Count - 1)
                    {
                        Point prevPosition = Player[i - 1].Item1;
                        Point tmp = new Point(prevPosition.X - position.X, prevPosition.Y - position.Y);
                        if (tmp == new Point(0, 1))
                            selected = SpecialChars.Player.Down;
                        else if (tmp == new Point(0, -1))
                            selected = SpecialChars.Player.Up;
                        else if (tmp == new Point(1, 0))
                            selected = SpecialChars.Player.Right;
                        else if (tmp == new Point(-1, 0))
                            selected = SpecialChars.Player.Left;
                        else
                            throw new ArgumentException($"Unexpected: {{X={tmp.X};Y={tmp.Y}}}");
                    }
                    else
                    {
                        Point prevPosition = Player[i - 1].Item1;
                        Point nextPosition = Player[i + 1].Item1;
                        prevPosition = new Point(prevPosition.X - position.X, position.Y - prevPosition.Y);
                        nextPosition = new Point(nextPosition.X - position.X, position.Y - nextPosition.Y);
                        Direction[] directions = new Direction[2];

                        if (prevPosition == new Point(0, 1))
                            directions[0] = Direction.Up;
                        else if (prevPosition == new Point(0, -1))
                            directions[0] = Direction.Down;
                        else if (prevPosition == new Point(1, 0))
                            directions[0] = Direction.Right;
                        else if (prevPosition == new Point(-1, 0))
                            directions[0] = Direction.Left;
                        else
                            throw new ArgumentException("Unexpected previous position delta");

                        if (nextPosition == new Point(0, 1))
                            directions[1] = Direction.Up;
                        else if (nextPosition == new Point(0, -1))
                            directions[1] = Direction.Down;
                        else if (nextPosition == new Point(1, 0))
                            directions[1] = Direction.Right;
                        else if (nextPosition == new Point(-1, 0))
                            directions[1] = Direction.Left;
                        else
                            throw new ArgumentException("Unexpected next position delta");

                        directions = directions.OrderBy(s => (int) s).ToArray();

                        switch (directions[0])
                        {
                            case Direction.Up:
                                switch (directions[1])
                                {
                                    case Direction.Down:
                                        selected = SpecialChars.Player.UpDown;
                                        break;
                                    case Direction.Left:
                                        selected = SpecialChars.Player.UpLeft;
                                        break;
                                    case Direction.Right:
                                        selected = SpecialChars.Player.UpRight;
                                        break;
                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }

                                break;
                            case Direction.Down:
                                switch (directions[1])
                                {
                                    case Direction.Left:
                                        selected = SpecialChars.Player.DownLeft;
                                        break;
                                    case Direction.Right:
                                        selected = SpecialChars.Player.DownRight;
                                        break;
                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }

                                break;
                            case Direction.Left:
                                switch (directions[1])
                                {
                                    case Direction.Right:
                                        selected = SpecialChars.Player.LeftRight;
                                        break;
                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }

                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
                else
                    selected = 'P';

                DiffDraw.Set(position, selected);
            }
        }
    }
}