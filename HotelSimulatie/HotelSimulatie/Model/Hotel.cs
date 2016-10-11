using HotelSimulatie.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Hotel
    {
        public static List<HotelRuimte> NodeLijst { get; set; }
        public Lobby LobbyRuimte { get; set; }
        public List<Kamer> KamerLijst { get; set; }
        public List<Gast> GastenLijst { get; set; }
        public Bioscoop bioscoop { get; set; }
        public Schoonmaker Schoonmaker_A { get; set; }
        public Schoonmaker Schoonmaker_B { get; set; }
        public Liftschacht lift { get; set; }
        public Fitness fitness { get; set; }
        public Hotel()
        {
            NodeLijst = new List<HotelRuimte>();
            GastenLijst = new List<Gast>();
            Schoonmaker_A = new Schoonmaker();
            Schoonmaker_B = new Schoonmaker();            
            LayoutLezer layoutLezer = new LayoutLezer();
            NodeLijst = layoutLezer.HotelRuimteLijst;
            lift = new Liftschacht(0);
            KamerLijst = maakKamerLijst();

            // Koppelt de bioscoop
            bioscoop = (Bioscoop)NodeLijst.OfType<Bioscoop>().First();

            // Koppelt de fitness
            fitness = (Fitness)NodeLijst.OfType<Fitness>().First();
        }

        private List<Kamer> maakKamerLijst()
        {
            KamerLijst = new List<Kamer>();
            int kamerNummerTeller = 1;
            foreach (HotelRuimte hotelRuimte in NodeLijst)
            {
                if(hotelRuimte is Kamer)
                {
                    Kamer kamer = (Kamer)hotelRuimte;
                    kamer.Kamernummer = kamerNummerTeller;
                    KamerLijst.Add((Kamer)hotelRuimte);
                    kamerNummerTeller++;
                }
            }
            return KamerLijst;
        }
    }
}
