﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie.Model;
using System.Collections.Generic;

namespace TDD
{
    [TestClass]
    public class LiftTests
    {
        [TestMethod]
        public void Zou_op_begane_grond_moeten_wachten_op_personen_bij_aanmaken_lift()
        {
            // Arrange
            HotelRuimteFactory hotelRuimteFactory = new HotelRuimteFactory();
            Lift lift = (Lift)hotelRuimteFactory.MaakHotelRuimte("Lift");
            List<Liftschacht> liftSchachtenLijst = new List<Liftschacht>();
            liftSchachtenLijst.Add(new Liftschacht(2));
            liftSchachtenLijst.Add(new Liftschacht(1));
            liftSchachtenLijst.Add(new Liftschacht(0));
            liftSchachtenLijst.Add(new Liftschacht(4));
            lift.InitializeerLift(liftSchachtenLijst);

            // Assert
            Assert.IsTrue(lift.HuidigeVerdieping.Verdieping == 0);
        }

        [TestMethod]
        public void Zou_persoon_op_bestemming_moeten_afzetten_bij_persoon_die_naar_boven_wilt()
        {
            // Arrange
            HotelRuimteFactory hotelRuimteFactory = new HotelRuimteFactory();
            Lift lift = (Lift)hotelRuimteFactory.MaakHotelRuimte("Lift");

            List<Liftschacht> liftSchachtenLijst = new List<Liftschacht>();
            liftSchachtenLijst.Add(new Liftschacht(2) { lift = lift });
            liftSchachtenLijst.Add(new Liftschacht(1) { lift = lift });
            liftSchachtenLijst.Add(new Liftschacht(0) { lift = lift });
            liftSchachtenLijst.Add(new Liftschacht(4) { lift = lift });

            lift.InitializeerLift(liftSchachtenLijst);

            Gast gast = new Gast();
            gast.HuidigeRuimte = liftSchachtenLijst[0];
            gast.Bestemming = liftSchachtenLijst[3];
            gast.BestemmingLijst = new List<HotelRuimte>();
            gast.BestemmingLijst.Add(liftSchachtenLijst[3]);
            gast.HuidigeRuimte.VoegPersoonToe(gast);

            // Act
            for (int i = 0; i < 10; i++)
            {
                lift.Update(i);
            }

            // Assert
            Assert.IsTrue(gast.HuidigeRuimte.Verdieping == 4);
        }

        [TestMethod]
        public void Zou_persoon_niet_uit_lift_moeten_laten_gaan_na_5_hte_bij_1_verdieping_naar_boven()
        {
            // 5 HTE want instappen op begane grond kost ook 1 HTE. Uitstappen en instappen kost 2 HTE
            // Arrange
            HotelRuimteFactory hotelRuimteFactory = new HotelRuimteFactory();
            Lift lift = (Lift)hotelRuimteFactory.MaakHotelRuimte("Lift");

            List<Liftschacht> liftSchachtenLijst = new List<Liftschacht>();
            liftSchachtenLijst.Add(new Liftschacht(2) { lift = lift });
            liftSchachtenLijst.Add(new Liftschacht(1) { lift = lift });
            liftSchachtenLijst.Add(new Liftschacht(0) { lift = lift });
            liftSchachtenLijst.Add(new Liftschacht(4) { lift = lift });

            lift.InitializeerLift(liftSchachtenLijst);

            Gast gast = new Gast();
            gast.HuidigeRuimte = liftSchachtenLijst[0];
            gast.Bestemming = liftSchachtenLijst[1];
            gast.BestemmingLijst = new List<HotelRuimte>();
            gast.BestemmingLijst.Add(liftSchachtenLijst[1]);
            gast.HuidigeRuimte.VoegPersoonToe(gast);

            // Act
            for (int i = 0; i <= 5; i++)
            {
                lift.Update(i);
            }

            // Assert
            Assert.IsFalse(gast.HuidigeRuimte.Verdieping == 1);
        }

        [TestMethod]
        public void Zou_persoon_uit_lift_moeten_laten_gaan_na_6_hte_bij_1_verdieping_naar_boven()
        {
            // 5 HTE want instappen op begane grond kost ook 1 HTE. uitstappen en instappen kost 2 HTE
            // Arrange
            HotelRuimteFactory hotelRuimteFactory = new HotelRuimteFactory();
            Lift lift = (Lift)hotelRuimteFactory.MaakHotelRuimte("Lift");

            List<Liftschacht> liftSchachtenLijst = new List<Liftschacht>();
            liftSchachtenLijst.Add(new Liftschacht(2) { lift = lift });
            liftSchachtenLijst.Add(new Liftschacht(1) { lift = lift });
            liftSchachtenLijst.Add(new Liftschacht(0) { lift = lift });
            liftSchachtenLijst.Add(new Liftschacht(4) { lift = lift });

            lift.InitializeerLift(liftSchachtenLijst);

            Gast gast = new Gast();
            gast.HuidigeRuimte = liftSchachtenLijst[0];
            gast.Bestemming = liftSchachtenLijst[1];
            gast.BestemmingLijst = new List<HotelRuimte>();
            gast.BestemmingLijst.Add(liftSchachtenLijst[1]);
            gast.HuidigeRuimte.VoegPersoonToe(gast);

            // Act
            for (int i = 0; i <= 6; i++)
            {
                lift.Update(i);
            }

            // Assert
            Assert.IsTrue(gast.HuidigeRuimte.Verdieping == 1);
        }
    }
}
