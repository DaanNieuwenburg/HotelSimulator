﻿using System;
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
        public string Naam { get; set; }
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
                            this.HuidigEvent = new HotelEvents.HotelEvent() { EventType = HotelEvents.HotelEventType.CHECK_OUT };
                            Console.WriteLine("CHECKOUT EVENT");
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
                    Console.WriteLine(HuidigeRuimte.Naam);
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
                BestemmingLijst = pathfindingAlgoritme.MaakAlgoritme(this, lobby, Bestemming);

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
    }
}
