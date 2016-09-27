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
        public Vector2 CoordinatenInSpel { get; set; }  // Plaats waar de gast zich op het bord bevindt
        public Texture2D Texture { get; set; }


        public void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>("Rob");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 vector = new Vector2(380, 760 - 65);
            spriteBatch.Draw(Texture, new Rectangle((Int32)vector.X,(Int32)vector.Y, 48, 74), Color.White);
            CoordinatenInSpel = vector;
        }

        public Vector2 GaNaarRuimte(HotelRuimte ruimte)
        {
            
        }
    }
}
