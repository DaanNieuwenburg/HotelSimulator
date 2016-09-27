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
        public string Positie { get; set; }
        public HotelRuimte Bestemming { get; set; }
        public HotelRuimte HuidigeRuimte { get; set; }  
        public Texture2D Texture { get; set; }

        // Store some information about the sprite's motion.
        Vector2 spriteSpeed = new Vector2(50.0f, 50.0f);

        public void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>("Rob");
        }
        
        public void GaNaarRuimte(HotelRuimte bestemming, HotelRuimte huidigeRuimte)
        {
            Bestemming = bestemming;
            HuidigeRuimte = huidigeRuimte;
            Vector2 vector = new Vector2(350, 20);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((Int32)HuidigeRuimte.CoordinatenInSpel.X + 45, (Int32)HuidigeRuimte.CoordinatenInSpel.Y + 16, 48, 74), Color.White);
        }
    }
}
