using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ttrs
{
    internal class MainMenu
    {
        public void Draw(Graphics g, Size clientSize)
        {
            string title = "TETRIS";
            string subtitle = "Press ENTER to start";
            string cheating = "CHEATING";

            using (var titleFont = new Font("Arial", 36, FontStyle.Bold))
            using (var subtitleFont = new Font("Arial", 16))
            using (var brush = new SolidBrush(Color.White))
            {
                g.Clear(Color.Black);

                var titleSize = g.MeasureString(title, titleFont);
                var subSize = g.MeasureString(subtitle, subtitleFont);

                g.DrawString(title, titleFont, brush,
                    (clientSize.Width - titleSize.Width) / 2,
                    (clientSize.Height - titleSize.Height) / 3);

                g.DrawString(subtitle, subtitleFont, brush,
                    (clientSize.Width - subSize.Width) / 2,
                    (clientSize.Height - subSize.Height) / 2);
                g.DrawString(cheating, subtitleFont, brush,
                    (clientSize.Width - subSize.Width) / 2,
                    (clientSize.Height - subSize.Height) / 4 );
            }
        }
    }
}



