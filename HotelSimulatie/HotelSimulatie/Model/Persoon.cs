using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Persoon
    {
        public HotelRuimte Bestemming { get; set; }
        public HotelRuimte HuidigeRuimte { get; set; }
        private Vector2 positie { get; set; }
        public GeanimeerdeTexture SpriteAnimatie { get; set; }
        private float b { get; set; }
        public Persoon()
        {
            Random random = new Random();
            int a = random.Next(1, 9);
            b = (float)a / 10;
        }
        public void LoopNaarRuimte(HotelRuimte bestemming, HotelRuimte huidigeRuimte)
        {
            
            Bestemming = bestemming;
            HuidigeRuimte = huidigeRuimte;

            if (positie.X == 0 && positie.Y == 0)
            {
                // Zet de positie goed en zorg ervoor dat de schoonmaker met beide benen op de grond komt te staan
                positie = new Vector2(huidigeRuimte.CoordinatenInSpel.X, huidigeRuimte.CoordinatenInSpel.Y + 20);
            }

            //Als positie gelijk is aan bestemming
            Console.WriteLine(bestemming.CoordinatenInSpel);
            if (positie.X >= bestemming.CoordinatenInSpel.X && positie.X <= bestemming.CoordinatenInSpel.X + 6)
            {
                Console.WriteLine("AANGEKOMEN op bestemming");
            }
            else
            {
                positie = new Vector2(positie.X - b, positie.Y);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteAnimatie.ToonFrame(spriteBatch, positie);
        }
    }
}
