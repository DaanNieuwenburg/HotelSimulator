using HotelSimulatie.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulatie
{
    public class DijkstraAlgoritme
    {
        public HotelRuimte Begin { get; set; }
        public HotelRuimte Eind { get; set; }
        public List<HotelRuimte> open { get; set; }
        public bool zoekDichtbijzijnde { get; set; }
        private List<HotelRuimte> bezochteRuimtes { get; set; }

        public List<HotelRuimte> MaakAlgoritme(Gast gast, HotelRuimte begin, HotelRuimte eind)
        {
            Begin = begin;
            Eind = eind;
            open = new List<HotelRuimte>();
            bezochteRuimtes = new List<HotelRuimte>();
            
            HotelRuimte temp = Begin;
            while (!Bezoek(temp, Eind))
            {
                temp = open.Aggregate((l, r) => l.Afstand < r.Afstand ? l : r);
            }
            
            ResetAfstanden();
            return MaakPad();
        }

        private List<HotelRuimte> MaakPad()
        {
            List<HotelRuimte> pad = new List<HotelRuimte>();
            HotelRuimte deze = Eind;
            pad.Add(Eind);
            while (deze != Begin)
            {
                if(deze.Vorige != Begin)
                {
                    pad.Add(deze.Vorige);
                }

                // Reset de afstand, anders is de afstand bij een volgend gebruik altijd 0
                deze.Afstand = Int32.MaxValue / 2;
                deze = deze.Vorige;
            }

            // Reset de afstand van de lobby
            Begin.Afstand = Int32.MaxValue / 2;
            pad.Add(Begin);
            pad.Reverse();
            if (Eind.GetType() == typeof(Lobby))
            {
                return pad;
            }
            else
            {
                return pad;
            }
        }

        private bool Bezoek(HotelRuimte deze, HotelRuimte eind)
        {
            deze.Afstand = 0;

            // Als de dichtbijzijnde hotelkamer gezocht moet worden
            Kamer TypeKamer = new Kamer(0);
            if (zoekDichtbijzijnde == true && deze.GetType() == eind.GetType() && eind.GetType() == TypeKamer.GetType())
            {
                Kamer teVindenKamer = (Kamer)eind;
                Kamer gevondenKamer = (Kamer)deze;
                if (gevondenKamer.Kamernummer == teVindenKamer.Kamernummer && gevondenKamer.Bezet == false)
                {
                    Eind = deze;
                    return true;
                }
            }

            // Als de dichtbijzijnde eetzaal gezocht moet worden
            else if(zoekDichtbijzijnde == true && deze.GetType() == eind.GetType())
            {
                Eind = deze;
                return true;
            }

            else if (zoekDichtbijzijnde == false && deze == eind)
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
                bezochteRuimtes.Add(x.Key);
            }
            return false;
        }
        
        private void ResetAfstanden()
        {
            foreach(HotelRuimte hotelRuimte in bezochteRuimtes)
            {
                hotelRuimte.Afstand = Int32.MaxValue / 2;
            }
        }
    }
}
