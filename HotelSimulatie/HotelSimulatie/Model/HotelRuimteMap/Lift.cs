using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Lift : HotelRuimte
    {
        public int Bestemming { get; set; }
        public int Positie { get; set; }
        public int Aantal_Personen { get; set; }
        public Lift()
        {
            Naam = "Lift";
            TextureCode = 6;
        }
    }
}
