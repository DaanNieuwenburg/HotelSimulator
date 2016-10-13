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
            snelheid = 2.5f;
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
                if(GasteninLift.Count > 0)
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
                bepaalLiftBestemming();
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
                HuidigeVerdieping.texturepath = @"Lift\Lift_Open";
                aangekomenOpBestemming = true;

                // Haal de bestemming weg uit de gastenlijst
                foreach (Gast gast in GasteninLift)
                {
                    if (gast.BestemmingLijst != null)
                    {
                        if (HuidigeVerdieping == gast.Bestemming)
                        {
                            gast.bestemmingslift = gast.BestemmingLijst.OfType<Liftschacht>().Last();
                            gast.HuidigeRuimte = gast.Bestemming;
                            gast.Bestemming = gast.BestemmingLijst.First();
                            gast.BestemmingLijst.Remove(gast.BestemmingLijst.First());
                            gast.Positie = HuidigeVerdieping.EventCoordinaten;
                            if(gast.Bestemming.GetType() == gast.HuidigeRuimte.GetType())
                            {
                                gast.InLift = false;
                            }
                        }
                    }
                }
            }
            return aangekomenOpBestemming;
        }

        private void bepaalLiftBestemming()
        {
            if(LiftStoppenlijst.Count > 0)
            {
                LiftBestemming = LiftStoppenlijst.First();
                LiftStoppenlijst.Remove(LiftStoppenlijst.First());
            }
            else
            {
                LiftBestemming = HuidigeVerdieping;
            }
        }
    }
}
