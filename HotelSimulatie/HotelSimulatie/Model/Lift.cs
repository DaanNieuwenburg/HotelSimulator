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
        public int verdieping { get; set; }
        public Dictionary<Gast, int> GasteninLift { get; set; }

        public Lift()
        {
            GasteninLift = new Dictionary<Gast, int>();
        }
        public void UpdateLift(Gast gast)
        {
            int? a = null;
            int? b = null;
            foreach(HotelRuimte ruimte in gast.Bestemminglijst)
            {
                if(ruimte is Liftschacht)
                {
                    if (a == null)
                        a = (int)ruimte.CoordinatenInSpel.Y;
                    else
                    {
                        b = (int)ruimte.CoordinatenInSpel.Y;
                        break;
                    }
                        
                }
            }
            // Kijk of de bestemming omhoog of omlaag is
            if((a - b) < 0)
            {
                GasteninLift.Add(gast, gast.Bestemming.Verdieping + gast.Bestemminglijst.OfType<Liftschacht>().Count());
            }
            else
            {
                GasteninLift.Add(gast, gast.Bestemming.Verdieping - gast.Bestemminglijst.OfType<Liftschacht>().Count());
            }
            
        }

    }
}
