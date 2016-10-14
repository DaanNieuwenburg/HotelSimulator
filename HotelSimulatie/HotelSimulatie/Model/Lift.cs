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
        public bool BovensteLiftschachtBereikt { get; set; }
        public List<Persoon> GasteninLift { get; set; }
        public List<Liftschacht> LiftStoppenlijst { get; set; }
        public List<Liftschacht> Liftschachtlijst { get; set; }
        private float snelheid { get; set; }

        public Lift(int Aantalverdiepingen)
        {
            LiftStoppenlijst = new List<Liftschacht>();
            snelheid = 1.5f;
            GasteninLift = new List<Persoon>();
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
                if (GasteninLift.Count > 0)
                {
                    HuidigeVerdieping.LaatGastenUitLiftGaan();
                }

                HuidigeVerdieping.LaatGastenLiftInGaan();
            }
        }

        public void VoegLiftStopToe(Liftschacht liftstop)
        {
            if (!LiftStoppenlijst.Contains(liftstop))
            {
                LiftStoppenlijst.Add(liftstop);
            }
        }

        private bool VerplaatsLift()
        {
            bool aangekomenOpBestemming = false;
            if ((Int32)EventCoordinaten.Y < LiftBestemming.EventCoordinaten.Y)
            {
                Vector2 nieuweLiftPositie = new Vector2(EventCoordinaten.X, EventCoordinaten.Y + snelheid);
                EventCoordinaten = nieuweLiftPositie;
            }
            else if ((Int32)EventCoordinaten.Y > LiftBestemming.EventCoordinaten.Y)
            {
                Vector2 nieuweLiftPositie = new Vector2(EventCoordinaten.X, EventCoordinaten.Y - snelheid);
                EventCoordinaten = nieuweLiftPositie;
            }

            if (EventCoordinaten.Y == LiftBestemming.EventCoordinaten.Y)
            {
                //LiftBestemming.texturepath = @"Lift\Lift_Open";
                HuidigeVerdieping = LiftBestemming;
                
                // Controleer of lift uiterst boven of beneden staat
                if (LiftBestemming.Verdieping == BovensteVerdieping)
                {
                    BovensteLiftschachtBereikt = true;
                }
                else if (LiftBestemming.Verdieping == 0)
                {
                    BovensteLiftschachtBereikt = false;
                }

                // Maak een nieuwe lift bestemming aan
                if (BovensteLiftschachtBereikt == true)
                {
                    LiftBestemming = Liftschachtlijst[HuidigeVerdieping.Verdieping - 1];
                }
                else
                {
                    LiftBestemming = Liftschachtlijst[HuidigeVerdieping.Verdieping + 1];
                }
                
                aangekomenOpBestemming = true;

                GasteninLift.Sort((o1, o2) => o1.Bestemming.Verdieping.CompareTo(o2.Bestemming.Verdieping));
                // Haal de bestemming weg uit de gastenlijst
                for (int i = 0; i < GasteninLift.Count(); i++)
                {
                    Persoon persoon = GasteninLift[i];
                    Console.WriteLine(persoon.Bestemming.Verdieping);
                    if (HuidigeVerdieping == persoon.Bestemming)
                    {
                        persoon.HuidigeRuimte = HuidigeVerdieping;
                        persoon.Bestemming = persoon.BestemmingLijst.First();
                        persoon.BestemmingLijst.Remove(persoon.Bestemming);
                        persoon.inLift = false;
                        persoon.wachtOpLift = false;
                        GasteninLift.Remove(persoon);
                        persoon.Positie = HuidigeVerdieping.EventCoordinaten;
                    }
                }
            }
            return aangekomenOpBestemming;
        }
    }
}
