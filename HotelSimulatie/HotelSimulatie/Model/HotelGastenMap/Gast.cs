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
        public Vector2 OudeCoordinatenInSpel { get; set; }
        public Vector2 CoordinatenInSpel { get; set; }  // Plaats waar de gast zich op het bord bevindt
        public Texture2D Texture { get; set; }


        public void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>("Rob");
        }

        public void GaNaarRuimte(HotelRuimte ruimte)
        {
            OudeCoordinatenInSpel = CoordinatenInSpel;
            CoordinatenInSpel = ruimte.CoordinatenInSpel;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 20; i++)
            {
                spriteBatch.Draw(Texture, new Rectangle((Int32)OudeCoordinatenInSpel.X - i, (Int32)CoordinatenInSpel.Y, 48, 74), Color.White);
            }
        }
    }
}
