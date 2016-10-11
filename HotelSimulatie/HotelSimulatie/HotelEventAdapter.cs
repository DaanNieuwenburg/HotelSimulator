using HotelSimulatie.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEvents;
using System.Text.RegularExpressions;

namespace HotelSimulatie
{
    public class HotelEventAdapter : HotelEvent
    {
        public enum EventCategory { Cleaning, Testing, Guest, Hotel, NotImplented };
        public Gast gast { get; set; }
        public int? aantalSterrenKamer { get; set; }
        public EventCategory Category { get; set; }
        public string message { get; set; }
        public HotelEventAdapter(HotelEvent evt, List<Gast> gastenLijst = null)
        {
            EventType = evt.EventType;
            Message = evt.Message;
            Time = evt.Time;

            bepaalHotelEventCategory(evt);

            // Als er geen sprake is van zo een vreselijk test event
            if (Category != EventCategory.NotImplented && Category != EventCategory.Hotel)
            {
                message = evt.Data.Values.ElementAt(0);

                Console.WriteLine(evt.Message);
                Console.WriteLine(message);

                if (Category == EventCategory.Guest)
                {
                    // Bepaal kamernummer
                    if (EventType == HotelEventType.CHECK_IN)
                    {
                        string aantalSterrenKamerStr = Regex.Match(evt.Data.First().Value, @"([1-9])").Value;
                        aantalSterrenKamer = Convert.ToInt32(aantalSterrenKamerStr);
                    }
                    bepaalGast(evt, gastenLijst);
                }
            }
        }

        private void bepaalHotelEventCategory(HotelEvent evt)
        {
            // Initialiseer de event categories
            HotelEventType[] cleaningEvents = new HotelEventType[1];
            cleaningEvents[0] = HotelEventType.CLEANING_EMERGENCY;

            HotelEventType[] guestEvents = new HotelEventType[5];
            guestEvents[0] = HotelEventType.CHECK_IN;
            guestEvents[1] = HotelEventType.CHECK_OUT;
            guestEvents[2] = HotelEventType.GOTO_CINEMA;
            guestEvents[3] = HotelEventType.GOTO_FITNESS;
            guestEvents[4] = HotelEventType.NEED_FOOD;

            HotelEventType[] hotelEvents = new HotelEventType[2];
            hotelEvents[0] = HotelEventType.START_CINEMA;
            hotelEvents[1] = HotelEventType.EVACUATE;

            // Bepaal de event category van het huidige event
            if (cleaningEvents.Contains(evt.EventType))
            {
                Category = EventCategory.Cleaning;
            }
            else if (guestEvents.Contains(evt.EventType))
            {
                Category = EventCategory.Guest;
            }
            else if (hotelEvents.Contains(evt.EventType))
            {
                Category = EventCategory.Hotel;
            }
            else
            {
                Category = EventCategory.NotImplented;
            }
        }

        private void bepaalGast(HotelEvent evt, List<Gast> gastenLijst)
        {
            // Maak en koppel gastnaam
            evt.Data.Keys.ElementAt(0);
            string gastNaam = evt.Data.Keys.ElementAt(0);

            // Als gastnaam gast is, pak de value van de key ( de gast id )
            if (evt.Data.Keys.ElementAt(0) == "Gast")
            {
                gastNaam = gastNaam + evt.Data.Values.ElementAt(0);
            }

            // Vind de gast in de gastenlijst
            gast = gastenLijst.Find(o => o.Naam == gastNaam);

            if (gast == null)
            {
                gast = new Gast();
                gast.Naam = gastNaam;
            }
        }
    }
}