using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie;

namespace TDD
{
    [TestClass]
    public class HotelEventAdapterTests
    {
        [TestMethod]
        public void Zou_instantie_van_hotelEventAdapter_moeten_returnen_bij_aanmaken_adapter()
        {
            HotelEventAdapter hea = new HotelEventAdapter(null);
            //Assert.
        }
    }
}
