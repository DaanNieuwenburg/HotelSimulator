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

        public Lift()
        {
            LiftStoppenlijst = new Dictionary<Persoon, List<object>>();
            PersonenInLift = new List<Persoon>();
        }

        public void InitializeerLift(List<Liftschacht> _liftschachtenLijst)
        {
            LiftSchachtenlijst = _liftschachtenLijst;
            BovensteVerdieping = LiftSchachtenlijst.Count;

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
            // Kijkt of gasten zijn doodgegaan
            for (int i = 0; i < LiftStoppenlijst.Count; i++)
            {
                if (LiftStoppenlijst.Keys.ElementAt(i) is Gast)
                {
                    Gast temp = (Gast)(LiftStoppenlijst.Keys.ElementAt(i));
                    if (temp.isDood == true)
                    {
                        LiftStoppenlijst.Remove(temp);
                    }
                }
            }
            verlopenTijd = verlopenTijdInSeconden;
            // Als de lift aangekomen is op bestemming
            if (LiftBestemming != null && verlopenTijd >= aankomstTijd + 2)
            {
                HuidigeVerdieping = LiftBestemming;
                LiftBestemming = null;
                personenUitstappen();
                personenInstappen();
            }
            // Als lift geen bestemming heeft zoek er een
            else if (LiftBestemming == null)
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

                if (persoon.Bestemming is Liftschacht)
                {
                    if (persoon is Gast)
                    {
                        Gast gast = (Gast)persoon;
                        if (gast.isDood == false)
                        {
                            // Zorg ervoor dat persoon niet meer grafisch getoond wordt
                            persoon.Wacht = false;
                            persoon.inLiftOfTrap = true;

                            // Voeg de liftschacht waar de persoon weer uit wil toe als liftstop
                            VoegLiftStopToe(persoon, persoon.Bestemming as Liftschacht);
                            if (!PersonenInLift.Contains(persoon))
                            {
                                PersonenInLift.Add(persoon);
                            }
                        }
                    }
                    else
                    {
                        // Zorg ervoor dat persoon niet meer grafisch getoond wordt
                        persoon.Wacht = false;
                        persoon.inLiftOfTrap = true;

                        // Voeg de liftschacht waar de persoon weer uit wil toe als liftstop
                        VoegLiftStopToe(persoon, persoon.Bestemming as Liftschacht);
                        if (!PersonenInLift.Contains(persoon))
                        {
                            PersonenInLift.Add(persoon);
                        }
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
                // Als een persoon op een lift wacht, is de bestemming van de persoon de uitstap liftschacht
                // Zo niet, dan is de bestemming de volgende ruimte
                if (persoon.Key.Wacht != true)
                {
                    persoon.Key.HuidigeRuimte = persoon.Key.Bestemming;
                    persoon.Key.Bestemming = persoon.Key.BestemmingLijst.First();
                    persoon.Key.BestemmingLijst.Remove(persoon.Key.Bestemming);
                    persoon.Key.Positie = HuidigeVerdieping.EventCoordinaten;
                }
                persoon.Key.Wacht = false;
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
                stopGegevens.Add(verlopenTijd);
                stopGegevens.Add(wachtTijd);
                LiftStoppenlijst.Add(persoon, stopGegevens);
            }
        }

        private void bepaalLiftBestemming()
        {
            // Hier maken we gebruik van het scheduling algoritme

            // Bepaal de totale tijd die de lift nog heen en weer moet gaan
            if (LiftStoppenlijst.Count > 0)
            {
                int totaleSomVanDuration = LiftStoppenlijst.Sum(p => (int)p.Value[1]);

                Dictionary<Persoon, List<object>> starvationGevallen = new Dictionary<Persoon, List<object>>();
                // Controleer op starvation
                foreach (KeyValuePair<Persoon, List<object>> persoon in LiftStoppenlijst)
                {
                    // Bepaal de verstreken tijd
                    int wachtTijd = (verlopenTijd - (int)persoon.Value[1]) + totaleSomVanDuration - (int)persoon.Value[2];
                    if (wachtTijd > HotelTijdsEenheid.doodgaanHTE)
                    {
                        starvationGevallen.Add(persoon.Key, persoon.Value);
                    }
                }

                if(starvationGevallen.Count > 0)
                {
                    // Sorteert de dictionary van kortste wachttijd naar hoogtste
                    Dictionary<Persoon, List<object>> starvationGevallenGesorteerd = (from persoon in starvationGevallen
                                                                        orderby (int)persoon.Value[2]
                                                                        select persoon).ToDictionary(o1 => o1.Key, o2 => o2.Value);
                    LiftBestemming = starvationGevallenGesorteerd.First().Value[0] as Liftschacht;
                }
                else
                {
                    // Sorteert de dictionary van kortste wachttijd naar hoogtste
                    Dictionary<Persoon, List<object>> liftStoppenGesorteerd = (from persoon in LiftStoppenlijst
                                                                               orderby (int)persoon.Value[2]
                                                                               select persoon).ToDictionary(o1 => o1.Key, o2 => o2.Value);
                    LiftBestemming = liftStoppenGesorteerd.First().Value[0] as Liftschacht;
                }

                LiftBestemming = (Liftschacht)LiftStoppenlijst.First().Value[0];

                // Bepaal aankomsttijd
                aankomstTijd = verlopenTijd + Math.Abs(HuidigeVerdieping.Verdieping - LiftBestemming.Verdieping);
            }
        }
    }
}