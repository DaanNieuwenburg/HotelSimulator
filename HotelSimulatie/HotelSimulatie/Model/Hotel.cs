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
        public List<Gast> Gastenlijst { get; set; }
        public List<Kamer> KamerLijst { get; set; }
        public Schoonmaker Schoonmaker_A { get; set; }
        public Schoonmaker Schoonmaker_B { get; set; }
        public Liftschacht lift { get; set; }
        public Hotel()
        {
            NodeLijst = new List<HotelRuimte>();
            Gastenlijst = new List<Gast>();
            Schoonmaker_A = new Schoonmaker();
            Schoonmaker_B = new Schoonmaker();
            lift = new Liftschacht(0);
            Addgasten();    // tijdelijke testcode
            LayoutLezer layoutLezer = new LayoutLezer();
            NodeLijst = layoutLezer.HotelRuimteLijst;
            KamerLijst = maakKamerLijst();
        }

        private void Addgasten()
        {
            // test gast
            Gast gast = new Gast();
            gast.HuidigeRuimte = LobbyRuimte;
            gast.Gastnummer = Gastenlijst.Count + 1;
            Gastenlijst.Add(gast);
            
            Gast gast1 = new Gast();
            gast.HuidigeRuimte = LobbyRuimte;
            gast.Gastnummer = Gastenlijst.Count + 1;
            Gastenlijst.Add(gast1);
            
            Gast gast2 = new Gast();
            gast.HuidigeRuimte = LobbyRuimte;
            gast.Gastnummer = Gastenlijst.Count + 1;
            Gastenlijst.Add(gast2); 
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
