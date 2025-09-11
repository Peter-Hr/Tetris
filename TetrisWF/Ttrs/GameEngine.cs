namespace Ttrs
{
    internal class GameEngine
    {
        private Blok currentBlok;
        public Blok CurrentBlok
        {
            get => currentBlok;
            private set
            {
                currentBlok = value;
                currentBlok.Reset();
            }
        }
        public GameSpace GameSpace { get; }
        public BlokSystem BlokSystem { get; }
        public bool GameOver { get; private set; }
        public GameEngine()
        {
            GameSpace = new GameSpace(22, 10);
            BlokSystem = new BlokSystem();
            CurrentBlok = BlokSystem.GiveBlokGenNew();
        }
        private bool BlokFits()
        {
            //kontrola, ci sa blok zmesti do nového miesta
            foreach (Pos p in CurrentBlok.TilePos())
            {
                if (!GameSpace.IsCellEmpty(p.RowOffset, p.ColumnOffset))
                {
                    return false;

                }
            }
            return true;

        }
        public void RotateCWIfPossible()
        {
            CurrentBlok.RotateCW();
            if (!BlokFits())
            {
                CurrentBlok.RotateCCW();
            }
        }
        public void RotateCCWIfPossible()
        {
            CurrentBlok.RotateCCW();
            if (!BlokFits())
            {
                CurrentBlok.RotateCW();
            }
        }
        public void MoveLeftIfPossible()
        {
            CurrentBlok.Move(0, -1);
            if (!BlokFits())
            {
                CurrentBlok.Move(0,1);
            }
        }
        public void MoveRightIfPossible()
        {
            currentBlok.Move(0, 1);
            if (!BlokFits())
            {
                CurrentBlok.Move(0, -1);
            }
        }
        private bool IsGameOver()
        {
            return !(GameSpace.IsRowEmpty(0) && GameSpace.IsRowEmpty(1));
        }
        private void PlaceBlok()
        {
            foreach (Pos p in currentBlok.TilePos())
            {
                GameSpace[p.RowOffset, p.ColumnOffset] = CurrentBlok.Id;
            }
            GameSpace.ClearFullRows();
            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlok = BlokSystem.GiveBlokGenNew();
            }
        }
        public void MoveBlokDown()
        {
            CurrentBlok.Move(1, 0);
            if (!BlokFits())
            {
                CurrentBlok.Move(-1, 0);
                PlaceBlok();
            }
        }
        public void DropBlok()
        {
            while (BlokFits()) { CurrentBlok.Move(1, 0); }
            CurrentBlok.Move(-1, 0);
            PlaceBlok();
        }
        public int GetTtlScore()
        {
            return GameSpace.TotalScore;
        }
        public void EngineReset()
        {
            GameSpace.TotalScore = 0;
            GameOver = false;
            GameSpace.gridReset();
            CurrentBlok = BlokSystem.GiveBlokGenNew();
        }
            

    }
}
