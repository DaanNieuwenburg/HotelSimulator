using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model.HotelRuimteMap
{
    public class Unknown : HotelRuimte
    {
        public Unknown()
        {
            Naam = "Unknown";
            texturepath = @"Kamers\Unknown";
        }
        public override void LoadContent(ContentManager contentManager) => Texture = contentManager.Load<Texture2D>(texturepath);
    }
}

