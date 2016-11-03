using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie.Model;
using System.Collections.Generic;

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
            lift.Liftschachtlijst = new List<Liftschacht>();
            Liftschacht ls = ((Liftschacht)hotelRuimteFactory.MaakHotelRuimte("Liftschacht"));
            ls.Verdieping = 0;
            lift.Liftschachtlijst.Add(ls);
            Assert.AreEqual(lift.HuidigeVerdieping, lift.Liftschachtlijst[0]);
        }
        [TestMethod]
        public void TestVerplaatsenVanLift()
        {
            //Test de verplaats functie van de lift
            HotelRuimteFactory hotelruimteFactory = new HotelRuimteFactory();
            HotelRuimte liftHRuimte = hotelruimteFactory.MaakHotelRuimte("Lift");
            Lift lift = (Lift)liftHRuimte;
            lift.Liftschachtlijst = new List<Liftschacht>();
            lift.Liftschachtlijst.Add((Liftschacht)hotelruimteFactory.MaakHotelRuimte("Liftschacht"));
            lift.Liftschachtlijst.Add((Liftschacht)hotelruimteFactory.MaakHotelRuimte("Liftschacht"));
            lift.Liftschachtlijst.Add((Liftschacht)hotelruimteFactory.MaakHotelRuimte("Liftschacht"));
            lift.LiftBestemming = lift.Liftschachtlijst[2];
            Assert.IsTrue(lift.VerplaatsLift());
        }

    }
}
