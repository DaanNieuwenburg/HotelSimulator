using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Kamer : HotelRuimte
    {
        [JsonProperty("Classification")]
        public int AantalSterren { get; set; }
        
        public bool Bezet { get; set; }

        public int hoogte { get; set; }
        public Kamer(int aantalSterren)
        {
            Bezet = false;
            Naam = "Kamer";
            AantalSterren = aantalSterren;
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
                /*if (Afmetingen.Y == 90)
                {*/
                    Texture = contentManager.Load<Texture2D>(@"Kamers\Kamer_4ster(1hoog)");
                /*}
                else
                {
                    Texture = contentManager.Load<Texture2D>(@"Kamers\Kamer_4ster(2hoog)");
                }*/
            }
            else if (AantalSterren == 5)
            {
                Texture = contentManager.Load<Texture2D>(@"Kamers\Kamer_5ster");
            }
        }
    }
}
