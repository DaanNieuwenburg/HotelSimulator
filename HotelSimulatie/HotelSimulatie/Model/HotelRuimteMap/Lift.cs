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
        private int verlopenTijd { get; set; }
        private int aankomstTijd { get; set; }
        public List<Persoon> PersonenInLift { get; set; }
        public Dictionary<Persoon, List<object>> LiftStoppenlijst { get; set; }
        private List<Liftschacht> LiftSchachtenlijst { get; set; }

        public Lift(int Aantalverdiepingen)
        {
            LiftStoppenlijst = new Dictionary<Persoon, List<object>>();
            PersonenInLift = new List<Persoon>();
            BovensteVerdieping = Aantalverdiepingen;
        }

        public void InitializeerLift(List<Liftschacht> _liftschachtenLijst)
        {
            LiftSchachtenlijst = _liftschachtenLijst;

            // Sorteer de liftschachtenLijst van de laagste verdieping naar de hoogste
            LiftSchachtenlijst.Sort((o1, o2) => o1.Verdieping.CompareTo(o2.Verdieping));
            HuidigeVerdieping = LiftSchachtenlijst.First();

            // Koppelt de eventcoordinaten van de lift, zodat gasten op de juiste manier uitstappen
            EventCoordinaten = HuidigeVerdieping.EventCoordinaten;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            // Deze lift kent geen texture
        }

        public override void Update(int verlopenTijdInSeconden)
        {
            verlopenTijd = verlopenTijdInSeconden;
            // Als de lift een bestemming heeft en aangekomen is op bestemming
            if (LiftBestemming != null && verlopenTijd >= aankomstTijd)
            {
                HuidigeVerdieping = LiftBestemming;
                LiftBestemming = null;
                personenUitstappen();
                personenInstappen();
            }
            else
            {
                bepaalLiftBestemming();
            }
        }

        private void personenInstappen()
        {
            foreach (Persoon persoon in HuidigeVerdieping.Wachtrij)
            {
                // De persoon is opgepikt, verwijder liftstop in huidige lijst
                LiftStoppenlijst.Remove(persoon);
                
                // Reset het doodgaan
                if (persoon is Gast)
                {
                    persoon.Wachtteller.Stop();
                    persoon.Wachtteller.Reset();
                }

                if(persoon.Bestemming is Liftschacht)
                {
                    if(persoon is Gast)
                    {
                        Gast gast = (Gast)persoon;
                        if(gast.isDood == false)
                        {
                            // Zorg ervoor dat persoon niet meer grafisch getoond wordt
                            persoon.Wacht = false;
                            persoon.inLiftOfTrap = true;
                            
                            // Voeg de liftschacht waar de persoon weer uit wil toe als liftstop
                            VoegLiftStopToe(persoon, persoon.Bestemming as Liftschacht);
                            if (!PersonenInLift.Contains(persoon))
                                PersonenInLift.Add(persoon);
                        }
                    }
                    else
                    {
                        // Zorg ervoor dat persoon niet meer grafisch getoond wordt
                        persoon.Wacht = false;
                        persoon.inLiftOfTrap = true;

                        // Voeg de liftschacht waar de persoon weer uit wil toe als liftstop
                        VoegLiftStopToe(persoon, persoon.Bestemming as Liftschacht);
                        if(!PersonenInLift.Contains(persoon))
                            PersonenInLift.Add(persoon);
                    } 
                }
            }
        }


        private void personenUitstappen()
        {
            // Bepaal welke personen willen uitstappen
            Dictionary<Persoon, List<object>> personenDieLiftUitGaan = (from persoon in LiftStoppenlijst
                                                                        where HuidigeVerdieping == (Liftschacht)persoon.Value[0] && persoon.Key.Wacht == false
                                                                        select persoon).ToDictionary(o1 => o1.Key, o2 => o2.Value);
            
            foreach (KeyValuePair<Persoon, List<object>> persoon in personenDieLiftUitGaan)
            {
                persoon.Key.HuidigeRuimte = (HotelRuimte)persoon.Value[0];
                persoon.Key.Bestemming = persoon.Key.BestemmingLijst.First();
                persoon.Key.BestemmingLijst.Remove(persoon.Value[0] as Liftschacht);
                persoon.Key.Positie = HuidigeVerdieping.EventCoordinaten;
                persoon.Key.inLiftOfTrap = false;
                LiftStoppenlijst.Remove(persoon.Key);
                PersonenInLift.Remove(persoon.Key);
            }
        }

        public void VoegLiftStopToe(Persoon persoon, Liftschacht liftstop)
        {
            if (!LiftStoppenlijst.ContainsKey(persoon))
            {
                // Bepaal hoe lang persoon op de lift moet wachten
                int wachtTijd = verlopenTijd + Math.Abs(liftstop.Verdieping - HuidigeVerdieping.Verdieping);

                List<object> stopGegevens = new List<object>();
                stopGegevens.Add(liftstop);
                stopGegevens.Add(wachtTijd);
                LiftStoppenlijst.Add(persoon, stopGegevens);
            }
        }

        private void bepaalLiftBestemming()
        {
            if (LiftStoppenlijst.Count > 0)
            {
                LiftBestemming = (Liftschacht)LiftStoppenlijst.First().Value[0];

                // Bepaal aankomsttijd
                aankomstTijd = verlopenTijd + Math.Abs(HuidigeVerdieping.Verdieping - LiftBestemming.Verdieping);
            }
        }
    }
}