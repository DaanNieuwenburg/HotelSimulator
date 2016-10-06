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
        private HotelEvent Event { get;}
        private bool timechanged { get; set; }

        private Vector2 Tijdpostitie { get; set; }
        
        public int Tijd { get; set; }

        public HotelEventHandler(Game game) : base(game)
        {
            Tijdpostitie = new Vector2(0, 700);
            
            spel = (Spel)game;
            Event = new HotelEvent();
            Tijd = Event.Time;
            // Start de event listener
            HotelEventManager.Start();
            HotelEventManager.Register(this);
        }
        protected override void LoadContent()
        {
            
        }
        public override void Update(GameTime gameTime)
        {
            if(Tijd != Event.Time)
            {

            }
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
                        Gast nieuweGast = new Gast() { Naam = gastEvent.Key, Positie = spel.GastSpawnLocatie };
                        nieuweGast.LoadContent(Game.Content);
                        spel.hotel.GastenLijst.Add(nieuweGast);
                    }

                    if (gastEvent.Value.Contains("Checkin"))
                    {

                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            spel.matrix = Matrix.CreateTranslation(new Vector3(0, 40, 0));

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, spel.spelCamera.TransformeerMatrix(this.Game.GraphicsDevice));
            spriteBatch.DrawString(spel.font, "Tijd: " + Event.Time.ToString(), Tijdpostitie, Color.Red);
            base.Draw(gameTime);

            

            // Toon gasten
            foreach (Gast gast in spel.hotel.GastenLijst)
            {
                //spriteBatch.Draw(gast.SpriteAnimatie.Texture, gast.Positie, Color.White);
                gast.Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
