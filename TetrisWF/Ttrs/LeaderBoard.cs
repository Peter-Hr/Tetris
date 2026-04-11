using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using log4net;

namespace Ttrs
{
    internal class Leaderboard
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Leaderboard));

        private readonly string filePath = "LeaderBoard.txt";
        private readonly Dictionary<string, int> scores = new Dictionary<string, int>();

        public Leaderboard()
        {
            Log.Info("Initializing leaderboard");
            Load();
        }

        private void Load()
        {
            scores.Clear();

            if (!File.Exists(filePath))
            {
                Log.Warn($"Leaderboard file not found: {filePath}");
                return;
            }

            try
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    var parts = line.Split(';');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                    {
                        scores[parts[0]] = score;
                    }
                    else
                    {
                        Log.Warn($"Invalid leaderboard line format: {line}");
                    }
                }

                Log.Info($"Leaderboard loaded successfully ({scores.Count} records)");
            }
            catch (Exception ex)
            {
                Log.Error("Failed to load leaderboard", ex);
            }
        }

        private void Save()
        {
            try
            {
                var lines = scores.Select(kvp => $"{kvp.Key};{kvp.Value}");
                File.WriteAllLines(filePath, lines);
                Log.Info($"Leaderboard saved ({scores.Count} records)");
            }
            catch (Exception ex)
            {
                Log.Error("Failed to save leaderboard", ex);
            }
        }

        public void AddScore(string name, int score)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Log.Warn("Attempted to add score with empty name");
                return;
            }

            bool changed = false;

            try
            {
                if (scores.TryGetValue(name, out int oldScore))
                {
                    if (score > oldScore)
                    {
                        scores[name] = score;
                        changed = true;
                        Log.Info($"Updated score for {name}: {oldScore} -> {score}");
                    }
                }
                else
                {
                    scores[name] = score;
                    changed = true;
                    Log.Info($"New player added: {name} with score {score}");
                }

                if (changed)
                {
                    Save();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while adding score", ex);
            }
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