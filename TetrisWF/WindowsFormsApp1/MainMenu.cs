using System.Drawing;

namespace Ttrs
{
    internal class MainMenu
    {
        public bool Cheating { get; private set; }

        public void ToggleCheating()
        {
            Cheating = !Cheating;
        }
        public void Draw(Graphics g, Size clientSize)
        {
            string title = "TETRIS";
            string subtitle = "Press ENTER to start";
            string cheatText = $"Cheating: {(Cheating ? "ON" : "OFF")} (Press C to toggle)";


            using (var titleFont = new Font("Arial", 36, FontStyle.Bold))
            using (var subtitleFont = new Font("Arial", 16))
            using (var cheatFont = new Font("Arial", 14))
            using (var brushPurple = new SolidBrush(Color.MediumPurple))
            using (var brush = new SolidBrush(Color.White))
            {
                g.Clear(Color.Black);

                var titleSize = g.MeasureString(title, titleFont);
                var subSize = g.MeasureString(subtitle, subtitleFont);
                var cheatSize = g.MeasureString(cheatText, cheatFont);

                g.DrawString(title, titleFont, brushPurple,
                    (clientSize.Width - titleSize.Width) / 2,
                    (clientSize.Height - titleSize.Height) / 3);

                g.DrawString(subtitle, subtitleFont, brush,
                    (clientSize.Width - subSize.Width) / 2,
                    (clientSize.Height - subSize.Height) / 2);
                g.DrawString(cheatText, cheatFont, brush,
                    (clientSize.Width - cheatSize.Width) / 2,
                    (clientSize.Height - cheatSize.Height) / 2 + 50);
            }
        }
    }
}



