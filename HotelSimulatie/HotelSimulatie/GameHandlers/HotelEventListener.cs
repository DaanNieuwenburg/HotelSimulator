﻿using HotelSimulatie.Model;
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
            // Zet hotelevent om naar adapter
            HotelEventAdapter hotelEventAdapter = new HotelEventAdapter(evt);

            if (hotelEventAdapter.Category == HotelEventAdapter.NEventCategory.Guest)
            {
                // Koppel event aan gast
                Gast gast = bepaalGast(hotelEventAdapter);
                gast.HuidigEvent = hotelEventAdapter;
            }
            else if (hotelEventAdapter.Category == HotelEventAdapter.NEventCategory.Cleaning)
            {
                // Bepaal schoon te maken ruimte
                int kamerCode = Convert.ToInt32(hotelEventAdapter.Message);
                HotelRuimte gevondenKamer = spel.hotel.hotelLayout.HotelRuimteLijst.Find(o => o.Code == kamerCode);
                if (gevondenKamer != null)
                {
                    Schoonmaker schoonmaker = (Schoonmaker)spel.hotel.PersonenInHotelLijst.Where(o => o is Schoonmaker).First();
                    schoonmaker.VoegSchoonmaakRuimteToe(gevondenKamer);
                }
            }
            else if (hotelEventAdapter.Category == HotelEventAdapter.NEventCategory.Hotel)
            {
                // Koppel event aan hotelruimte
                if (hotelEventAdapter.NEvent == HotelEventAdapter.NEventType.EVACUATE)
                {
                    spel.hotel.huidigEvent.NEvent = HotelEventAdapter.NEventType.EVACUATE;
                }
                else if (hotelEventAdapter.NEvent == HotelEventAdapter.NEventType.START_CINEMA)
                {
                    int code = Convert.ToInt32(hotelEventAdapter.Data.First().Value);
                    Bioscoop bioscoop = (Bioscoop)spel.hotel.hotelLayout.HotelRuimteLijst.Find(o => o.Code == code);
                    bioscoop.HuidigEvent = hotelEventAdapter;
                }
            }
        }

        private Gast bepaalGast(HotelEventAdapter hotelEvent)
        {
            // Haal gastnaam op
            hotelEvent.Data.Keys.ElementAt(0);
            string gastNaam = hotelEvent.Data.Keys.ElementAt(0);

            // Als gastnaam gast is, pak de value van de key ( de gast id )
            if (hotelEvent.Data.Keys.ElementAt(0) == "Gast")
            {
                gastNaam = gastNaam + hotelEvent.Data.Values.ElementAt(0);
            }

            // Vind de gast in de gastenlijst
            Gast gast = (Gast)spel.hotel.PersonenInHotelLijst.Find(o => o.Naam == gastNaam);

            if (gast == null && hotelEvent.NEvent == HotelEventAdapter.NEventType.CHECK_IN)
            {
                // Maak een nieuwe gast
                gast = new Gast();
                gast.Naam = gastNaam;

                // Zet de gast in gastenlijst
                spel.hotel.PersonenInHotelLijst.Add(gast);
                string aantalSterrenKamerStr = Regex.Match(hotelEvent.Data.First().Value, @"([1-9])").Value;
                gast.AantalSterrenKamer = Convert.ToInt32(aantalSterrenKamerStr);

                // Zet spawnpositie van gast goed
                gast.Positie = spel.GastSpawnLocatie;

                // Koppel de texture van de gast
                gast.LoadContent(Game.Content);

                // Geef gast de bestemming van de lobby
                gast.HuidigeRuimte = spel.hotel.hotelLayout.lobby;
                gast.Bestemming = spel.hotel.hotelLayout.lobby;

                // Start het event
                gast.Inchecken(spel.hotel.hotelLayout, gametime);
            }
            return gast;
        }
    }
}
