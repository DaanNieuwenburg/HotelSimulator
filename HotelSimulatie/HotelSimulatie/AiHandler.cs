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
        private Spel spel { get; set; }
        private ContentManager Content { get; set; }
        private bool eersteKeer { get; set; }

        public AiHandler(Game game) : base(game)
        {
            spel = (Spel)game;
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
            foreach (Gast gast in spel.hotel.Gastenlijst)
            {
                // Maak nieuwe gast indien nodig
                if (gast.SpriteAnimatie == null && spel.hotel.LobbyRuimte != null)
                {
                    MaakNieuweGast(gast);
                }
                // Inchecken
                else if (gast.Kamernummer == null && gast.BestemmingBereikt == false)
                {
                    gast.Inchecken(spel.hotel.LobbyRuimte, gameTime, spel.EersteKamer);
                }
                // Loop naar een ruimte
                else if(gast.BestemmingBereikt == false)
                {
                    gast.LoopNaarRuimte();
                }
                gast.UpdateFrame(gameTime);
            }
        }

        private void MaakNieuweGast(Gast gast)
        {
            // Spawn nieuwe gast als hij nog niet bestaat
            if (gast.SpriteAnimatie == null && spel.hotel.LobbyRuimte != null)
            {
                Vector2 legeVector = new Vector2(0, 0);
                if (gast.Positie == legeVector)
                {
                    gast.Positie = spel.GastSpawnLocatie;
                    gast.HuidigeRuimte = spel.hotel.LobbyRuimte;
                }
                gast.LoadContent(spel.Content);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            spel.matrix = Matrix.CreateTranslation(new Vector3(0, 40, 0));
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, spel.spelCamera.TransformeerMatrix(this.Game.GraphicsDevice));
            base.Draw(gameTime);

            if (eersteKeer == false)
            {
                // Toon gasten
                foreach (Gast gast in spel.hotel.Gastenlijst)
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
