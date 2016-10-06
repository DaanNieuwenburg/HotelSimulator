using HotelSimulatie.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEvents;

namespace HotelSimulatie
{
    public class HotelEventHandler : Microsoft.Xna.Framework.DrawableGameComponent, HotelEventListener
    {
        private Spel spel { get; set; }
        private bool eersteKeer { get; set; }

        public HotelEventHandler(Game game) : base(game)
        {
            spel = (Spel)game;

            // Start de event listener
            HotelEventManager.Start();
            HotelEventManager.Register(this);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Notify(HotelEvent evt)
        {
            Dictionary<string, string> eventData = evt.Data;
            if (evt.Data != null)
            {
                foreach (KeyValuePair<string,string> gastEvent in eventData)
                {
                    Gast gevondenGast = spel.hotel.GastenLijst.Find(o => o.Naam == gastEvent.Key);
                    if(gevondenGast == null)
                    {
                        spel.hotel.GastenLijst.Add(new Gast() { Naam = gastEvent.Key });
                    }

                    if (gastEvent.Value.Contains("Checkin"))
                        {

                    }
                    /*if(gastEvent.Value == HotelEventType.CHECK_IN)
                    {

                    }*/
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
                gast.Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
