using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;
using DLL = HotelEvents;
using HotelSimulatie.Model;

namespace TDD
{
    [TestClass]
    public class HotelEventAdapterTests
    {
        [TestCategory("HotelEventAdapter creëren en returnen")]
        [TestMethod]
        public void Zou_adapter_moeten_aanmaken()
        {
            HotelEventAdapter adapter = new HotelEventAdapter();
            Assert.IsInstanceOfType(adapter, typeof(HotelEventAdapter));
        }

        [TestMethod]
        public void Zou_persoon_event_klasse_moeten_returnen_bij_ruimte_eventtype()
        {
            // Kijkt of adapter een persoon event teruggeeft.
            HotelEventAdapter adapter = new HotelEventAdapter();
            Event evt = adapter.ConverteerNaarHotelEvent(null, new DLL.HotelEvent() { EventType = DLL.HotelEventType.CHECK_IN });
            Assert.IsInstanceOfType(evt, typeof(PersoonEvent));
        }

        [TestMethod]
        public void Zou_ruimte_event_klasse_moeten_returnen_bij_persoon_eventtype()
        {
            // Kijkt of adapter een ruimte event teruggeeft.
            HotelEventAdapter adapter = new HotelEventAdapter();
            Event evt = adapter.ConverteerNaarHotelEvent(null, new DLL.HotelEvent() { EventType = DLL.HotelEventType.START_CINEMA });
            Assert.IsInstanceOfType(evt, typeof(RuimteEvent));
        }

        [TestMethod]
        public void Zou_null_moeten_returnen_bij_leeg_event()
        {
            // Kijkt of adapter null teruggeeft.
            HotelEventAdapter adapter = new HotelEventAdapter();
            Assert.IsNull(adapter.ConverteerNaarHotelEvent(null, new DLL.HotelEvent()));
        }


        [TestCategory("HotelEventAdapter koppelen van gasten")]
        [TestMethod]
        public void Zou_gast_moeten_koppelen_bij_meegegeven_gastcode_vanuit_dll()
        {
            Gast gast = new Gast();
            gast.Naam = "Gast1";
            Hotel hotel = new Hotel();
            hotel.PersonenInHotelLijst.Add(gast);


            HotelEventAdapter adapter = new HotelEventAdapter();
            PersoonEvent evt = (PersoonEvent)adapter.ConverteerNaarHotelEvent(hotel, new DLL.HotelEvent() { EventType = DLL.HotelEventType.GOTO_FITNESS });
            Assert.AreEqual(evt.persoon, gast);
        }
    }
}
