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
    }
}
