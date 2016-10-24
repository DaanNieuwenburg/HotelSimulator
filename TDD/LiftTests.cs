using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie.Model;

namespace TDD
{
    [TestClass]
    public class LiftTests
    {
        [TestMethod]
        public void TestCreatieVanLiftInFactory()
        {
            // Test de creatie van de lift in de factory
            HotelRuimteFactory hotelRuimteFactory = new HotelRuimteFactory();
            HotelRuimte liftHRuimte = hotelRuimteFactory.MaakHotelRuimte("Lift");
            Assert.IsInstanceOfType(liftHRuimte, typeof(HotelSimulatie.Model.Lift));
        }

        [TestMethod]
        public void TestStartPositieVanLift()
        {
            // Test of de lift start vanaf verdieping 0
            HotelRuimteFactory hotelRuimteFactory = new HotelRuimteFactory();
            HotelRuimte liftHRuimte = hotelRuimteFactory.MaakHotelRuimte("Lift");
            Lift lift = (Lift)liftHRuimte;
            Assert.AreEqual(lift.HuidigeVerdieping, lift.Liftschachtlijst[0]);
        }
    }
}
