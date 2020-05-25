using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using CC_Functions.Commandline.TUI;
using CC_Functions.Misc;
using Snakity.Graphics;
using Label = Snakity.Graphics.Label;

namespace Snakity.Loop
{
    internal static class Program
    {
        private static int _score;
        public static Random Rnd = new Random();

        public static void Main(string[] args)
        {
            args = args.Select(s => s.TrimStart('-', '/').ToLower()).ToArray();
            if (args.Contains("drawtest") || args.Contains("bench") || args.Contains("b"))
            {
                Benchmark.Perform();
                return;
            }
            bool playing = true;
            while (playing)
            {
                Console.Clear();
                //Main menu
                Console.WriteLine($@"
~ Snakity ~
Highscore: {SettingsMan.Highscore}

   s - Start
   v - Settings
   b - Benchmark
   x - Exit");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Enter:
                    case ConsoleKey.S:
                        PlayRound();
                        while (GameOver())
                            PlayRound();
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.X:
                        playing = false;
                        break;
                    case ConsoleKey.V:
                        SettingsGui.Show();
                        break;
                    case ConsoleKey.B:
                        Benchmark.Perform();
                        break;
                }
            }

            Console.Clear();
            Console.WriteLine("Bye");
        }

        private static bool GameOver()
        {
            Thread.Sleep(200);
            Console.Clear();
            if (_score > SettingsMan.Highscore)
                SettingsMan.Highscore = _score;
            Console.WriteLine($@"
GAME OVER
Score: {_score}

Play again? (y/n)");
            _score = 0;
            ConsoleKey tmp;
            do
                tmp = Console.ReadKey().Key;
            while (tmp != ConsoleKey.Y && tmp != ConsoleKey.N);
            return tmp == ConsoleKey.Y;
        }

        private static void PlayRound()
        {
            Console.Clear();
            bool playing = true;
            Pixel[,] init = new Pixel[1,2];
            init.Populate(new Pixel());
            DiffDraw.Clear(init);
            DiffDraw.Draw(false);
            Label scoreLabel = new Label(new Point(0, 0), "");
            Renderer.Labels.Clear();
            Renderer.Labels.Add(scoreLabel);
            Levels.Load();
            (char[,] level, bool[,] spawnable) =
                CharArrayLoader.LoadLevel(Levels.levels[Rnd.Next(Levels.levels.Length)], SettingsMan.SmoothTerrain);
            bool hasIncreased = false;
            Renderer.Player.Clear();
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(1, 1), new Point(0, 1)));
            Renderer.Enemies.Clear();
            DiffDraw.Clear(ColorSelector.Get(level));
            DiffDraw.Draw(SettingsMan.Color);
            Input.Reset();
            while (playing)
            {
                DiffDraw.Clear(ColorSelector.Get(level));
                scoreLabel.Text = $"Score: {_score}";
                Renderer.Render(SettingsMan.SmoothPlayer);
                Input.Get();
                if (Input.Move(hasIncreased) || Input.Esc)
                {
                    Renderer.Player.Clear();
                    Input.Reset();
                    playing = false;
                    Input.Esc = false;
                    continue;
                }

                if (Input.R)
                {
                    _score = 0;
                    init.Populate(new Pixel());
                    DiffDraw.Clear(init);
                    DiffDraw.Draw(SettingsMan.Color);
                    Console.Clear();
                    (level, spawnable) = CharArrayLoader.LoadLevel(Levels.levels[Rnd.Next(Levels.levels.Length)], SettingsMan.SmoothTerrain);
                    DiffDraw.Clear(ColorSelector.Get(level));
                    Renderer.Player.Clear();
                    Renderer.Player.Add(new Tuple<Point, Point>(new Point(2, 2), new Point(0, 1)));
                    Renderer.Enemies.Clear();
                    Input.Reset();
                    Input.R = false;
                }

                Renderer.Render(SettingsMan.SmoothPlayer);
                hasIncreased = Enemy.Compute(spawnable);
                if (hasIncreased)
                    _score++;
                DiffDraw.Draw(SettingsMan.Color);
            }
            Console.ResetColor();
        }
    }
}