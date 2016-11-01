using HotelSimulatie.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEvents;
using System.Text.RegularExpressions;

namespace HotelSimulatie
{
    public class HotelEventAdapter
    {
        public HotelEvent dllHotelEvent { get; set; }
        public enum EventCategory { Cleaning, Testing, Guest, Hotel, NotImplented }
        // Let op - enkel waardes aan het eind van deze enum toevoegen, i.v.m. cast op de originele EventType uit dll
        public enum EventType { NONE, CHECK_IN, CHECK_OUT, CLEANING_EMERGENCY, EVACUATE, GODZILLA, NEED_FOOD, GOTO_CINEMA, GOTO_FITNESS, START_CINEMA, GOTO_ROOM, IS_CLEANING }
        public EventType Event { get; set; }
        public EventCategory Category { get; set; }
        public int HuidigeDuurEvent { get; set; }
        public int Tijd { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public string Message { get; set; }
        public HotelEventAdapter(HotelEvent evt)
        {
            try
            {
                if (evt != null && evt.Data != null)
                {
                    dllHotelEvent = evt;
                    // Bepaal event category en type
                    Event = (EventType)evt.EventType;
                    Message = evt.Message;
                    Data = evt.Data;
                    Tijd = evt.Time;

                    bepaalHotelEventCategory(evt);

                    // Als er geen sprake is van zo een vreselijk test event
                    if (Category != EventCategory.NotImplented && Category != EventCategory.Hotel)
                    {
                        Message = evt.Data.Values.ElementAt(0);
                    }
                    if(Category == EventCategory.Cleaning)
                    {
                        Console.WriteLine("");
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Exception in hotelEventadapter" + ex);
            }
        }

        private void bepaalHotelEventCategory(HotelEvent evt)
        {
            // Initialiseer de event categories
            EventType[] cleaningEvents = new EventType[1];
            cleaningEvents[0] = EventType.CLEANING_EMERGENCY;

            EventType[] guestEvents = new EventType[5];
            guestEvents[0] = EventType.CHECK_IN;
            guestEvents[1] = EventType.CHECK_OUT;
            guestEvents[2] = EventType.GOTO_CINEMA;
            guestEvents[3] = EventType.GOTO_FITNESS;
            guestEvents[4] = EventType.NEED_FOOD;

            EventType[] hotelEvents = new EventType[3];
            hotelEvents[0] = EventType.START_CINEMA;
            hotelEvents[1] = EventType.EVACUATE;
            hotelEvents[2] = EventType.GODZILLA;

            // Bepaal de event category van het huidige event
            if (cleaningEvents.Contains(Event))
            {
                Category = EventCategory.Cleaning;
            }
            else if (guestEvents.Contains(Event))
            {
                Category = EventCategory.Guest;
            }
            else if (hotelEvents.Contains(Event))
            {
                Category = EventCategory.Hotel;
            }
            else
            {
                Category = EventCategory.NotImplented;
            }
        }
    }
}