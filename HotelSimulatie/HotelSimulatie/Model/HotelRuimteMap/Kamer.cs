using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Kamer : HotelRuimte
    {
        public int Kamernummer { get; set; }
        public int AantalSterren { get; set; }
        public Kamer()
        {
            TextureCode = 1;
        }
        
    }
}
