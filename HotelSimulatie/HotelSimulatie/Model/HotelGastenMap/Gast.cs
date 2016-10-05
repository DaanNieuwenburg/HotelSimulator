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
        public int Gastnummer { get; set; }
        public bool Honger { get; set; }
        public int? Kamernummer { get; set; }
        public bool Wacht { get; set; }
        private Algoritme algoritme { get; set; }

        public Gast()
        {
            Honger = false;
            Wacht = false;
            algoritme = new Algoritme();
        }

        public void Inchecken(Lobby lobby, GameTime gameTime)
        {
            Bestemming = lobby;
            if (LoopNaarRuimte())
            {
                lobby.Naam = "lobby_Death";
                HotelRuimte kamer = lobby.GastInChecken(this, gameTime);

                if (kamer != null)
                {
                    Bestemminglijst = algoritme.MaakAlgoritme(HuidigeRuimte, kamer);
                    lobby.Naam = "lobby_Normaal";

                    Bestemming = kamer;
                    bool gearriveerd = LoopNaarRuimte();
                    if (gearriveerd == true)
                    {
                        GaKamerIn(Bestemming);
                    }
                }
            }
        }
    }
}
