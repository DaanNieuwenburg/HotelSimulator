using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class HotelRuimteFactory
    {
        public HotelRuimte MaakHotelRuimte(string soort, int verdieping = 0)
        {
            if (soort == "Bioscoop")
                return new Bioscoop();
            else if (soort == "Eetzaal")
                return new Eetzaal();
            else if (soort == "Fitness")
                return new Fitness();
            else if (soort == "Liftschacht")
                return new Liftschacht(verdieping);
            else if (soort == "Lobby")
                return new Lobby();
            else if (soort == "Trap")
                return new Trap();
            else if (soort == "Kamer")
                return new Kamer(1);
            else if (soort == "Gang")
                return new Gang();
            else if (soort == "Lift")
                return new Lift(verdieping);
            else if (soort == "Zwembad")
                return new Zwembad();
            else
                return null;
        }
    }
}
