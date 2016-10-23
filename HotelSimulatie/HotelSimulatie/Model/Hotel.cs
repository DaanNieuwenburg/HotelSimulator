using HotelSimulatie.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Hotel
    {
        public HotelLayout hotelLayout { get; set; }
        public Lobby LobbyRuimte { get; set; }
        public List<Gast> GastenLijst { get; set; }
        public Schoonmaker Schoonmaker_A { get; set; }
        public Schoonmaker Schoonmaker_B { get; set; }
        public bool IsEvacuatie { get; set; }
        public Hotel()
        {
            IsEvacuatie = false;
            hotelLayout = new HotelLayout();
            GastenLijst = new List<Gast>();
            Schoonmaker_A = new Schoonmaker();
            Schoonmaker_B = new Schoonmaker();   
        }

        public void Evacueer()
        {
            if (IsEvacuatie == false)
            {
                IsEvacuatie = true;
                Console.WriteLine("OMG EVACUATIE");
                foreach (Gast gast in GastenLijst)
                {
                    gast.Bestemming = LobbyRuimte;
                    gast.BestemmingLijst = null;
                    gast.HuidigEvent.NEvent = HotelEventAdapter.NEventType.EVACUATE;
                }
            }
            else
            {
                int aantalGasten = GastenLijst.Count;
                int aantalGastenOpEvacuatiePunt = (from Gast in GastenLijst where Gast.HuidigeRuimte == LobbyRuimte select Gast).Count();
                if(aantalGasten == aantalGastenOpEvacuatiePunt)
                {
                    Console.WriteLine("Evacuatie completed");
                    foreach(Gast gast in GastenLijst)
                    {
                        gast.HuidigeRuimte = LobbyRuimte;
                        gast.HuidigEvent.NEvent = HotelEventAdapter.NEventType.GOTO_ROOM;
                    }
                }
            }

        }
    }
}
