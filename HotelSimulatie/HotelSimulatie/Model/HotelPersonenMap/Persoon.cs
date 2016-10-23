﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEvents;

namespace HotelSimulatie.Model
{
    public abstract class Persoon
    {
        public HotelRuimte Bestemming { get; set; }
        public List<HotelRuimte> BestemmingLijst { get; set; }
        public HotelEventAdapter HuidigEvent { get; set; }
        public HotelRuimte HuidigeRuimte { get; set; }
        public Liftschacht bestemmingslift { get; set; }
        public Vector2 Positie { get; set; }
        public GeanimeerdeTexture SpriteAnimatie { get; set; }
        private float loopSnelheid { get; set; }
        public List<string> Texturelijst { get; set; }
        private ContentManager tempmanager { get; set; }
        public string Naam { get; set; }
        private int textureIndex { get; set; }
        private bool LooptNaarLinks { get; set; }
        public bool inLift { get; set; }
        public Persoon()
        {
            inLift = false;
            LooptNaarLinks = false;
            /*Random random = new Random();
            int a = random.Next(1, 9);
            loopSnelheid = (float)a / 10;*/
            loopSnelheid = (float)0.7;  // dit mag nooit minder dan 0,6 zijn

            Texturelijst = new List<string>();
            //Texturelijst.Add(@"Gasten\AnimatedRob");
            Texturelijst.Add(@"Gasten\AnimatedGast1");
            Texturelijst.Add(@"Gasten\AnimatedGast2");
            Texturelijst.Add(@"Gasten\AnimatedGast3");
            Texturelijst.Add(@"Gasten\AnimatedGast4");

        }

        public void LoadContent(ContentManager contentManager)
        {
            tempmanager = contentManager;
            Random randomgast = new Random();
            textureIndex = randomgast.Next(0, Texturelijst.Count());
            SpriteAnimatie = new GeanimeerdeTexture(contentManager, Texturelijst[textureIndex], 3);
        }

        public bool LoopNaarRuimte()
        {
            bool bestemmingBereikt = false;

            if (Bestemming != null && bestemmingBereikt == false)
            {
                HuidigeRuimte = HuidigeRuimte;
                Bestemming = Bestemming;

                // Als persoon aan komt op bestemming
                if ((Int32)Positie.X == Bestemming.EventCoordinaten.X)
                {
                    bestemmingBereikt = true;
                    HuidigeRuimte = Bestemming;
                }

                // Als persoon naar links moet
                else if (Positie.X > Bestemming.EventCoordinaten.X)
                {
                    BeweegNaarLinks();
                }

                // Als persoon naar rechts moet
                else if (Positie.X < Bestemming.EventCoordinaten.X)
                {
                    BeweegNaarRechts();
                }
            }

            return bestemmingBereikt;
        }

        private bool BeweegNaarLinks()
        {
            SpriteAnimatie = new GeanimeerdeTexture(tempmanager, Texturelijst[textureIndex], 3);
            Positie = new Vector2(Positie.X - loopSnelheid, Positie.Y);
            return false;
        }

        private bool BeweegNaarRechts()
        {
            SpriteAnimatie = new GeanimeerdeTexture(tempmanager, Texturelijst[textureIndex], 3);
            Positie = new Vector2(Positie.X + loopSnelheid, Positie.Y);
            return false;
        }

        public void UpdateFrame(GameTime spelTijd)
        {
            SpriteAnimatie.UpdateFrame(spelTijd);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            SpriteAnimatie.ToonFrame(spritebatch, Positie);
        }
    }
}