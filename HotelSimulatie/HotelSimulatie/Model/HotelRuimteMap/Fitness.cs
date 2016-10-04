using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Fitness : HotelRuimte
    {
        public Fitness()
        {
            Naam = "Fitness";
            texturepath = @"Kamers\Fitness";
        }
        public override void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(Naam);
        }
    }
}
