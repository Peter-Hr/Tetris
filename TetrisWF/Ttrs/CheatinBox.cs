using System.Drawing;

namespace Ttrs
{
    internal class CheatinBox
    {
        private readonly GameEngine engine;
        private readonly Color[] blockColors;
        public CheatinBox(GameEngine engine, Color[] blockColors)
        {
            this.engine = engine;
            this.blockColors = blockColors;
        }

        public void drawCheatingBox(Graphics g,int cellSize,int offsetX, int offsetY, int ttrsWidth, int ttrsHeight)
        {
            int previewCell = cellSize;
            int previewWidth = 4 * previewCell;
            int previewHeight = 4 * previewCell;

            int previewX = offsetX + ttrsWidth + 20;
            int previewY = offsetY + (ttrsHeight - previewHeight) / 2;

            g.FillRectangle(Brushes.Gray, previewX - 5, previewY - 5, previewWidth + 10, previewHeight + 10);
            g.DrawRectangle(Pens.White, previewX - 5, previewY - 5, previewWidth + 10, previewHeight + 10);

            int minRow = int.MaxValue;
            int minCol = int.MaxValue;
            foreach (var pos in engine.BlokSystem.NextBlok.TilePos())
            {
                if (pos.RowOffset < minRow) minRow = pos.RowOffset;
                if (pos.ColumnOffset < minCol) minCol = pos.ColumnOffset;
            }

            foreach (var pos in engine.BlokSystem.NextBlok.TilePos())
            {
                int drawRow = pos.RowOffset - minRow;
                int drawCol = pos.ColumnOffset - minCol;

                Rectangle rect = new Rectangle(
                    previewX + drawCol * previewCell,
                    previewY + drawRow * previewCell,
                    previewCell,
                    previewCell);

                using (var brush = new SolidBrush(blockColors[engine.BlokSystem.NextBlok.Id]))
                {
                    g.FillRectangle(brush, rect);
                }
                g.DrawRectangle(Pens.Black, rect);
            }
        }
    }
}
