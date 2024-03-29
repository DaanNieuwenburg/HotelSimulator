﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HotelSimulatie
{
    public class GeanimeerdeTexture
    {
        public Texture2D Texture { get; set; }
        public float VerstrekenTijd { get; set; }
        public float TijdPerFrame { get; set; }
        public int Frame { get; set; }
        public int TotaalAantalFrames { get; set; }
        public GeanimeerdeTexture(ContentManager contentManager, string textureNaam, int totaalAantalFrames)
        {
            Frame = 0;
            Texture = contentManager.Load<Texture2D>(textureNaam);
            TotaalAantalFrames = totaalAantalFrames;
            VerstrekenTijd = 0;
            TijdPerFrame = 200;
        }

        public void UpdateFrame(GameTime spelTijd)
        {
            VerstrekenTijd += spelTijd.ElapsedGameTime.Milliseconds;
            if (VerstrekenTijd > TijdPerFrame)
            {
                Frame++;
                VerstrekenTijd = 0;
            }
        }

        public void ToonFrame(SpriteBatch spriteBatch, Vector2 positie)
        {
            if (Texture != null)
            {
                int FrameGrootte = Texture.Width / TotaalAantalFrames;
                Rectangle sourcerect = new Rectangle(Frame * FrameGrootte, 0, FrameGrootte, 74);
                spriteBatch.Draw(Texture, positie, sourcerect, Color.White);
            }

            if (Frame >= TotaalAantalFrames)
            {
                Frame = 0;
            }
        }
    }
}
