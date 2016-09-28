using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            TijdPerFrame = 0.005f;
        }

        public void UpdateFrame(GameTime spelTijd)
        {
            VerstrekenTijd += (float)spelTijd.ElapsedGameTime.TotalSeconds;
            if(VerstrekenTijd > TijdPerFrame)
            {
                Frame++;
                TijdPerFrame = TijdPerFrame + VerstrekenTijd;
                VerstrekenTijd -= TijdPerFrame;
            }
            else
            {
                // Reset het aantal frames naar 0, zodat er weer vanaf 0 geteld wordt.
                if (Frame == TotaalAantalFrames)
                {
                    Frame = 0;
                }
            }
        }

        public void ToonFrame(SpriteBatch spriteBatch, Vector2 positie)
        {
            int FrameGrootte = Texture.Width / 4;
            Rectangle sourcerect = new Rectangle(FrameGrootte * Frame, 0, FrameGrootte, Texture.Height);
            spriteBatch.Draw(Texture, positie, sourcerect, Color.White);
            
        }
    }
}
