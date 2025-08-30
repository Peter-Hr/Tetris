using System.Drawing;

namespace Ttrs
{
    internal class GameOverScreen
    {
        public void Draw(Graphics g, Size clientSize, string score)
        {
            g.Clear(Color.Black);
            using (var font = new Font("Arial", 28, FontStyle.Bold))
            using (var brush = new SolidBrush(Color.White))
            {
                var text = $"Game Over\nPress ENTER to restart\nScore: {score}";
                var size = g.MeasureString(text, font);
                g.DrawString(text, font, brush,
                    (clientSize.Width - size.Width) / 2,
                    (clientSize.Height - size.Height) / 3);
            }
        }
    }
}



