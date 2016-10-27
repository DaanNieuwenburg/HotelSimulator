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
        public List<Gast> GastenLijst { get; set; }
        public Schoonmaker[] Schoonmakers { get; set; }
        public bool IsEvacuatie { get; set; }
        public Hotel()
        {
            IsEvacuatie = false;
            hotelLayout = new HotelLayout();
            GastenLijst = new List<Gast>();
            Schoonmakers = new Schoonmaker[2];
            Schoonmakers[0] = new Schoonmaker();
            Schoonmakers[1] = new Schoonmaker();
            Schoonmakers[0].Collega = Schoonmakers[1];
            Schoonmakers[1].Collega = Schoonmakers[0];
            // Schoonmakers[0].texturenaam = "AnimatedSchoonmaker1";
            // Schoonmakers[1].texturenaam = "AnimatedSchoonmaker2";
            
        }

        public void Evacueer()
        {
            if (IsEvacuatie == false)
            {
                IsEvacuatie = true;
                Console.WriteLine("OMG EVACUATIE");
                foreach (Gast gast in GastenLijst)
                {
                    gast.Bestemming = hotelLayout.lobby;
                    gast.BestemmingLijst = null;
                    gast.HuidigEvent.NEvent = HotelEventAdapter.NEventType.EVACUATE;
                }
            }
            else
            {
                int aantalGasten = GastenLijst.Count;
                int aantalGastenOpEvacuatiePunt = (from Gast in GastenLijst where Gast.HuidigeRuimte == hotelLayout.lobby select Gast).Count();
                if(aantalGasten == aantalGastenOpEvacuatiePunt)
                {
                    Console.WriteLine("Evacuatie completed");
                    foreach(Gast gast in GastenLijst)
                    {
                        gast.HuidigeRuimte = hotelLayout.lobby;
                        gast.HuidigEvent.NEvent = HotelEventAdapter.NEventType.GOTO_ROOM;
                    }
                }
            }
        }
    }
}
