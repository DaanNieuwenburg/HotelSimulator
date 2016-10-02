using HotelSimulatie.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie
{
    public class AiHandler : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch { get; set; }
        private Spel spell { get; set; }
        private ContentManager Content { get; set; }
        private Hotel hotel { get; set; }
        private bool eersteKeer { get; set; }

        public AiHandler(Game game) : base(game)
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            Spel spel = (Spel)game;
            hotel = spel.hotel;
            Content = spel.Content;
            spell = spel;
            eersteKeer = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (eersteKeer == false)
            {
                GastenUpdate(gameTime);
            }
            base.Update(gameTime);
        }

        private void GastenUpdate(GameTime gameTime)
        {
            foreach (Gast gast in hotel.Gastenlijst)
            {
                // Spawn nieuwe gast als hij nog niet bestaat, ga dan inchecken
                if (gast.SpriteAnimatie == null && hotel.LobbyRuimte != null)
                {
                    Vector2 legeVector = new Vector2(0, 0);
                    if (gast.Positie == legeVector)
                    {
                        gast.Positie = spell.GastSpawnLocatie;
                    }
                    gast.LoadContent(Content);
                    gast.LoopNaarRuimte(hotel.LobbyRuimte, hotel.LobbyRuimte);
                }
                gast.UpdateFrame(gameTime);
            }
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();

            if (eersteKeer == false)
            {
                // Toon gasten
                foreach (Gast gast in hotel.Gastenlijst)
                {
                    gast.Draw(spriteBatch);
                }
            }
            else
            {
                eersteKeer = false;
            }

            spriteBatch.End();
        }

    }
}
