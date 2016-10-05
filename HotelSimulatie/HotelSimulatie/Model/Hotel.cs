using HotelSimulatie.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Hotel
    {
        public List<HotelRuimte> NodeLijst { get; set; }
        public Lobby LobbyRuimte { get; set; }
        public List<Kamer> KamerLijst { get; set; }
        public List<Gast> GastenLijst { get; set; }
        public Schoonmaker Schoonmaker_A { get; set; }
        public Schoonmaker Schoonmaker_B { get; set; }
        public Liftschacht lift { get; set; }
        public Hotel()
        {
            NodeLijst = new List<HotelRuimte>();
            GastenLijst = new List<Gast>();
            Schoonmaker_A = new Schoonmaker();
            Schoonmaker_B = new Schoonmaker();
            lift = new Liftschacht(0);
            LayoutLezer layoutLezer = new LayoutLezer();
            NodeLijst = layoutLezer.HotelRuimteLijst;
            KamerLijst = maakKamerLijst();
        }

        private List<Kamer> maakKamerLijst()
        {
            KamerLijst = new List<Kamer>();
            foreach (HotelRuimte hotelRuimte in NodeLijst)
            {
                if(hotelRuimte is Kamer)
                {
                    KamerLijst.Add((Kamer)hotelRuimte);
                }
            }
            return KamerLijst;
        }
    }
}
