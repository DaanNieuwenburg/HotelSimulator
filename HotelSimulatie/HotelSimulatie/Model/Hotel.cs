﻿using HotelSimulatie.Model;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Hotel
    {
        public HotelLayout hotelLayout { get; set; }
        public List<Persoon> PersonenInHotelLijst { get; set; }
        public HotelEventAdapter huidigEvent { get; set; }
        private HotelTijdsEenheid hotelTijdsEenheid { get; set; }
        private bool isEvacuatie { get; set; }
        public Hotel()
        {
            huidigEvent = new HotelEventAdapter(new HotelEvents.HotelEvent() { EventType = HotelEvents.HotelEventType.NONE });
            hotelLayout = new HotelLayout();
            PersonenInHotelLijst = new List<Persoon>();
            Schoonmaker SchoonmakerEen = new Schoonmaker() { Naam = "SchoonmakerTim" };
            Schoonmaker SchoonmakerTwee = new Schoonmaker() { Naam = "SchoonmakerVincent" };
            SchoonmakerEen.Collega = SchoonmakerTwee;
            SchoonmakerTwee.Collega = SchoonmakerEen;
            PersonenInHotelLijst.Add(SchoonmakerEen);
            PersonenInHotelLijst.Add(SchoonmakerTwee);
        }

        public void Update()
        {
            if(huidigEvent.Event == HotelEventAdapter.EventType.EVACUATE)
            {
                Evacueer();
            }
            
        }

        private void Evacueer()
        {
            if (isEvacuatie == false)
            {
                isEvacuatie = true;
                Console.WriteLine("OMG EVACUATIE");
                foreach (Gast gast in PersonenInHotelLijst.Where(o => o is Gast))
                {
                    gast.Bestemming = hotelLayout.lobby;
                    gast.BestemmingLijst = null;
                    gast.HuidigEvent.Event = HotelEventAdapter.EventType.EVACUATE;
                }
            }
            else
            {
                int aantalGasten = (from gast in PersonenInHotelLijst.OfType<Gast>().Cast<Gast>() where gast.isDood == false select gast).Count();
                int aantalGastenOpEvacuatiePunt = (from gast in PersonenInHotelLijst.OfType<Gast>().Cast<Gast>() where gast.HuidigeRuimte == hotelLayout.lobby select gast).Count();
                if(aantalGasten == aantalGastenOpEvacuatiePunt)
                {
                    Console.WriteLine("Evacuatie completed");
                    foreach(Gast gast in PersonenInHotelLijst.Where(o => o is Gast))
                    {
                        gast.HuidigeRuimte = hotelLayout.lobby;
                        gast.Bestemming = gast.ToegewezenKamer;
                        gast.HuidigEvent.Event = HotelEventAdapter.EventType.GOTO_ROOM;
                    }
                    huidigEvent.Event = HotelEventAdapter.EventType.NONE;
                }
            }
        }
    }
}
