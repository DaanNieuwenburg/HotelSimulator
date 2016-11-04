using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulatie.Model;
using System.Collections.Generic;

namespace TDD
{
    [TestClass]
    public class TrapTests
    {
        [TestMethod]
        public void Zou_persoon_moeten_laten_verplaatsen_bij_persoon_die_via_trap_omhoog_gaat()
        {
            // Arrange
            Trap trap = (Trap)new HotelRuimteFactory().MaakHotelRuimte("Trap");

            List<Trappenhuis> trappenHuizen = new List<Trappenhuis>();
            trappenHuizen.Add(new Trappenhuis(1) { trap = trap });
            trappenHuizen.Add(new Trappenhuis(2) { trap = trap });
            trappenHuizen.Add(new Trappenhuis(3) { trap = trap });

            Gast gast = new Gast();
            gast.HuidigeRuimte = trappenHuizen[0];
            gast.Bestemming = trappenHuizen[2];
            gast.BestemmingLijst = new List<HotelRuimte>();
            gast.BestemmingLijst.Add(trappenHuizen[2]);

            trap.VoegPersoonToe(gast);
            
            // Act
            for(int i = 0; i < 10; i++)
            {
                trap.Update(i);
            }
            int gastZijnVerdieping = gast.HuidigeRuimte.Verdieping;

            // Assert
            Assert.IsTrue(3 == gastZijnVerdieping);
        }

        [TestMethod]
        public void Zou_persoon_een_keer_moeten_laten_instappen_bij_twee_dezelfde_personen_met_andere_verdiepingen()
        {
            // Arrange
            Trap trap = (Trap)new HotelRuimteFactory().MaakHotelRuimte("Trap");

            List<Trappenhuis> trappenHuizen = new List<Trappenhuis>();
            trappenHuizen.Add(new Trappenhuis(1) { trap = trap });
            trappenHuizen.Add(new Trappenhuis(2) { trap = trap });
            trappenHuizen.Add(new Trappenhuis(3) { trap = trap });

            Gast gast = new Gast();
            gast.HuidigeRuimte = trappenHuizen[0];
            gast.Bestemming = trappenHuizen[2];
            gast.BestemmingLijst = new List<HotelRuimte>();
            gast.BestemmingLijst.Add(trappenHuizen[2]);

            trap.VoegPersoonToe(gast);

            gast.HuidigeRuimte = trappenHuizen[0];
            gast.Bestemming = trappenHuizen[1];

            trap.VoegPersoonToe(gast);

            // Act
            for (int i = 0; i < 10; i++)
            {
                trap.Update(i);
            }
            int gastZijnVerdieping = gast.HuidigeRuimte.Verdieping;

            // Assert
            Assert.IsTrue(3 == gastZijnVerdieping);
        }



        [TestMethod]
        public void Zou_persoon_niet_uit_trap_moeten_laten_gaan_na_3_hte_bij_3_verdiepingen_naar_boven()
        {
            // Arrange
            Trap trap = (Trap)new HotelRuimteFactory().MaakHotelRuimte("Trap");

            List<Trappenhuis> trappenHuizen = new List<Trappenhuis>();
            trappenHuizen.Add(new Trappenhuis(1) { trap = trap });
            trappenHuizen.Add(new Trappenhuis(2) { trap = trap });
            trappenHuizen.Add(new Trappenhuis(3) { trap = trap });

            Gast gast = new Gast();
            gast.HuidigeRuimte = trappenHuizen[0];
            gast.Bestemming = trappenHuizen[2];
            gast.BestemmingLijst = new List<HotelRuimte>();
            gast.BestemmingLijst.Add(trappenHuizen[2]);

            trap.VoegPersoonToe(gast);

            // Act
            for (int i = 0; i < 4; i++)
            {
                trap.Update(i);
            }
            int gastZijnVerdieping = gast.HuidigeRuimte.Verdieping;

            // Assert
            Assert.IsFalse(3 == gast.HuidigeRuimte.Verdieping);
        }

        [TestMethod]
        public void Zou_persoon_9hte_moeten_laten_wachten_bij_4_verdiepingen_naar_beneden()
        {
            // Arrange
            Trap trap = (Trap)new HotelRuimteFactory().MaakHotelRuimte("Trap");

            List<Trappenhuis> trappenHuizen = new List<Trappenhuis>();
            trappenHuizen.Add(new Trappenhuis(1) { trap = trap });
            trappenHuizen.Add(new Trappenhuis(2) { trap = trap });
            trappenHuizen.Add(new Trappenhuis(3) { trap = trap });
            trappenHuizen.Add(new Trappenhuis(4) { trap = trap });

            Gast gast = new Gast();
            gast.HuidigeRuimte = trappenHuizen[3];
            gast.Bestemming = trappenHuizen[0];
            gast.BestemmingLijst = new List<HotelRuimte>();
            gast.BestemmingLijst.Add(trappenHuizen[0]);

            trap.VoegPersoonToe(gast);

            // Act
            for (int i = 0; i <= 9; i++)
            {
                trap.Update(i);
            }

            // Assert
            Assert.IsTrue(1 == gast.HuidigeRuimte.Verdieping);
        }
    }
}
