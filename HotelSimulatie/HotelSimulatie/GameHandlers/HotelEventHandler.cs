using HotelSimulatie.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEvents;
using System.Text.RegularExpressions;

namespace HotelSimulatie
{
    public class HotelEventHandler : Microsoft.Xna.Framework.GameComponent, HotelEventListener
    {
        private Simulatie spel { get; set; }
        private HotelEvent Event { get; }
        private GameTime GameTijd { get; set; }

        public int Tijd { get; set; }

        public HotelEventHandler(Game game) : base(game)
        {
            spel = (Simulatie)game;
            Event = new HotelEvent();
            // Start de event listener
            HotelEventManager.Register(this);
            HotelEventManager.Start();
        }

        public void Notify(HotelEvent evt)
        {
            // Adapter gebruiken om HotelEvent om te zetten
            HotelEventAdapter hotelEventAdapter = new HotelEventAdapter(evt, spel.hotel.GastenLijst);

            if(hotelEventAdapter.Category == HotelEventAdapter.EventCategory.Guest && spel.hotel.IsEvacuatie == false)
            {
                if(hotelEventAdapter.EventType == HotelEventType.CHECK_IN)
                {
                    CheckinEvent(hotelEventAdapter.gast, hotelEventAdapter);
                }
                else if(hotelEventAdapter.EventType == HotelEventType.CHECK_OUT)
                {
                    hotelEventAdapter.gast.HuidigeRuimte = hotelEventAdapter.gast.HuidigeRuimte;
                    hotelEventAdapter.gast.Bestemming = spel.hotel.LobbyRuimte;
                    CheckoutEvent(hotelEventAdapter.gast, hotelEventAdapter);
                }
                else if(hotelEventAdapter.EventType == HotelEventType.GOTO_CINEMA)
                {
                    GaNaarBioscoopEvent(hotelEventAdapter.gast, hotelEventAdapter);
                }
                else if(hotelEventAdapter.EventType == HotelEventType.GOTO_FITNESS)
                {
                    GaNaarFitnessEvent(hotelEventAdapter.gast, hotelEventAdapter);
                }
            }
            else if(hotelEventAdapter.Category == HotelEventAdapter.EventCategory.Hotel)
            {
                if(hotelEventAdapter.EventType == HotelEventType.EVACUATE)
                {
                    spel.hotel.Evacueer();
                }
            }
        }

        private void CheckinEvent(Gast gast, HotelEventAdapter hotelEvent)
        {
            // Zet spawnpositie van gast goed
            gast.Positie = spel.GastSpawnLocatie;

            // Zet huidig event om naar inchecken
            gast.HuidigEvent = hotelEvent;

            // Koppel de texture van de  gast
            gast.LoadContent(Game.Content);

            // Geef gast de bestemming van de lobby
            gast.HuidigeRuimte = spel.hotel.LobbyRuimte;
            gast.Bestemming = spel.hotel.LobbyRuimte;

            // Koppel gast aan gastenlijst
            spel.hotel.GastenLijst.Add(gast);

            // Start het event
            gast.Inchecken(spel.hotel.LobbyRuimte, GameTijd);
        }

        private void CheckoutEvent(Gast gast, HotelEventAdapter hotelEvent)
        {
            gast.HuidigEvent = hotelEvent;
            gast.Uitchecken(spel.hotel.LobbyRuimte);
        }

        private void GaNaarBioscoopEvent(Gast gast, HotelEventAdapter hotelEvent)
        {
            gast.HuidigEvent = hotelEvent;
            gast.GaNaarBioscoop(spel.hotel.bioscoop);
        }

        private void GaNaarFitnessEvent(Gast gast, HotelEventAdapter hotelEvent)
        {
            gast.HuidigEvent = hotelEvent;
            gast.GaNaarFitness(spel.hotel.fitness);
        }
    }
}
