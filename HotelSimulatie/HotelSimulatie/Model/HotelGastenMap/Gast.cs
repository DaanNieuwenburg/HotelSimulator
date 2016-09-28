using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HotelSimulatie.Model
{
    public class Gast
    {
        public int Gastnummer { get; set; }
        public bool Honger { get; set; }
        public int Kamernummer { get; set; }
        public bool Wacht { get; set; }
        public HotelRuimte Bestemming { get; set; }
        public HotelRuimte HuidigeRuimte { get; set; }
        private Vector2 positie { get; set; }
        public GeanimeerdeTexture SpriteAnimatie { get; set; }
        public List<Texture2D> Texturelijst { get; set; }

        public void LoadContent(ContentManager contentManager)
        {
            SpriteAnimatie = new GeanimeerdeTexture(contentManager, "AnimatedRob", 3);
        }

        public void UpdateFrame(GameTime spelTijd)
        {
            SpriteAnimatie.UpdateFrame(spelTijd);
        }

        public void LoopNaarRuimte(HotelRuimte bestemming, HotelRuimte huidigeRuimte)
        {
            Bestemming = bestemming;
            HuidigeRuimte = huidigeRuimte;

            if (positie.X == 0 && positie.Y == 0)
            {
                // Zet de positie goed en zorg ervoor dat de gast met beide benen op de grond komt te staan
                positie = new Vector2(huidigeRuimte.CoordinatenInSpel.X, huidigeRuimte.CoordinatenInSpel.Y + 18);
            }

            // Als positie gelijk is aan bestemming
            Console.WriteLine(bestemming.CoordinatenInSpel);
            if (positie.X == bestemming.CoordinatenInSpel.X)
            {
                Console.WriteLine("AANGEKOMEN op bestemming");
            }
            else
            {
                positie = new Vector2(positie.X + 0.1f, positie.Y);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteAnimatie.ToonFrame(spriteBatch, positie);
        }
    }
}
