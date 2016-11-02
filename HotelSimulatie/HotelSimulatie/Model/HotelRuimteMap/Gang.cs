using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HotelSimulatie.Model
{
    public class Gang : HotelRuimte
    {
        public Gang()
        {
            Naam = "Gang";
            texturepath = @"Kamers\Gang";
        }
        public override void LoadContent(ContentManager contentManager) => Texture = contentManager.Load<Texture2D>(texturepath);
    }
}
