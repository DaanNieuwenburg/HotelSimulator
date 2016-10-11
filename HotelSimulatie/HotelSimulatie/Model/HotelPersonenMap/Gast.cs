using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HotelSimulatie.Model
{
    public class Gast : Persoon
    {
        public bool Honger { get; set; }
        public int? Kamernummer { get; set; }
        public bool Wacht { get; set; }

        public Gast()
        {
            Honger = false;
            Wacht = false;
        }

        public void Inchecken(Lobby lobby, GameTime gameTime)
        {
            // Ga naar lobby en vraag om een kamer
            if (Kamernummer == null)
            {
                if (LoopNaarRuimte())
                {
                    // Voeg gast aan wachtrij toe
                    if (!lobby.Wachtrij.Contains(this))
                    {
                        lobby.Wachtrij.Enqueue(this);
                    }

                    // koppel de toegewezenkammer
                    Kamer toegewezenKamer = lobby.GastInChecken(this, gameTime);

                    // Als er een kamer is toegewezen
                    if (toegewezenKamer != null)
                    {
                        if (toegewezenKamer.AantalSterren == 0)
                        {
                            // Ga uitchecken, gevraagde kamer is niet beschikbaar
                            HuidigEvent.EventType = HotelEvents.HotelEventType.CHECK_OUT;
                        }
                        else
                        {
                            Bestemming = toegewezenKamer;
                            Kamernummer = toegewezenKamer.Kamernummer;
                        }
                    }
                }
            }

            // Bepaal route naar kamer
            else if (BestemmingLijst == null && Bestemming is Kamer)
            {
                // Zoek pad naar kamer
                DijkstraAlgoritme pathfindingAlgoritme = new DijkstraAlgoritme();
                BestemmingLijst = pathfindingAlgoritme.MaakAlgoritme(this, lobby, Bestemming);

                // Koppel eerste node aan bestemming
                Bestemming = BestemmingLijst.First();
                BestemmingLijst.Remove(BestemmingLijst.First());
            }

            // Loop naar kamer
            else if (BestemmingLijst != null)
            {
                if (LoopNaarRuimte() && BestemmingLijst.Count > 0)
                {
                    Bestemming = BestemmingLijst.First();
                    BestemmingLijst.Remove(BestemmingLijst.First());
                }
                else if (LoopNaarRuimte() && BestemmingLijst.Count == 0)
                {
                    // Haal het event weg, want de gast is bij zijn kamer aangekomen
                    BestemmingLijst = null;
                    HuidigEvent.EventType = HotelEvents.HotelEventType.NONE;
                    GaKamerIn(HuidigeRuimte);
                }
            }
        }

        public void Uitchecken(Lobby lobby)
        {
            HuidigeRuimte = HuidigeRuimte;
            // Bepaal route naar lobby
            if (BestemmingLijst == null && Bestemming is Lobby)
            {
                // Zoek pad naar lobby
                DijkstraAlgoritme pathfindingAlgoritme = new DijkstraAlgoritme();
                BestemmingLijst = pathfindingAlgoritme.MaakAlgoritme(this, HuidigeRuimte, Bestemming);

                // Koppel eerste node aan bestemming
                Bestemming = BestemmingLijst.First();
                BestemmingLijst.Remove(BestemmingLijst.First());
            }

            // Loop naar lobby
            else if (BestemmingLijst != null)
            {
                if (LoopNaarRuimte() && BestemmingLijst.Count > 0)
                {
                    Bestemming = BestemmingLijst.First();
                    BestemmingLijst.Remove(BestemmingLijst.First());
                }
                else if (LoopNaarRuimte() && BestemmingLijst.Count == 0)
                {
                    // Haal het event weg, want de gast is bij zijn kamer aangekomen
                    lobby.GastUitchecken(this);
                }
            }
        }

        public void GaNaarBioscoop(Bioscoop bioscoop)
        {
            if(Bestemming == null && HuidigeRuimte != bioscoop)
            {
                Bestemming = bioscoop;
            }
            // Bepaal route naar bioscoop
            if (BestemmingLijst == null && Bestemming is Bioscoop)
            {
                // Zoek pad naar bioscoop
                DijkstraAlgoritme pathfindingAlgoritme = new DijkstraAlgoritme();
                BestemmingLijst = pathfindingAlgoritme.MaakAlgoritme(this, HuidigeRuimte, Bestemming);

                // Koppel eerste node aan bestemming
                Bestemming = BestemmingLijst.First();
                BestemmingLijst.Remove(BestemmingLijst.First());
            }

            // Loop naar bioscoop
            else if (BestemmingLijst != null)
            {
                if (LoopNaarRuimte() && BestemmingLijst.Count > 0)
                {
                    Bestemming = BestemmingLijst.First();
                    BestemmingLijst.Remove(BestemmingLijst.First());
                }
                else if (LoopNaarRuimte() && BestemmingLijst.Count == 0)
                {
                    // Haal het event weg, want de gast is bij zijn kamer aangekomen
                    HuidigEvent.EventType = HotelEvents.HotelEventType.NONE;
                    Bestemming = null;
                }
            }
        }

        public void GaNaarFitness(Fitness fitness)
        {
            if (Bestemming == null && HuidigeRuimte != fitness)
            {
                Bestemming = fitness;
            }
            // Bepaal route naar fitness
            if (BestemmingLijst == null && Bestemming is Fitness)
            {
                // Zoek pad naar fitness
                DijkstraAlgoritme pathfindingAlgoritme = new DijkstraAlgoritme();
                BestemmingLijst = pathfindingAlgoritme.MaakAlgoritme(this, HuidigeRuimte, Bestemming);

                // Koppel eerste node aan bestemming
                Bestemming = BestemmingLijst.First();
                BestemmingLijst.Remove(BestemmingLijst.First());
            }

            // Loop naar fitness
            else if (BestemmingLijst != null)
            {
                if (LoopNaarRuimte() && BestemmingLijst.Count > 0)
                {
                    Bestemming = BestemmingLijst.First();
                    BestemmingLijst.Remove(BestemmingLijst.First());
                }
                else if (LoopNaarRuimte() && BestemmingLijst.Count == 0)
                {
                    // Haal het event weg, want de gast is bij zijn kamer aangekomen
                    HuidigEvent.EventType = HotelEvents.HotelEventType.NONE;
                    Bestemming = null;
                }
            }
        }
    }
}

