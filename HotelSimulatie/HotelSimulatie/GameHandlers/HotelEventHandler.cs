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
        private GameTime gametime { get; set; }

        public HotelEventHandler(Game game) : base(game)
        {
            spel = (Simulatie)game;
            Event = new HotelEvent();
            // Start de event listener
            HotelEventManager.Register(this);
            HotelEventManager.Start();
        }
        public override void Update(GameTime gameTime)
        {
            gametime = gameTime;
            base.Update(gameTime);
        }
        public void Notify(HotelEvent evt)
        {
            // Adapter gebruiken om HotelEvent om te zetten
            HotelEventAdapter hotelEventAdapter = new HotelEventAdapter(evt, spel.hotel.GastenLijst);

            if(hotelEventAdapter.Category == HotelEventAdapter.NEventCategory.Guest && spel.hotel.IsEvacuatie == false)
            {
                if(hotelEventAdapter.NEvent == HotelEventAdapter.NEventType.CHECK_IN)
                {
                    CheckinEvent(hotelEventAdapter.gast, hotelEventAdapter);
                }
                else if(hotelEventAdapter.NEvent == HotelEventAdapter.NEventType.CHECK_OUT)
                {
                    hotelEventAdapter.gast.HuidigeRuimte = hotelEventAdapter.gast.HuidigeRuimte;
                    hotelEventAdapter.gast.Bestemming = spel.hotel.hotelLayout.lobby;
                    CheckoutEvent(hotelEventAdapter.gast, hotelEventAdapter);
                }
                else if(hotelEventAdapter.NEvent == HotelEventAdapter.NEventType.GOTO_CINEMA)
                {
                    GaNaarBioscoopEvent(hotelEventAdapter.gast, hotelEventAdapter);
                }
                else if(hotelEventAdapter.NEvent == HotelEventAdapter.NEventType.GOTO_FITNESS)
                {
                    GaNaarFitnessEvent(hotelEventAdapter.gast, hotelEventAdapter);
                }
                else if(hotelEventAdapter.NEvent == HotelEventAdapter.NEventType.GOTO_ROOM)
                {
                    GaNaarEigenKamerEvent(hotelEventAdapter.gast, hotelEventAdapter);
                }
                else if (hotelEventAdapter.NEvent == HotelEventAdapter.NEventType.NEED_FOOD)
                {
                    GaNaarEetzaalEvent(hotelEventAdapter.gast, hotelEventAdapter);
                }
            }
            else if (hotelEventAdapter.Category == HotelEventAdapter.NEventCategory.Hotel)
            {
                if (hotelEventAdapter.NEvent == HotelEventAdapter.NEventType.START_CINEMA)
                {
                    Start_Cinema(hotelEventAdapter);
                }
            }
            else if(hotelEventAdapter.Category == HotelEventAdapter.NEventCategory.Hotel)
            {
                if(hotelEventAdapter.NEvent == HotelEventAdapter.NEventType.EVACUATE)
                {
                    spel.hotel.Evacueer();
                }
            }
            
            else if(hotelEventAdapter.Category == HotelEventAdapter.NEventCategory.Cleaning)
            {
                if(hotelEventAdapter.NEvent == HotelEventAdapter.NEventType.CLEANING_EMERGENCY)
                {
                    SchoonmaakEvent(hotelEventAdapter);
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
            gast.HuidigeRuimte = spel.hotel.hotelLayout.lobby;
            gast.Bestemming = spel.hotel.hotelLayout.lobby;

            // Koppel gast aan gastenlijst
            spel.hotel.GastenLijst.Add(gast);

            // Start het event
            gast.Inchecken(spel.hotel.hotelLayout.lobby, gametime);
        }
        private void Start_Cinema(HotelEventAdapter hotelEvent)
        {
            spel.hotel.hotelLayout.bioscoop.HuidigEvent = hotelEvent;
            spel.hotel.hotelLayout.bioscoop.Start(gametime);
        }
        private void CheckoutEvent(Gast gast, HotelEventAdapter hotelEvent)
        {
            gast.HuidigEvent = hotelEvent;
            Lobby lobby = spel.hotel.hotelLayout.lobby;
            gast.GaNaarKamer<Lobby>(ref lobby);
            SchoonmaakEvent(hotelEvent);
        }

        private void GaNaarBioscoopEvent(Gast gast, HotelEventAdapter hotelEvent)
        {
            gast.HuidigEvent = hotelEvent;
            Bioscoop bioscoop = spel.hotel.hotelLayout.bioscoop;
            gast.GaNaarKamer<Bioscoop>(ref bioscoop);
        }

        private void GaNaarFitnessEvent(Gast gast, HotelEventAdapter hotelEvent)
        {
            gast.HuidigEvent = hotelEvent;
            Fitness fitness = spel.hotel.hotelLayout.fitness;
            gast.GaNaarKamer<Fitness>(ref fitness);
        }

        private void GaNaarEigenKamerEvent(Gast gast, HotelEventAdapter hotelEvent)
        {
            gast.HuidigEvent = hotelEvent;
            Kamer kamer = gast.ToegewezenKamer;
            gast.GaNaarKamer<Kamer>(ref kamer);
        }

        private void GaNaarEetzaalEvent(Gast gast, HotelEventAdapter hotelEvent)
        {
            gast.HuidigEvent = hotelEvent;
            Eetzaal eetzaal = new Eetzaal();
            gast.GaNaarKamer<Eetzaal>(ref eetzaal);
        }

        private void SchoonmaakEvent(HotelEventAdapter hotelEvent)
        {
            // Bepaal kamer
            int kamerCode = Convert.ToInt32(hotelEvent.Message);
            Kamer gevondenKamer = spel.hotel.hotelLayout.KamerLijst.Find(o => o.Code == kamerCode);
            if (gevondenKamer != null)
            {
                spel.hotel.Schoonmakers[0].NieuweSchoonTeMakenKamer(gevondenKamer, hotelEvent, spel.hotel.Schoonmakers[1]);
            }
        }
    }
}
