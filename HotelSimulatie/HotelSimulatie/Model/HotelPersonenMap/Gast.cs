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
        public Kamer ToegewezenKamer { get; set; }
        public int AantalSterrenKamer { get; set; }

        public Gast()
        {
            Texturelijst = new List<string>();
            //Texturelijst.Add(@"Gasten\AnimatedRob");
            Texturelijst.Add(@"Gasten\AnimatedGast1");
            Texturelijst.Add(@"Gasten\AnimatedGast2");
            Texturelijst.Add(@"Gasten\AnimatedGast3");
            Texturelijst.Add(@"Gasten\AnimatedGast4");
        }

        public void Inchecken(Lobby lobby, GameTime gameTime)
        {
            // Ga naar lobby en vraag om een kamer
            if (ToegewezenKamer == null)
            {
                if (LoopNaarRuimte())
                {
                    // Voeg gast aan wachtrij toe
                    if (!lobby.Wachtrij.Contains(this))
                    {
                        lobby.Wachtrij.Enqueue(this);
                    }

                    // koppel de toegewezenkammer
                    Kamer gevondenKamer = lobby.GastInChecken(this, gameTime);

                    // Als er een kamer is toegewezen
                    if (gevondenKamer != null)
                    {
                        if (gevondenKamer.AantalSterren == 0)
                        {
                            // Ga uitchecken, gevraagde kamer is niet beschikbaar
                            HuidigEvent.NEvent = HotelEventAdapter.NEventType.CHECK_OUT;
                        }
                        else
                        {
                            Bestemming = gevondenKamer;
                            ToegewezenKamer = gevondenKamer;
                            HuidigEvent.NEvent = HotelEventAdapter.NEventType.GOTO_ROOM;
                        }
                    }
                }
            }
        }

        public void GaNaarKamer<T>(ref T ruimte)
        {

            if (Bestemming == null && HuidigeRuimte != ruimte as HotelRuimte)
            {
                Bestemming = ruimte as HotelRuimte;
            }

            if (BestemmingLijst == null && Bestemming is T)
            {
                // Zoek kortste pad naar bestemming
                DijkstraAlgoritme pathfindingAlgoritme = new DijkstraAlgoritme();
                if (Bestemming is Eetzaal)
                {
                    pathfindingAlgoritme.zoekDichtbijzijnde = true;
                }
                BestemmingLijst = pathfindingAlgoritme.MaakAlgoritme(this, HuidigeRuimte, ruimte as HotelRuimte);

                // Koppel eerste node aan bestemming
                HuidigEvent = HuidigEvent;
                Bestemming = BestemmingLijst.First();
                BestemmingLijst.Remove(BestemmingLijst.First());
            }

            // Loop via pathfinding naar bestemming
            else if (BestemmingLijst != null)
            {
                if (LoopNaarRuimte() && BestemmingLijst.Count > 0)
                {
                    if (HuidigeRuimte is Liftschacht)
                    {
                        // Ga verder met de lift
                        Liftschacht liftschacht = (Liftschacht)HuidigeRuimte;
                        liftschacht.VraagOmLift(this);
                        Bestemming = HuidigeRuimte;
                    }
                    else if (HuidigeRuimte.GetType() != typeof(Liftschacht))
                    {
                        HuidigeRuimte = Bestemming;
                        Bestemming = BestemmingLijst.First();
                        BestemmingLijst.Remove(BestemmingLijst.First());
                    }
                }
                else if (LoopNaarRuimte() && BestemmingLijst.Count == 0)
                {
                    Bestemming = null;
                    BestemmingLijst = null;
                    HuidigEvent.NEvent = HotelEventAdapter.NEventType.NONE;
                    if (HuidigeRuimte is Eetzaal || HuidigeRuimte is Bioscoop || HuidigeRuimte is Fitness)
                    {
                        HuidigeRuimte.voegPersoonToe(this);
                    }
                }
            }
        }
    }
}


