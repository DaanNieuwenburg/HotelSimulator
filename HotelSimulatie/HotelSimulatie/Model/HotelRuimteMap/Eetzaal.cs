using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Eetzaal : HotelRuimte
    {
        public Eetzaal()
        {
            Naam = "Eetzaal";
            texturepath = @"Kamers\Eetzaal";
        }
        public override void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(Naam);
        }
    }
}
