using HotelSimulatie.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEvents;

namespace HotelSimulatie
{
    public class HotelEventAdapter : HotelEvent
    {
        public enum EventCategory { Cleaning, Testing, Guest, Hotel, NotImplented };
        public Gast gast { get; set; }
        public Kamer hotelKamer { get; set; }
        public EventCategory Category { get; set; }
        public string message { get; set; }
        public HotelEventAdapter(HotelEvent evt, List<Gast> gastenLijst)
        {
            EventType = evt.EventType;
            Message = evt.Message;
            Time = evt.Time;
            message = evt.Data.Values.ElementAt(0);
            
            bepaalHotelEventCategory(evt);
            bepaalGast(evt, gastenLijst);
            if(Category == EventCategory.Cleaning)
            {
                // Koppel kamer
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
            hotelEvents[0] = HotelEventType.EVACUATE;
            hotelEvents[1] = HotelEventType.START_CINEMA;

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
            // Als gast naam gast is, koppel dan de value aan de gastnaam
            evt.Data.Keys.ElementAt(0);
            string gastNaam = evt.Data.Keys.ElementAt(0);
            if (evt.Data.Keys.ElementAt(0) == "Gast")
            {
                gastNaam = gastNaam + evt.Data.Values.ElementAt(0);
            }

            // Vind de gast in gastenlijst
            gast = gastenLijst.Find(o => o.Naam == gastNaam);
        }

        private void bepaalKamer(HotelEvent evt)
        {

        }
    }
}
