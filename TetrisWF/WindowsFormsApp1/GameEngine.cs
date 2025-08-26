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
            foreach (Pos p in CurrentBlok.TilePos())
            {
                if (!GameSpace.IsCellEmpty(p.RowOffset, p.ColumnOffset))
                {
                    return false;

                }
            }
            return true;

        }
    }
}
