using System.Drawing;

namespace Ttrs
{
    internal class Renderer
    {
        private readonly GameEngine engine;
        private readonly int cellSize;

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

        public Renderer(GameEngine engine, int cellSize)
        {
            this.engine = engine;
            this.cellSize = cellSize;
        }   
        public void Draw(Graphics g)
        {
            using (var Pen = new Pen(Color.Purple,1))
            {
                for (int r = 0;  r < engine.GameSpace.Rows; r++)
                {
                    for(int c=0;  c < engine.GameSpace.Columns; c++)
                    {
                        g.DrawRectangle(Pen,c * cellSize,r*cellSize,cellSize,cellSize);
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
                        DrawCell(g, r, c, blockColors[id]);
                    }
                }
            }
            if (!engine.GameOver && engine.CurrentBlok != null)
            {
                foreach (var pos in engine.CurrentBlok.TilePos())
                {
                    DrawCell(g, pos.RowOffset, pos.ColumnOffset, blockColors[engine.CurrentBlok.Id]);
                }
            }
        }
        private void DrawCell(Graphics g, int row, int col, Color color)
        {
            Rectangle rect = new Rectangle(col * cellSize, row * cellSize, cellSize, cellSize);
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
