using System;
using System.Linq;
using Snakity.Graphics;

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
                    case ConsoleKey.S:
                        PlayRound();
                        while (GameOver())
                            PlayRound();
                        break;
                    case ConsoleKey.X:
                        playing = false;
                        break;
                    case ConsoleKey.V:
                        int currentSetting = 0;
                        bool settingVals = true;
                        while (settingVals)
                        {
                            Console.ResetColor();
                            Console.Clear();
                            Console.WriteLine("Smooth Player");
                            Console.ForegroundColor =
                                SettingsMan.SmoothPlayer ? ConsoleColor.Black : ConsoleColor.White;
                            Console.BackgroundColor =
                                SettingsMan.SmoothPlayer ? ConsoleColor.White : ConsoleColor.Black;
                            Console.Write("Yes ");
                            Console.ForegroundColor =
                                SettingsMan.SmoothPlayer ? ConsoleColor.White : ConsoleColor.Black;
                            Console.BackgroundColor =
                                SettingsMan.SmoothPlayer ? ConsoleColor.Black : ConsoleColor.White;
                            Console.WriteLine("No");
                            Console.ResetColor();
                            Console.WriteLine("Smooth Terrain");
                            Console.ForegroundColor =
                                SettingsMan.SmoothTerrain ? ConsoleColor.Black : ConsoleColor.White;
                            Console.BackgroundColor =
                                SettingsMan.SmoothTerrain ? ConsoleColor.White : ConsoleColor.Black;
                            Console.Write("Yes ");
                            Console.ForegroundColor =
                                SettingsMan.SmoothTerrain ? ConsoleColor.White : ConsoleColor.Black;
                            Console.BackgroundColor =
                                SettingsMan.SmoothTerrain ? ConsoleColor.Black : ConsoleColor.White;
                            Console.WriteLine("No");
                            Console.ResetColor();
                            Console.WriteLine("Use Color");
                            Console.ForegroundColor =
                                SettingsMan.Color ? ConsoleColor.Black : ConsoleColor.White;
                            Console.BackgroundColor =
                                SettingsMan.Color ? ConsoleColor.White : ConsoleColor.Black;
                            Console.Write("Yes ");
                            Console.ForegroundColor =
                                SettingsMan.Color ? ConsoleColor.White : ConsoleColor.Black;
                            Console.BackgroundColor =
                                SettingsMan.Color ? ConsoleColor.Black : ConsoleColor.White;
                            Console.WriteLine("No");
                            Console.ResetColor();
                            switch (Console.ReadKey().Key)
                            {
                                case ConsoleKey.Escape:
                                case ConsoleKey.Enter:
                                    settingVals = false;
                                    break;;
                                case ConsoleKey.LeftArrow:
                                case ConsoleKey.RightArrow:
                                case ConsoleKey.Spacebar:
                                    switch (currentSetting)
                                    {
                                        case 0:
                                            SettingsMan.SmoothPlayer = !SettingsMan.SmoothPlayer;
                                            break;
                                        case 1:
                                            SettingsMan.SmoothTerrain = !SettingsMan.SmoothTerrain;
                                            break;
                                        case 2:
                                            SettingsMan.Color = !SettingsMan.Color;
                                            break;
                                    }
                                    break;
                                case ConsoleKey.UpArrow:
                                    currentSetting--;
                                    if (currentSetting < 0)
                                        currentSetting = 2;
                                    break;
                                case ConsoleKey.DownArrow:
                                case ConsoleKey.Tab:
                                    currentSetting++;
                                    if (currentSetting > 2)
                                        currentSetting = 0;
                                    break;
                            }
                        }
                        Console.ResetColor();
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
            Console.Clear();
            if (_score > SettingsMan.Highscore)
                SettingsMan.Highscore = _score;
            Console.WriteLine($@"
GAME OVER
Score: {_score}

Play again? (y/n)");
            _score = 0;
            return Console.ReadKey().KeyChar == 'y';
        }

        private static void PlayRound()
        {
            Console.Clear();
            bool playing = true;
            DiffDraw.Clear(5, 5);
            Label scoreLabel = new Label(new Point(0, 0), "");
            Renderer.Labels.Clear();
            Renderer.Labels.Add(scoreLabel);
            (char[,] level, bool[,] spawnable) =
                CharArrayLoader.LoadLevel(Levels.levels[Rnd.Next(Levels.levels.Length)], SettingsMan.SmoothTerrain);
            bool hasIncreased = false;
            Renderer.Player.Clear();
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(1, 1), new Point(0, 1)));
            Renderer.Enemies.Clear();
            DiffDraw.Clear((char[,]) level.Clone());
            DiffDraw.Draw(SettingsMan.Color);
            Input.Reset();
            while (playing)
            {
                DiffDraw.Clear((char[,]) level.Clone());
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
                    DiffDraw.Clear();
                    DiffDraw.Draw(SettingsMan.Color);
                    (level, spawnable) = CharArrayLoader.LoadLevel(Levels.levels[Rnd.Next(Levels.levels.Length)], SettingsMan.SmoothTerrain);
                    DiffDraw.Clear((char[,]) level.Clone());
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
        }
    }
}