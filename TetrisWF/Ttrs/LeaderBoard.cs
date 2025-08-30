using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Ttrs
{
    internal class Leaderboard
    {
        private readonly string filePath = "LeaderBoard.txt";
        private readonly Dictionary<string, int> scores = new Dictionary<string, int>();


        public Leaderboard()
        {
            Load();
        }

        private void Load()
        {
            scores.Clear();
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(';');
                if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                {
                    scores[parts[0]] = score;
                }
            }
        }

        private void Save()
        {
            var lines = scores.Select(kvp => $"{kvp.Key};{kvp.Value}");
            File.WriteAllLines(filePath, lines);
        }

        public void AddScore(string name, int score)
        {
            if (string.IsNullOrWhiteSpace(name)) return;

            if (scores.TryGetValue(name, out int oldScore))
            {
                if (score > oldScore)
                    scores[name] = score;
            }
            else
            {
                scores[name] = score;
            }
            Save();
        }

        public void Draw(Graphics g, Size clientSize)
        {
            g.Clear(Color.Black);

            using (var titleFont = new Font("Arial", 28, FontStyle.Bold))
            using (var entryFont = new Font("Arial", 16))
            using (var brush = new SolidBrush(Color.White))
            {

                string title = "LEADERBOARD";
                var titleSize = g.MeasureString(title, titleFont);
                g.DrawString(title, titleFont, brush,
                    (clientSize.Width - titleSize.Width) / 2, 50);


                var ordered = scores.OrderByDescending(kvp => kvp.Value).Take(10).ToList();

                int y = 120;
                foreach (var kvp in ordered)
                {
                    string line = $"{kvp.Key} - {kvp.Value}";
                    g.DrawString(line, entryFont, brush, (clientSize.Width - 300) / 2, y);
                    y += 30;
                }

                string back = "Press ESC to return";
                var backSize = g.MeasureString(back, entryFont);
                g.DrawString(back, entryFont, brush,
                    (clientSize.Width - backSize.Width) / 2,
                    clientSize.Height - 60);
            }

        }
    }
}
