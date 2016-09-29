using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace HotelSimulatie.Model
{
    public class Bioscoop : HotelRuimte
    {
        public Bioscoop()
        {
            Naam = "Bioscoop";
        }
        public override void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(Naam);
        }
    }
}
