using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Kamer : HotelRuimte
    {
        public int Kamernummer { get; set; }
        public int AantalSterren { get; set; }
        public int hoogte { get; set; }
        public Kamer()
        {
            AantalSterren = 1;
        }
        public override void LoadContent(ContentManager contentManager)
        {
            if (AantalSterren == 1)
            {
                Texture = contentManager.Load<Texture2D>(@"Kamers\Kamer_1ster");
            }
            else if (AantalSterren == 2)
            {
                Texture = contentManager.Load<Texture2D>(@"Kamers\Kamer_2ster");
            }
            else if (AantalSterren == 3)
            {
                Texture = contentManager.Load<Texture2D>(@"Kamers\Kamer_3ster");
            }
            else if (AantalSterren == 4)
            {
                if (hoogte == 1)
                {

                Texture = contentManager.Load<Texture2D>(@"Kamers\Kamer_4ster(1hoog)");
                }
                else
                {
                    Texture = contentManager.Load<Texture2D>(@"Kamers\Kamer_4ster(2hoog)");
                }
            }
            else if (AantalSterren == 5)
            {
                Texture = contentManager.Load<Texture2D>("5SterHotelKamer");
            }
        }
    }
}
