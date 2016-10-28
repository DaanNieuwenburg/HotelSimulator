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
        public enum NEventCategory { Cleaning, Testing, Guest, Hotel, NotImplented}
        // Let op - enkel waardes aan het eind van deze enum toevoegen, i.v.m. cast op de originele EventType uit dll
        public enum NEventType { NONE, CHECK_IN, CHECK_OUT, CLEANING_EMERGENCY, EVACUATE, GODZILLA, NEED_FOOD, GOTO_CINEMA, GOTO_FITNESS, START_CINEMA, GOTO_ROOM, IS_CLEANING }
        public NEventType NEvent { get; set; }
        public NEventCategory Category { get; set; }
        public int HuidigeDuurEvent { get; set; }
        public HotelEventAdapter(HotelEvent evt)
        {
            // Bepaal event category en type
            NEvent = (NEventType)evt.EventType;
            Message = evt.Message;
            Data = evt.Data;
            Time = evt.Time;

            bepaalHotelEventCategory(evt);

            // Als er geen sprake is van zo een vreselijk test event
            if (Category != NEventCategory.NotImplented && Category != NEventCategory.Hotel)
            {
                Message = evt.Data.Values.ElementAt(0);
            }
        }

        private void bepaalHotelEventCategory(HotelEvent evt)
        {
            // Initialiseer de event categories
            NEventType[] cleaningEvents = new NEventType[1];
            cleaningEvents[0] = NEventType.CLEANING_EMERGENCY;

            NEventType[] guestEvents = new NEventType[5];
            guestEvents[0] = NEventType.CHECK_IN;
            guestEvents[1] = NEventType.CHECK_OUT;
            guestEvents[2] = NEventType.GOTO_CINEMA;
            guestEvents[3] = NEventType.GOTO_FITNESS;
            guestEvents[4] = NEventType.NEED_FOOD;

            NEventType[] hotelEvents = new NEventType[2];
            hotelEvents[0] = NEventType.START_CINEMA;
            hotelEvents[1] = NEventType.EVACUATE;

            // Bepaal de event category van het huidige event
            if (cleaningEvents.Contains(NEvent))
            {
                Category = NEventCategory.Cleaning;
            }
            else if (guestEvents.Contains(NEvent))
            {
                Category = NEventCategory.Guest;
            }
            else if (hotelEvents.Contains(NEvent))
            {
                Category = NEventCategory.Hotel;
            }
            else
            {
                Category = NEventCategory.NotImplented;
            }
        }
    }
}