    namespace Ttrs
    {
        public class OBlok : Blok
        {
            private readonly Pos[][] tiles = new Pos[][]
            {
                    new Pos[] { new Pos(0,0), new Pos(0,1), new Pos(1,0), new Pos(1,1)}
            };
            public override int Id => 4;
        protected override Pos StartOffset => new Pos(0, 4);
        protected override Pos[][] Tiles => tiles;
    }
    }
