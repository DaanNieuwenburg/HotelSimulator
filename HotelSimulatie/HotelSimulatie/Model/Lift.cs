using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Lift
    {
        public int Huidigeverdieping { get; set; }
        public int Volgendeverdieping { get; set; }
        public Dictionary<Gast, int> Bestemmingen { get; set; }

        public Lift()
        {

        }

    }
}
