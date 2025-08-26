namespace Ttrs
{
    public class JBlok : Blok
    {
        private readonly Pos[][] tiles = new Pos[][]
        {
                    new Pos[] { new Pos(0,0), new Pos(1,0), new Pos(1,1), new Pos(1,2)},
                    new Pos[] { new Pos(0,1), new Pos(0,2), new Pos(1,1), new Pos(2,1)},
                    new Pos[] { new Pos(1,0), new Pos(1,1), new Pos(1,2), new Pos(2,2)},
                    new Pos[] { new Pos(0,1), new Pos(1,1), new Pos(2,0), new Pos(2,1)}

        };
        public override int Id => 2;
        protected override Pos StartOffset => new Pos(0, 3);
        protected override Pos[][] Tiles => tiles;
    }
}
