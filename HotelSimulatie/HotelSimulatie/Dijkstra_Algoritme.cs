using HotelSimulatie.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    public class Algoritme
    {
        public HotelRuimte Begin { get; set; }
        public HotelRuimte Eind { get; set; }
        public List<HotelRuimte> open { get; set; }

        public List<HotelRuimte> MaakAlgoritme(HotelRuimte begin, HotelRuimte eind)
        {
            Begin = begin;
            Eind = eind;
            open = new List<HotelRuimte>();

            HotelRuimte Temp = Begin;
            while (!Bezoek(Temp, Eind))
            {
                Temp = open.Aggregate((l, r) => l.Afstand < r.Afstand ? l : r);
            }

            return MaakPad();
        }

        private List<HotelRuimte> MaakPad()
        {
            List<HotelRuimte> pad = new List<HotelRuimte>();
            HotelRuimte deze = Eind;
            while (deze != Begin)
            {
                if(deze.Vorige != Begin)
                {
                    pad.Add(deze.Vorige);
                }

                // Reset de afstand, anders is de afstand bij een volgend gebruik altijd 0
                deze.Afstand = Int32.MaxValue;
                deze = deze.Vorige;
            }
            pad.Reverse();
            return pad;
        }

        private bool Bezoek(HotelRuimte deze, HotelRuimte eind)
        {
            deze.Afstand = 0;
            if (deze == eind)
            {
                return true;
            }

            if (open.Contains(deze))
            {
                open.Remove(deze);
            }

            foreach (KeyValuePair<HotelRuimte, int> x in deze.Buren)
            {
                int NieuweAfstand = deze.Afstand + x.Value;
                Console.WriteLine(NieuweAfstand);
                if (NieuweAfstand < x.Key.Afstand)
                {
                    x.Key.Afstand = NieuweAfstand;
                    x.Key.Vorige = deze;
                    open.Add(x.Key);
                }
            }
            return false;
        }
    }
}
