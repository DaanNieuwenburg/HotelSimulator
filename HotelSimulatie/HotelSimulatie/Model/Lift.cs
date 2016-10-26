using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Lift : HotelRuimte
    {
        public Liftschacht HuidigeVerdieping { get; set; }
        public Liftschacht LiftBestemming { get; set; }
        public int BovensteVerdieping { get; set; }
        public bool LiftOmhoog { get; set; }
        public List<Persoon> PersonenInLift { get; set; }
        public List<Liftschacht> LiftStoppenlijst { get; set; }
        public List<Liftschacht> Liftschachtlijst { get; set; }
        private float snelheid { get; set; }

        public Lift(int Aantalverdiepingen)
        {
            LiftOmhoog = true;
            LiftStoppenlijst = new List<Liftschacht>();
            snelheid = 1.5f;
            PersonenInLift = new List<Persoon>();
            BovensteVerdieping = Aantalverdiepingen;
        }

        public void InitializeerLift()
        {
            HuidigeVerdieping = Liftschachtlijst.Find(o => o.Verdieping == 0);
            EventCoordinaten = HuidigeVerdieping.EventCoordinaten;
            LiftBestemming = Liftschachtlijst.First();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            // Deze lift kent geen texture
        }

        public void UpdateLift()
        {
            if (VerplaatsLift())
            {
                bepaalLiftBestemming();
                HuidigeVerdieping.LaatGastenLiftInGaan();
            }
        }

        public void VoegLiftStopToe(Liftschacht liftstop)
        {
            if (LiftStoppenlijst.Any(o => o.Verdieping == liftstop.Verdieping) == false)
            {
                LiftStoppenlijst.Add(liftstop);
            }
        }

        private bool VerplaatsLift()
        {
            bool aangekomenOpBestemming = false;

            // Omhoog
            if ((Int32)EventCoordinaten.Y < LiftBestemming.EventCoordinaten.Y)
            {
                Vector2 nieuweLiftPositie = new Vector2(EventCoordinaten.X, EventCoordinaten.Y + snelheid);
                EventCoordinaten = nieuweLiftPositie;
            }

            // Omlaag
            else if ((Int32)EventCoordinaten.Y > LiftBestemming.EventCoordinaten.Y)
            {
                Vector2 nieuweLiftPositie = new Vector2(EventCoordinaten.X, EventCoordinaten.Y - snelheid);
                EventCoordinaten = nieuweLiftPositie;
            }

            // Bij aankomst
            if (EventCoordinaten.Y == LiftBestemming.EventCoordinaten.Y)
            {
                aangekomenOpBestemming = true;

                // Verwijder liftstop uit liftstoppenlijst
                HuidigeVerdieping = LiftBestemming;
                LiftStoppenlijst.Remove(HuidigeVerdieping);
                HuidigeVerdieping = LiftBestemming;

                PersonenInLift.Sort((o1, o2) => o1.Bestemming.Verdieping.CompareTo(o2.Bestemming.Verdieping));

                // Laat de gasten uitstappen
                var personenDieUitstappen = (from persoon in PersonenInLift
                                             where persoon.Bestemming == HuidigeVerdieping
                                             select persoon);
                List<Persoon> personenDieUitstappenLijst = personenDieUitstappen.ToList();
                HuidigeVerdieping.LaatPersonenUitLiftGaan(personenDieUitstappenLijst);
            }
            return aangekomenOpBestemming;
        }

        private void bepaalLiftBestemming()
        {
            int wachtendeBovenDeLift = LiftStoppenlijst.Count(o => o.Verdieping > HuidigeVerdieping.Verdieping);
            int wachtendeOnderDeLift = LiftStoppenlijst.Count(o => o.Verdieping < HuidigeVerdieping.Verdieping);

            // Ga omhoog, bij wachtende mensen boven de huidige lift
            if (wachtendeBovenDeLift > 0)
            {
                // Sorteer de lijst van laag naar hoog, want de lift gaat omhoog
                LiftStoppenlijst.Sort((o1, o2) => o1.Verdieping.CompareTo(o2.Verdieping));

                // Pak de eerste bestemming boven de huidige liftverdieping
                LiftBestemming = LiftStoppenlijst.First(o => o.Verdieping > HuidigeVerdieping.Verdieping);
            }

            // Ga omlaag bij wachtende mensen onder de huidige lift
            else if (wachtendeOnderDeLift > 0)
            {
                // Sorteer de lijst van hoog naar laag want de lift gaat omlaag
                LiftStoppenlijst.Sort((o1, o2) => o2.Verdieping.CompareTo(o1.Verdieping));

                // Pak de eerste bestemming onder de huidige liftverdieping
                LiftBestemming = LiftStoppenlijst.First(o => o.Verdieping < HuidigeVerdieping.Verdieping);
            }

            // Als er niemand wacht op de lift, blijf op huidige verdieping

        }
    }
}