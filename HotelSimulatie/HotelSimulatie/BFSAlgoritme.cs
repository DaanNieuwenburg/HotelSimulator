using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelSimulatie.Model;

namespace HotelSimulatie
{
    public class BFSAlgoritme
    {
        public HotelRuimte VindDichtbijzijndeKamer(HotelRuimte lobby, int aantalSterren)
        {
            bool found = false;
            HotelRuimte foundHotelRuimte = null;
            Queue<HotelRuimte> HotelRuimteQueue = new Queue<HotelRuimte>();
            HotelRuimteQueue.Enqueue(lobby);
            while (HotelRuimteQueue.Count > 0 && found == false)
            {
                HotelRuimte hotelRuimte = HotelRuimteQueue.Dequeue();
                if (hotelRuimte != null && hotelRuimte is Kamer)
                {
                    Kamer kamer = (Kamer)hotelRuimte;
                    if(kamer.Bezet == false && kamer.AantalSterren == aantalSterren)
                    {
                        foundHotelRuimte = hotelRuimte;
                        return foundHotelRuimte;
                    }
                }
                else
                {
                    foreach(HotelRuimte buur in hotelRuimte.Buren.Keys)
                    {
                        if (buur is Lift)
                        {
                            HotelRuimteQueue.Enqueue(buur);
                        }
                    }
                }
            }
            return foundHotelRuimte;
        }
    }
}
