using System;
using System.Linq;
using Snakity.Graphics;

namespace Snakity.Loop
{
    public static class Enemy
    {
        private static DateTime _lastCheck = DateTime.Now;
        private static readonly TimeSpan Delay = new TimeSpan(1000000);

        public static bool Compute(bool[,] spawnMap)
        {
            bool increase = false;
            foreach (Point point in Renderer.Enemies.Where(enemy => Renderer.Player.Any(s => s.Item1 == enemy))
                .ToList())
            {
                Renderer.Enemies.Remove(point);
                increase = true;
            }

            DateTime tmp = DateTime.Now;
            while (tmp - _lastCheck > Delay)
            {
                _lastCheck += Delay;
                if (Renderer.Enemies.Count > 0 && Program.Rnd.Next(50) != 0 || Renderer.Enemies.Count > 4) continue;
                Point tmp1 = new Point(Program.Rnd.Next(DiffDraw.Width), Program.Rnd.Next(DiffDraw.Height));
                int attempts = 0;
                while (!spawnMap[tmp1.X, tmp1.Y] && attempts < 5)
                {
                    tmp1.X = Program.Rnd.Next(DiffDraw.Width);
                    tmp1.Y = Program.Rnd.Next(DiffDraw.Height);
                    attempts++;
                }

                if (attempts < 5)
                    Renderer.Enemies.Add(tmp1);
            }

            return increase;
        }
    }
}