using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public abstract class HotelRuimte
    {
        public string Naam { get; set; }
        public int Prijs { get; set; }
        public int TextureCode { get; set; }
        public int hoogte { get; set; }
        public int breette { get; set; }
        public int Capaciteit { get; set; }


    }
}
