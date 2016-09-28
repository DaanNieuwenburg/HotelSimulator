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
            TijdPerFrame = 900;
        }

        public void UpdateFrame(GameTime spelTijd)
        {
            VerstrekenTijd += spelTijd.ElapsedGameTime.Milliseconds;
            Console.WriteLine("Verstrekentijd " + VerstrekenTijd);
            if (VerstrekenTijd > TijdPerFrame)
            {
                Frame++;
                VerstrekenTijd = 0;
            }
            else
            {
                // Reset het aantal frames naar 0, zodat er weer vanaf 0 geteld wordt.
                if (Frame == TotaalAantalFrames)
                {
                    Frame = 1;
                }
            }
        }

        public void ToonFrame(SpriteBatch spriteBatch, Vector2 positie)
        {
            int FrameGrootte = Texture.Width / TotaalAantalFrames;
            Rectangle sourcerect = new Rectangle(FrameGrootte * Frame, 0, FrameGrootte, Texture.Height);
            spriteBatch.Draw(Texture, positie, sourcerect, Color.White);
        }
    }
}
