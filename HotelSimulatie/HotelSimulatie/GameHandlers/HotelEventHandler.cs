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
        private bool eersteKeer { get; set; }
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
            Dictionary<string, string> eventData = evt.Data;
            if (evt.Data != null)
            {
                foreach (KeyValuePair<string, string> gastEvent in eventData)
                {
                    Gast gevondenGast = spel.hotel.GastenLijst.Find(o => o.Naam == gastEvent.Key);
                    if (gevondenGast == null && evt.EventType == HotelEventType.CHECK_IN)
                    {
                        Gast nieuweGast = new Gast() { Naam = gastEvent.Key, Positie = spel.GastSpawnLocatie };
                        nieuweGast.LoadContent(Game.Content);
                        spel.hotel.GastenLijst.Add(nieuweGast);
                        if (evt.EventType == HotelEventType.CHECK_IN)
                        {
                            CheckinEvent(nieuweGast, evt);
                        }
                    }
                    else
                    {
                        if (evt.EventType == HotelEventType.CHECK_IN)
                        {
                            CheckinEvent(gevondenGast, evt);
                        }
                        /*else if (evt.EventType == HotelEventType.CHECK_OUT)
                        {
                            gevondenGast.HuidigeRuimte = gevondenGast.HuidigeRuimte;
                            gevondenGast.Bestemming = spel.hotel.LobbyRuimte;
                            CheckoutEvent(gevondenGast, evt);
                        }*/
                    }
                }
            }
        }

        private void CheckinEvent(Gast gast, HotelEvent hotelEvent)
        {
            // Bepaal kamernummer
            string aantalSterrenKamerStr = Regex.Match(hotelEvent.Data.First().Value, @"([1-9])").Value;
            int aantalSterrenKamer = Convert.ToInt32(aantalSterrenKamerStr);

            // Vervang de hotelevent.Value met het kamernummer, voor hergebruik
            hotelEvent.Message = aantalSterrenKamer.ToString();

            // Zet huidig event om naar inchecken
            gast.HuidigEvent = hotelEvent;

            // Geef gast de bestemming van de lobby
            gast.HuidigeRuimte = spel.hotel.LobbyRuimte;
            gast.Bestemming = spel.hotel.LobbyRuimte;

            // Start het event
            gast.Inchecken(spel.hotel.LobbyRuimte, GameTijd);
        }

        private void CheckoutEvent(Gast gast, HotelEvent hotelEvent)
        {
            Console.WriteLine("Checkoutevent");
            gast.HuidigEvent = hotelEvent;
            gast.Uitchecken(spel.hotel.LobbyRuimte);
        }
    }
}
