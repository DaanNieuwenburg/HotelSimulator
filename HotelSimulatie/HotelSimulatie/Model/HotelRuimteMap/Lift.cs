using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Lift : HotelRuimte
    {
        public int Bestemming { get; set; }
        public int Positie { get; set; }
        public int AantalPersonen { get; set; }
        public int Verdieping { get; set; }
        public Lift()
        {
            Naam = "Lift";
        }
        public override void LoadContent(ContentManager contentManager)
        {
            string texture;
            if (Verdieping == 0)
                texture = @"Lift\Lift_Beneden";
            else
                texture = @"Lift\Gesloten";

            Texture = contentManager.Load<Texture2D>(texture);
        }
    }
}
