using HotelSimulatie.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    public class Algortime
    {
        public HotelRuimte Begin { get; set; }
        public HotelRuimte Eind { get; set; }
        public List<HotelRuimte> open { get; set; }
        public Algortime(HotelRuimte begin, HotelRuimte eind)
        {
            Begin = begin;
            Eind = eind;
            open = new List<HotelRuimte>();

            HotelRuimte Temp = Begin;
            while (!Bezoek(Temp, Eind))
            {
                Temp = open.Aggregate((l, r) => l.Afstand < r.Afstand ? l : r);
            }
            Console.WriteLine(MaakPad());
        }
        public string MaakPad()
        {
            HotelRuimte deze = Eind;
            string path = "het snelste pad = ";
            while (deze != Begin)
            {
                path += " " + deze.Naam;
                deze = deze.Vorige;
            }
            path += " " + "begin";
            return path;
        }
        public bool Bezoek(HotelRuimte deze, HotelRuimte eind)
        {
            // Bezoek node
            Console.WriteLine("Bezoek Node: " + deze.Naam);
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
