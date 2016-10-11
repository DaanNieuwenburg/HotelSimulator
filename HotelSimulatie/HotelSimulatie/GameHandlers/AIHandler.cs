using HotelEvents;
using HotelSimulatie.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie
{
    public class AIHandler : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Simulatie spel { get; set; }
        public AIHandler(Game game) : base(game)
        {
            spel = (Simulatie)game;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // For loop aangezien we aanpassing maken aan de gasten die in de lijst staan
            for(int i = 0; i < spel.hotel.GastenLijst.Count(); i++)
            {
                Gast gast = spel.hotel.GastenLijst[i];
                if (gast.HuidigEvent != null)
                {
                    if (gast.HuidigEvent.EventType == HotelEventType.CHECK_IN)
                    {
                        gast.Inchecken(spel.hotel.LobbyRuimte, gameTime);
                    }
                    else if(gast.HuidigEvent.EventType == HotelEventType.CHECK_OUT)
                    {
                        gast.Uitchecken(spel.hotel.LobbyRuimte);
                    }
                    else if(gast.HuidigEvent.EventType == HotelEventType.GOTO_CINEMA)
                    {
                        Console.WriteLine("Cinema");
                        gast.GaNaarBioscoop(spel.hotel.bioscoop);
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            spel.matrix = Matrix.CreateTranslation(new Vector3(0, 40, 0));

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, spel.spelCamera.TransformeerMatrix(this.Game.GraphicsDevice));
            base.Draw(gameTime);

            // Toon gasten
            foreach (Gast gast in spel.hotel.GastenLijst)
            {
                if (gast.Bestemming != null)
                {
                    gast.Draw(spriteBatch);
                }
            }

            spriteBatch.End();
        }
    }
}
