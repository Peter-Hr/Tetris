using System;

namespace Ttrs
{
    public class BlokSystem
    {
        private readonly Blok[] bloks = new Blok[]
        {
            new IBlok(),
            new JBlok(),
            new LBlok(),
            new OBlok(),
            new SBlok(),
            new TBlok(),
            new ZBlok(),
        };
        private readonly Random rnd = new Random();
        public Blok NextBlok { get; private set; }
        public BlokSystem()
        {
            NextBlok = RndBlok();
        }
        private Blok RndBlok()
        { 
            return bloks[rnd.Next(bloks.Length)]; 
        }
        public Blok GiveBlokGenNew()
        {
            //funkcia odovzdá blok a rovno vyrobí nový
            Blok blok = NextBlok;
            do
            {
                NextBlok = RndBlok();
            }
            while (blok.Id == NextBlok.Id);
            return blok;
        }



    }
}
