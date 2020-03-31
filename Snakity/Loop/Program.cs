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
            if (args.Contains("DrawTest"))
            {
                Console.Clear();
                (char[,] level, _) =
                    CharArrayLoader.LoadLevel(@"
#####################
#                   #
#                   #
#                   #
#                   #
#                   #
#####################");
                DiffDraw.Clear(level);
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
                Renderer.Render();
                DiffDraw.Draw();
                Console.ReadKey();
                return;
            }

            bool playing = true;
            while (playing)
            {
                Console.Clear();
                //Main menu
                Console.WriteLine(@"
~ Snakity ~

      s - Start
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
                }
            }

            Console.Clear();
            Console.WriteLine("Bye");
        }

        private static bool GameOver()
        {
            Console.Clear();
            Console.WriteLine($@"
GAME OVER
Score: {_score}

Play again? (y/n)");
            return Console.ReadKey().KeyChar == 'y';
        }

        private static void PlayRound()
        {
            bool playing = true;
            DiffDraw.Clear(5, 5);
            Label scoreLabel = new Label(new Point(0, 0), "");
            Renderer.Labels.Add(scoreLabel);
            (char[,] level, bool[,] spawnable) =
                CharArrayLoader.LoadLevel(Levels.levels[Rnd.Next(Levels.levels.Length)]);
            bool hasIncreased = false;
            Renderer.Player.Clear();
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(1, 1), new Point(0, 1)));
            Renderer.Enemies.Clear();
            Input.ResetSpeed();
            while (playing)
            {
                DiffDraw.Clear((char[,]) level.Clone());
                scoreLabel.Text = $"Score: {_score}";
                Renderer.Render();
                Input.Get();
                if (Input.Move(hasIncreased) || Input.Esc)
                {
                    playing = false;
                    Input.Esc = false;
                    continue;
                }

                if (Input.R)
                {
                    DiffDraw.Clear();
                    DiffDraw.Draw();
                    (level, spawnable) = CharArrayLoader.LoadLevel(Levels.levels[Rnd.Next(Levels.levels.Length)]);
                    Renderer.Player.Clear();
                    Renderer.Enemies.Clear();
                    Input.ResetSpeed();
                    Renderer.Player.Add(new Tuple<Point, Point>(new Point(2, 2), new Point(0, 1)));
                    DiffDraw.Clear((char[,]) level.Clone());
                    Input.R = false;
                }

                Renderer.Render();
                hasIncreased = Enemy.Compute(spawnable);
                if (hasIncreased)
                    _score++;
                DiffDraw.Draw();
            }
        }
    }
}