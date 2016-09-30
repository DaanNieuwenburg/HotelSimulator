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
        public int Gastnummer { get; set; }
        public bool Honger { get; set; }
        public int? Kamernummer { get; set; }
        public bool Wacht { get; set; }
       
        public List<Texture2D> Texturelijst { get; set; }

        public Gast()
        {
            Honger = false;
            Wacht = false;
        }
        public void LoadContent(ContentManager contentManager)
        {
            SpriteAnimatie = new GeanimeerdeTexture(contentManager, "AnimatedRob", 3);
        }

        public void UpdateFrame(GameTime spelTijd)
        {
            SpriteAnimatie.UpdateFrame(spelTijd);
        }
    }
}
