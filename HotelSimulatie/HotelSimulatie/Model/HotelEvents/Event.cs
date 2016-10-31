using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Event
    {
        public enum EventType { NONE, CHECK_IN, CHECK_OUT, CLEANING_EMERGENCY, EVACUATE, GODZILLA, NEED_FOOD, GOTO_CINEMA, GOTO_FITNESS, START_CINEMA, GOTO_ROOM, IS_CLEANING }
        public EventType SoortEvent { get; set; }
    }
}
