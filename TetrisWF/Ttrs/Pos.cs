namespace Ttrs
{
    public class Pos
    {
        public int RowOffset {  get; set; }
        public int ColumnOffset { get; set; }

        public Pos(int row, int column)
        {
            RowOffset = row;
            ColumnOffset = column;
        }
    }
}
