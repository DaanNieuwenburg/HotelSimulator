using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public abstract class Persoon 
    {
        public HotelRuimte Bestemming { get; set; }
        public List<HotelRuimte> Bestemminglijst { get; set; }
        public bool BestemmingBereikt { get; set; }
        public HotelRuimte HuidigeRuimte { get; set; }
        public Vector2 Positie { get; set; }
        public GeanimeerdeTexture SpriteAnimatie { get; set; }
        private float loopSnelheid { get; set; }
        public List<string> Texturelijst { get; set; }
        private ContentManager tempmanager { get; set; }
        private int textureindex { get; set; }
        private bool LooptnaarLinks { get; set; }
        public Persoon()
        {
            LooptnaarLinks = false;
            Random random = new Random();
            int a = random.Next(1, 9);
            loopSnelheid = (float)a / 10;
            BestemmingBereikt = false;

            Texturelijst = new List<string>();
            //Texturelijst.Add(@"Gasten\AnimatedRob");
            Texturelijst.Add(@"Gasten\AnimatedGast1");
            Texturelijst.Add(@"Gasten\AnimatedGast2");
            Texturelijst.Add(@"Gasten\AnimatedGast3");
        }

        public void LoadContent(ContentManager contentManager)
        {
            tempmanager = contentManager;
            Random randomgast = new Random();
            textureindex = randomgast.Next(0, Texturelijst.Count());
            //SpriteAnimatie = new GeanimeerdeTexture(contentManager, @"Gasten\AnimatedGast3", 3);
            SpriteAnimatie = new GeanimeerdeTexture(contentManager, Texturelijst[textureindex], 3);
        }

        public bool LoopNaarRuimte()
        {
            if(Bestemminglijst != null && Bestemminglijst.Count > 0)
            {
                Bestemming = Bestemminglijst.First();
                Bestemminglijst.Remove(Bestemming);
            }
            
            int x = Convert.ToInt32(Positie.X);
            if (x != Bestemming.EventCoordinaten.X)
            {
                if (Positie.X > Bestemming.EventCoordinaten.X)
                {
                    string texture = Texturelijst[textureindex] += "_Links";
                    if (LooptnaarLinks == false)
                    {
                        SpriteAnimatie = new GeanimeerdeTexture(tempmanager, texture, 3);
                    }
                    LooptnaarLinks = true;    
                    Positie = new Vector2(Positie.X - loopSnelheid, Positie.Y);
                }
                else
                {
                    LooptnaarLinks = false;
                    SpriteAnimatie = new GeanimeerdeTexture(tempmanager, Texturelijst[textureindex], 3);
                    Positie = new Vector2(Positie.X + loopSnelheid, Positie.Y);
                }
                return false;
            }
            else
            {
                return true;
            }
        }


        public void UpdateFrame(GameTime spelTijd)
        {
            SpriteAnimatie.UpdateFrame(spelTijd);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteAnimatie.ToonFrame(spriteBatch, Positie);
        }
    }
}
