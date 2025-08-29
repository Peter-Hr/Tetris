using System.Collections.Generic;

namespace Ttrs
{
    public abstract class Blok
    {
        protected abstract Pos[][] Tiles { get; }
        protected abstract Pos StartOffset { get; }
        public abstract int Id { get; }
        private int rotationState;
        private Pos offset;

        public Blok()
        {
            offset = new Pos(StartOffset.RowOffset, StartOffset.ColumnOffset);
        }
        public IEnumerable<Pos> TilePos()
        {
            foreach (Pos ps in Tiles[rotationState])
            {
                yield return new Pos(ps.RowOffset + offset.RowOffset, ps.ColumnOffset + offset.ColumnOffset);
            }
        }
        public void RotateCW()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }
        public void RotateCCW()
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState = rotationState - 1;
            }
        }
        public void Move(int rowsToMove, int columnsToMove)
        {
            offset.RowOffset += rowsToMove;
            offset.ColumnOffset += columnsToMove;
        }
        public void Reset()
        {
            rotationState = 0;
            offset.RowOffset = StartOffset.RowOffset;
            offset.ColumnOffset = StartOffset.ColumnOffset;    
        }
    }
}
