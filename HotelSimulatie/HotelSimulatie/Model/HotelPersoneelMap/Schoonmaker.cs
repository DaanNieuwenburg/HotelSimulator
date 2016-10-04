using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Schoonmaker : Persoon
    {
        public HotelRuimte SchoonmaakPositie { get; set; }
        public bool InKamer { get; set; }
        public string Texturenaam { get; set; }
        public void LoadContent(ContentManager contentManager)
        {
            if (Texturenaam == "AnimatedSchoonmaker")
                SpriteAnimatie = new GeanimeerdeTexture(contentManager, Texturenaam, 2);
            else
                SpriteAnimatie = new GeanimeerdeTexture(contentManager, Texturenaam, 4);
        }

        public void UpdateFrame(GameTime spelTijd)
        {
            SpriteAnimatie.UpdateFrame(spelTijd);
        }

        
    }

}
