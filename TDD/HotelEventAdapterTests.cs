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
        public void Zou_tijd_moeten_koppelen_bij_meegegeven_tijd()
        {
            int time = 10;
            HotelEvents.HotelEvent evt = new HotelEvents.HotelEvent();
            evt.Time = time;
            evt.Data = new System.Collections.Generic.Dictionary<string, string>();
            HotelEventAdapter hea = new HotelEventAdapter(evt);

            bool test = hea.Tijd == time;
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void Zou_data_moeten_koppelen_bij_meegegeven_data()
        {
            int time = 10;
            HotelEvents.HotelEvent evt = new HotelEvents.HotelEvent();
            evt.Time = time;
            evt.Data = new System.Collections.Generic.Dictionary<string, string>();
            evt.Data.Add("test", "test");
            HotelEventAdapter hea = new HotelEventAdapter(evt);

            bool test = evt.Data.Keys == hea.Data.Keys;
            Assert.IsTrue(test);
        }
    }
}
