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
        public bool BestemmingBereikt { get; set; }
        public HotelRuimte HuidigeRuimte { get; set; }
        public Vector2 Positie { get; set; }
        public GeanimeerdeTexture SpriteAnimatie { get; set; }
        private float loopSnelheid { get; set; }
        public Persoon()
        {
            Random random = new Random();
            int a = random.Next(1, 9);
            loopSnelheid = (float)a / 10;
            BestemmingBereikt = false;
        }

        public void LoadContent(ContentManager contentManager)
        {
            SpriteAnimatie = new GeanimeerdeTexture(contentManager, "AnimatedRob", 3);
        }

        public bool LoopNaarRuimte(HotelRuimte bestemming)
        {
            Bestemming = bestemming;
            int x = Convert.ToInt32(Positie.X);
            if (x != bestemming.EventCoordinaten.X)
            {
                if (Positie.X > bestemming.EventCoordinaten.X)
                {
                    Positie = new Vector2(Positie.X - loopSnelheid, Positie.Y);
                }
                else
                {
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
