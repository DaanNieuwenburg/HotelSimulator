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

        public List<HotelRuimte> MaakAlgoritme(Persoon gast, HotelRuimte begin, HotelRuimte eind)
        {
            Begin = begin;
            Eind = eind;
            open = new List<HotelRuimte>();
            bezochteRuimtes = new List<HotelRuimte>();

            HotelRuimte temp = Begin;
            temp.Afstand = 0;
            while (!Bezoek(temp, Eind))
            {
                temp = open.Aggregate((l, r) => l.Afstand < r.Afstand ? l : r);
            }

            ResetAfstanden();
            return MaakPad();
        }

        private List<HotelRuimte> MaakPad()
        {
            Begin.Afstand = 0;
            List<HotelRuimte> pad = new List<HotelRuimte>();
            HotelRuimte deze = Eind;
            pad.Add(Eind);
            while (deze != Begin)
            {
                if (deze.Vorige != Begin)
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
            return pad;
        }

        private bool Bezoek(HotelRuimte deze, HotelRuimte eind)
        {

            // Als de dichtbijzijnde hotelkamer gezocht moet worden
            Kamer TypeKamer = new Kamer(0);
            if (zoekDichtbijzijnde == true && deze.GetType() == eind.GetType() && eind.GetType() == TypeKamer.GetType())
            {
                Kamer teVindenKamer = (Kamer)eind;
                Kamer gevondenKamer = (Kamer)deze;
                if (gevondenKamer.AantalSterren == teVindenKamer.AantalSterren && gevondenKamer.Bezet == false)
                {
                    Eind = deze;
                    return true;
                }
            }

            // Als de dichtbijzijnde eetzaal gezocht moet worden
            else if (zoekDichtbijzijnde == true && deze.GetType() == eind.GetType())
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

            foreach (HotelRuimte hotelRuimte in deze.Buren)
            {
                int NieuweAfstand = deze.Afstand + hotelRuimte.Gewicht;
                if (NieuweAfstand < hotelRuimte.Afstand)
                {
                    hotelRuimte.Afstand = NieuweAfstand;
                    hotelRuimte.Vorige = deze;
                    open.Add(hotelRuimte);
                }
                bezochteRuimtes.Add(hotelRuimte);
            }
            return false;
        }

        private void ResetAfstanden()
        {
            foreach (HotelRuimte hotelRuimte in bezochteRuimtes)
            {
                hotelRuimte.Afstand = Int32.MaxValue / 2;
            }
        }
    }
}
