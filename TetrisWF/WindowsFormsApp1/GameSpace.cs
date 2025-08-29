using System.Runtime.CompilerServices;

namespace Ttrs
{
    public class GameSpace
    {
        private readonly int[,] grid;
        public int Rows { get; }
        public int Columns { get; }
        public int TotalScore = 0;

        public GameSpace(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[Rows, Columns];
        }
        public int this[int r, int c]
            {
                get => grid[r, c];
                set => grid[r, c] = value;
            }
        public bool IsCellInside(int r, int c)
        {
            return r >= 0 && c >= 0 && r < Rows && c < Columns;
        }

        public bool IsCellEmpty(int r, int c)
        {
            return IsCellInside(r, c) && grid[r, c] == 0; 
        }
        public bool IsRowFull(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0)
                { return false; }
            }
            return true;
        }   
        public bool IsRowEmpty(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if(grid[r, c] != 0) { return false; }
            }
            return true;
        }
        private void ClearRow(int r)
        { for (int c = 0; c  < Columns; c++)
            {
                grid[r, c] = 0;
            }
        }
        private void MoveRowDown(int r, int numOfRows)
        {
            for (int c = 0; c < Columns; c++)
            {
                grid[r + numOfRows, c] = grid[r, c];
                grid[r,c] = 0;
            }
        }
        public int ClearFullRows()
        {
            int cleared = 0;
            for (int r = Rows - 1; r > 0; r--)
            {
                if (IsRowFull(r))
                {
                    cleared++;
                    TotalScore++;
                    ClearRow(r);
                }
                else if (cleared > 0)
                { MoveRowDown(r, cleared); }
            }
            return cleared;
        }
        public void gridReset()
        {
            for(int r = 0; r < Rows; r++)
            {
                for(int c = 0; c < Columns; c++)
                {
                    grid[r, c] = 0;
                }
            }
        }


    }
}
