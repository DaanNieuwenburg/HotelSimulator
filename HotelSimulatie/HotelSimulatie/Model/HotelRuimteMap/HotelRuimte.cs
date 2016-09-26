using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public abstract class HotelRuimte
    {
        public int Prijs { get; set; }
        public int TextureCode { get; set; }
    }
}
