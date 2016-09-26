using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Kamer : HotelRuimte
    {
        public Kamer()
        {
            TextureCode = 1;
        }
        public int AantalSterren { get; set; }
    }
}
