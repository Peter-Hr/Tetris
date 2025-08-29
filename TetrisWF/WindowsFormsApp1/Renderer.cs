using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace Ttrs
{
    internal class Renderer
    {
        private readonly GameEngine engine;
        private readonly CheatinBox cheatinBox;
        public bool Cheating { get; set; }

        private static readonly Color[] blockColors = new Color[]
        {
                    Color.Transparent, // 0 = prázdne
                    Color.Cyan,        // 1 = I
                    Color.Blue,        // 2 = J
                    Color.Orange,      // 3 = L
                    Color.Yellow,      // 4 = O
                    Color.Green,       // 5 = S
                    Color.Purple,      // 6 = T
                    Color.Red          // 7 = Z
        };

        public Renderer(GameEngine engine)
        {
            this.engine = engine;
            cheatinBox = new CheatinBox(engine, blockColors);
        }
       
        public void Draw(Graphics g, Size clientSize)
        {
            int cellWidth = clientSize.Width / engine.GameSpace.Columns;
            int cellHeight = clientSize.Height / engine.GameSpace.Rows;
            int cellSize = Math.Min(cellWidth, cellHeight);
            int ttrsWidth = engine.GameSpace.Columns * cellSize;
            int ttrsHeight = engine.GameSpace.Rows * cellSize;
            int offsetX = (clientSize.Width - ttrsWidth) / 2;
            int offsetY = (clientSize.Height - ttrsHeight) / 2;
            g.FillRectangle(Brushes.Black, offsetX, offsetY, ttrsWidth, ttrsHeight);

            using (var Pen = new Pen(Color.Purple,1))
            {
                for (int r = 0;  r < engine.GameSpace.Rows; r++)
                {
                    for(int c=0;  c < engine.GameSpace.Columns; c++)
                    {
                        g.DrawRectangle(Pen,offsetX + c * cellSize,offsetY + r*cellSize,cellSize,cellSize);
                    }
                }
            }

            for (int r = 0; r < engine.GameSpace.Rows; r++)
            {
                for (int c = 0; c < engine.GameSpace.Columns; c++)
                {
                    int id = engine.GameSpace[r, c];
                    if (id != 0)
                    {
                        DrawCell(g, r, c, blockColors[id], offsetX, offsetY, cellSize);
                    }
                }
            }
            if (!engine.GameOver && engine.CurrentBlok != null)
            {
                foreach (var pos in engine.CurrentBlok.TilePos())
                {
                    DrawCell(g, pos.RowOffset, pos.ColumnOffset, blockColors[engine.CurrentBlok.Id], offsetX,offsetY, cellSize);
                }
            }
            if (Cheating && engine.BlokSystem.NextBlok != null)
            {
                cheatinBox.drawCheatingBox(g,cellSize, offsetX, offsetY, ttrsWidth, ttrsHeight);
            }


        }
        private void DrawCell(Graphics g, int row, int col, Color color, int ofsetX, int ofsetY, int cellSize)
        {
            Rectangle rect = new Rectangle(ofsetX + col * cellSize,ofsetY + row * cellSize, cellSize, cellSize);
            using (var brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, rect);
            }
            using (var pen = new Pen(Color.Black, 1))
            {
                g.DrawRectangle(pen, rect);
            }
        }
    }
}
