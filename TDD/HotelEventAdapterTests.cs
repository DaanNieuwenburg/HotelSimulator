using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;

namespace TDD
{
    [TestClass]
    public class HotelEventAdapterTests
    {
        [TestMethod]
        public void Zou_instantie_van_hotelEventAdapter_moeten_returnen_bij_aanmaken_adapter_met_null_event()
        {
            HotelEventAdapter hea = new HotelEventAdapter(null);
            Assert.IsInstanceOfType(hea, typeof(HotelEventAdapter));
        }

        [TestMethod]
        public void Zou_event_type_niet_moeten_koppelen_bij_geen_meegegeven_data()
        {
            HotelEvents.HotelEvent evt = new HotelEvents.HotelEvent() { EventType = HotelEvents.HotelEventType.CHECK_IN};
            evt.Data = new System.Collections.Generic.Dictionary<string, string>();
            evt.Data.Add("test", "test");
            HotelEventAdapter hea = new HotelEventAdapter(evt);
            Assert.AreSame(evt.EventType, hea.Event);
        }
    }
}
