using System.Linq;

namespace Snakity.Graphics
{
    public class Label
    {
        public Point Position;
        public string Text;

        public Label(Point position, string text)
        {
            Position = position;
            Text = text;
        }

        public void Render()
        {
            string[] lines = Text.Split('\n').Select(s => s.EndsWith('\r') ? s.Remove(s.Length - 1) : s).ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    int tmpX = Position.X + j;
                    int tmpY = Position.Y + i;
                    if (tmpX < DiffDraw.Width && tmpY < DiffDraw.Height)
                        DiffDraw.Set(tmpX, tmpY, lines[i][j]);
                }
            }
        }
    }
}