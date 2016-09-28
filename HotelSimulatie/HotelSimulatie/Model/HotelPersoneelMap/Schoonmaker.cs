using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Schoonmaker
    {
        public string Positie { get; set; }
        public bool InKamer { get; set; }
        public GeanimeerdeTexture SpriteAnimatie { get; set; }
        public HotelRuimte Bestemming { get; set; }
        public HotelRuimte HuidigeRuimte { get; set; }
        public void LoadContent(ContentManager contentManager)
        {
            SpriteAnimatie = new GeanimeerdeTexture(contentManager, "AnimatedSchoonmaker", 2);
        }

        public void UpdateFrame(GameTime spelTijd)
        {
            SpriteAnimatie.UpdateFrame(spelTijd);
        }

        public void LoopNaarRuimte(HotelRuimte bestemming, HotelRuimte huidigeRuimte)
        {
            Bestemming = bestemming;
            HuidigeRuimte = huidigeRuimte;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteAnimatie.ToonFrame(spriteBatch, HuidigeRuimte.CoordinatenInSpel);
        }
    }

}
