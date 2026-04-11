using log4net;
using System.Drawing;
using System.Windows.Forms;


namespace Ttrs
{
    internal class MainMenu
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(GameEngine));
        public bool Cheating { get; private set; }
        public string PlayerName { get; private set; } = "";
        public bool enteringName { get; private set; }



        public void ToggleCheating()
        {
            Cheating = !Cheating;
        }
        public void StartEnteringName()
        {
            Log.Info("Started entering name");
            enteringName = true;
            PlayerName = "";
        }

        public void StopEnteringName()
        {
            Log.Info("Stopped entering name");
            enteringName = false;
        }
        public void HandleKey(Keys key)
        {
            if (!enteringName)
            {
                if (key == Keys.C) ToggleCheating();
                else if (key == Keys.D1) StartEnteringName();
            }
            else
            {
                if (key == Keys.D1)
                {
                    StopEnteringName();
                }
                else if (key == Keys.Back && PlayerName.Length > 0)
                {
                    PlayerName = PlayerName.Substring(0, PlayerName.Length - 1);
                }
                else if (key >= Keys.A && key <= Keys.Z)
                {
                    PlayerName += key.ToString();
                }
                else if (key == Keys.Space)
                {
                    PlayerName += " ";
                }
            }
        }
        public void Draw(Graphics g, Size clientSize)
        {
      
            string title = "TETRIS";
            string subtitle = "Press ENTER to start";
            string cheatText = $"Cheating: {(Cheating ? "ON" : "OFF")} (Press C to toggle)";
            string nameText = $"Enter Name (Press 1): {PlayerName}";
            string leaderboardText = "Press L to show leaderboard";

            using (var titleFont = new Font("Arial", 36, FontStyle.Bold))
            using (var subtitleFont = new Font("Arial", 16))
            using (var miniFont = new Font("Arial", 14))
            using (var brushPurple = new SolidBrush(Color.MediumPurple))
            using (var brush = new SolidBrush(Color.White))
            {
                g.Clear(Color.Black);

                var titleSize = g.MeasureString(title, titleFont);
                var subSize = g.MeasureString(subtitle, subtitleFont);
                var cheatSize = g.MeasureString(cheatText, miniFont);
                var nameSize = g.MeasureString(nameText, miniFont);
                var leaderboardSize = g.MeasureString(leaderboardText, miniFont);

                g.DrawString(title, titleFont, brushPurple,
                    (clientSize.Width - titleSize.Width) / 2,
                    (clientSize.Height - titleSize.Height) / 3);

                g.DrawString(subtitle, subtitleFont, brush,
                    (clientSize.Width - subSize.Width) / 2,
                    (clientSize.Height - subSize.Height) / 2);
                
                g.DrawString(cheatText, miniFont, brush,
                    (clientSize.Width - cheatSize.Width) / 2,
                    (clientSize.Height - cheatSize.Height) / 2 + 50);
                
                g.DrawString(nameText, miniFont, brush,
                    (clientSize.Width - nameSize.Width) / 2,
                    (clientSize.Height - nameSize.Height) / 2 + 80);

                g.DrawString(leaderboardText, miniFont, brush,
                    (clientSize.Width - leaderboardSize.Width) / 2,
                    (clientSize.Height - leaderboardSize.Height) / 2 + 110);
            }
        }
    }
}



