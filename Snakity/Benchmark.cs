using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using CC_Functions.Commandline.TUI;
using CC_Functions.Misc;
using Snakity.Graphics;
using Label = Snakity.Graphics.Label;

namespace Snakity
{
    public static class Benchmark
    {
        public static void Perform()
        {
            Levels.Load();
            Console.Clear();
            Renderer.Enemies.Clear();
            Renderer.Labels.Clear();
            Renderer.Player.Clear();
            DiffDraw.Clear(21, 1);
            Label status = new Label(new Point(0, 0), "Snakity Bench running");
            Renderer.Labels.Add(status);
            Renderer.Render(false);
            Tuple<bool, bool, bool, TimeSpan>[] result = new[] {false, true}.SelectMany(b1 => new[] {false, true}, (b1, b2) => new {b1, b2})
                .SelectMany(t => new[] {false, true}, (t, b3) => new {t, b3})
                .Select(t => new Tuple<bool, bool, bool, TimeSpan>(t.t.b1, t.t.b2, t.b3, Perform(t.t.b1, t.t.b2, t.b3)))
                .OrderBy(s => s.Item4).ToArray();
            Console.Clear();
            Console.WriteLine("╔═Smooth1═╤═Smooth2═╤═Color═╤═Time═╗");
            bool color2 = false;
            foreach ((bool smooth1, bool smooth2, bool color, TimeSpan time) in result)
            {
                color2 = !color2;
                Console.BackgroundColor = color2 ? ConsoleColor.Black : ConsoleColor.White;
                Console.ForegroundColor = color2 ? ConsoleColor.White : ConsoleColor.Black;
                string tmp = time.TotalSeconds.ToString(CultureInfo.InvariantCulture);
                if (tmp.Length > 5) tmp = tmp.Remove(5);
                else if (tmp.Length < 5) tmp += new string(' ', 6 - tmp.Length);
                Console.WriteLine(
                    $"║    {(smooth1 ? "X" : " ")}    │    {(smooth2 ? "X" : " ")}    │   {(color ? "X" : " ")}   │{tmp}s║");
            }
            Console.ResetColor();
            Console.WriteLine("╚═Smooth1═╧═Smooth2═╧═Color═╧═Time═╝");
            Console.WriteLine();
            Console.WriteLine("Impact:");
            Console.WriteLine($"- Smooth1: {GetImpact(result, tuple => tuple.Item1)}s");
            Console.WriteLine($"- Smooth2: {GetImpact(result, tuple => tuple.Item2)}s");
            Console.WriteLine($"- Color:   {GetImpact(result, tuple => tuple.Item3)}s");
            Console.WriteLine();
            Console.WriteLine("Press any key to return");
            Console.ReadKey();
        }

        private static double GetImpact(Tuple<bool, bool, bool, TimeSpan>[] result,
            Func<Tuple<bool, bool, bool, TimeSpan>, bool> selector) =>
            GetAvgTime(result, selector) - GetAvgTime(result, tuple => !selector.Invoke(tuple));

        private static double GetAvgTime(Tuple<bool, bool, bool, TimeSpan>[] result,
            Func<Tuple<bool, bool, bool, TimeSpan>, bool> selector) =>
            Average(result.Where(selector).Select(s => s.Item4).ToArray());

        private static double Average(params TimeSpan[] spans) => spans.Aggregate(TimeSpan.Zero, (current, t) => current + t / spans.Length).TotalSeconds;

        private static TimeSpan Perform(bool smooth1, bool smooth2, bool color)
        {
            DiffDraw.Clear(0, 0);
            Renderer.Player.Clear();
            Pixel[,] init = new Pixel[1,2];
            init.Populate(new Pixel());
            DiffDraw.Clear(init);
            DiffDraw.FullDraw(false);
            Stopwatch t = Stopwatch.StartNew();
            (char[,] level, _) =
                CharArrayLoader.LoadLevel(@"
#####################
#                 ###
#               ### #
#            # ##   #
#             #     #
#            ##     #
#####################", smooth1);
            DiffDraw.Clear(ColorSelector.Get(level));
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(1, 2), new Point(0, 0)));
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(2, 2), new Point(0, 0)));
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(2, 3), new Point(0, 0)));
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(1, 3), new Point(0, 0)));
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(1, 4), new Point(0, 0)));
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(1, 5), new Point(0, 0)));
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(1, 6), new Point(0, 0)));
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(2, 6), new Point(0, 0)));
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(2, 5), new Point(0, 0)));
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(2, 4), new Point(0, 0)));
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(3, 4), new Point(0, 0)));
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(3, 3), new Point(0, 0)));
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(3, 2), new Point(0, 0)));
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(4, 2), new Point(0, 0)));
            Renderer.Enemies.Add(new Point(5, 4));
            Renderer.Enemies.Add(new Point(5, 5));
            Renderer.Enemies.Add(new Point(5, 6));
            Renderer.Enemies.Add(new Point(6, 6));
            Renderer.Enemies.Add(new Point(7, 6));
            Renderer.Enemies.Add(new Point(7, 5));
            Renderer.Enemies.Add(new Point(7, 4));
            Renderer.Enemies.Add(new Point(6, 3));
            Renderer.Enemies.Add(new Point(5, 2));
            Renderer.Render(smooth2);
            DiffDraw.Draw(color);
            foreach (string t1 in Levels.levels)
            {
                (char[,] lvl, _) =
                    CharArrayLoader.LoadLevel(t1, SettingsMan.SmoothTerrain);
                DiffDraw.Clear(ColorSelector.Get(lvl));
                DiffDraw.Draw(true);
            }
            t.Stop();
            return t.Elapsed;
        }
    }
}