using System;
using System.Threading;
using System.Windows.Forms;
using CC_Functions.W32;
using Snakity.Graphics;
using Label = Snakity.Graphics.Label;

namespace Snakity.Loop
{
    static class Program
    {
        private static int _score = 0;
        public static Random Rnd = new Random();
        public static void Main(string[] args)
        {
            DiffDraw.Clear(5, 5);
            Label scoreLabel = new Label(new Point(0, 0), "");
            Renderer.Labels.Add(scoreLabel);
            (char[,] level, bool[,] spawnable) = 
                LevelLoader.Load(Levels.levels[Rnd.Next(Levels.levels.Length)]);
            Renderer.Player.Add(new Tuple<Point, Point>(new Point(2, 2), new Point(0, 1)));
            bool hasIncreased = false;
            while (true)
            {
                DiffDraw.Clear((char[,])level.Clone());
                scoreLabel.Text = $"Score: {_score}; d={Input.Delay.TotalMilliseconds}";
                Renderer.Render();
                Input.Get();
                bool down = KeyboardReader.IsKeyDown(Keys.R);
                if (Input.Move(hasIncreased) || down)
                {
                    DiffDraw.Clear();
                    DiffDraw.Draw();
                    (level, spawnable) = LevelLoader.Load(Levels.levels[Rnd.Next(Levels.levels.Length)]);
                    Renderer.Player.Clear();
                    Renderer.Enemies.Clear();
                    Input.ResetSpeed();
                    Renderer.Player.Add(new Tuple<Point, Point>(new Point(2, 2), new Point(0, 1)));
                    DiffDraw.Clear((char[,])level.Clone());
                    if (down)
                        while (KeyboardReader.IsKeyDown(Keys.R))
                            Thread.Sleep(100);
                }
                Renderer.Render();
                hasIncreased = Enemy.Compute(spawnable);
                if (hasIncreased)
                    _score++;
                DiffDraw.Draw();
                if (KeyboardReader.IsKeyDown(Keys.Escape))
                    return;
            }
        }
    }
}
