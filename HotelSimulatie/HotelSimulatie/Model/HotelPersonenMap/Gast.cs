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
        private HotelLayout hotellayout { get; set; }
        public Kamer ToegewezenKamer { get; set; }
        public int AantalSterrenKamer { get; set; }
        public bool isDood { get; set; }
        public bool heeftHonger { get; set; }
        public Gast()
        {
            isDood = false;
            Wachtteller = new System.Diagnostics.Stopwatch();
            Texturelijst = new List<string>();
            Texturelijst.Add(@"Gasten\AnimatedGast1");
            Texturelijst.Add(@"Gasten\AnimatedGast2");
            Texturelijst.Add(@"Gasten\AnimatedGast3");
            Texturelijst.Add(@"Gasten\AnimatedGast4");
            Texturelijst.Add(@"Gasten\AnimatedGast5");
            Texturelijst.Add(@"Gasten\AnimatedGast6");
            Texturelijst.Add(@"Gasten\AnimatedGast7");
            Texturelijst.Add(@"Gasten\AnimatedGast8");
        }

        public override void LoadContent(ContentManager contentManager)
        {
            tempmanager = contentManager;
            Random randomgast = new Random();
            textureIndex = randomgast.Next(0, Texturelijst.Count());
            SpriteAnimatie = new GeanimeerdeTexture(contentManager, Texturelijst[textureIndex], 3);
        }
        public void Inchecken(HotelLayout hotellayout, GameTime gameTime)
        {
            // Ga naar lobby en vraag om een kamer
            if (ToegewezenKamer == null)
            {
                if (Beweeg())
                {
                    // Voeg gast aan wachtrij toe
                    if (!hotellayout.lobby.Wachtrij.Contains(this))
                    {
                        hotellayout.lobby.Wachtrij.Enqueue(this);
                    }

                    // koppel de toegewezenkammer
                    Kamer gevondenKamer = hotellayout.lobby.GastInChecken(this, gameTime);

                    // Als er een kamer is toegewezen
                    if (gevondenKamer != null)
                    {
                        if (gevondenKamer.AantalSterren == 0)
                        {
                            // Ga uitchecken, gevraagde kamer is niet beschikbaar
                            HuidigEvent.Event = HotelEventAdapter.EventType.CHECK_OUT;
                        }
                        else
                        {
                            Bestemming = gevondenKamer;
                            ToegewezenKamer = gevondenKamer;
                            HuidigEvent.Event = HotelEventAdapter.EventType.GOTO_ROOM;
                        }
                    }
                }
            }
        }
        public void Rondspoken()
        { /*
            Trappenhuis trap = hotellayout.Trappenhuislijst[HuidigeRuimte.Verdieping];
            Liftschacht lift = hotellayout.liftSchachtenLijst[HuidigeRuimte.Verdieping];
            this.GaNaarRuimte(ref trap);
            if(this.HuidigeRuimte == trap)
            {
                this.GaNaarRuimte(ref lift);
            }
            else if(HuidigeRuimte == lift)
            {
                this.GaNaarRuimte(ref trap);
            }*/
        }
    }
}


