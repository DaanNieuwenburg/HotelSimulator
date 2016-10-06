using HotelSimulatie.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEvents;
using System.Text.RegularExpressions;

namespace HotelSimulatie
{
    public class HotelEventHandler : Microsoft.Xna.Framework.DrawableGameComponent, HotelEventListener
    {
        private Spel spel { get; set; }
        private bool eersteKeer { get; set; }
        private HotelEvent Event { get; }
        private GameTime GameTijd { get; set; }
        
        public int Tijd { get; set; }

        public HotelEventHandler(Game game) : base(game)
        {
            
            spel = (Spel)game;
            Event = new HotelEvent();
            // Start de event listener
            HotelEventManager.Start();
            HotelEventManager.Register(this);
        }
        protected override void LoadContent()
        {
            
        }
        public override void Update(GameTime gameTime)
        {
            GameTijd = gameTime;
            base.Update(gameTime);
        }

        public void Notify(HotelEvent evt)
        {
            Dictionary<string, string> eventData = evt.Data;
            if (evt.Data != null)
            {
                foreach (KeyValuePair<string, string> gastEvent in eventData)
                {
                    Gast gevondenGast = spel.hotel.GastenLijst.Find(o => o.Naam == gastEvent.Key);
                    if (gevondenGast == null)
                    {
                        Gast nieuweGast = new Gast() { Naam = gastEvent.Key, Positie = spel.GastSpawnLocatie };
                        nieuweGast.LoadContent(Game.Content);
                        spel.hotel.GastenLijst.Add(nieuweGast);
                    }
                    else
                    {
                    if (gastEvent.Value.Contains("Checkin"))
                    {
                            Regex regexToSplit = new Regex(@"([1-9])");
                            string[] teSplitten = regexToSplit.Split(gastEvent.Value);
                            string kamernummer = teSplitten[1];
                            gevondenGast.Inchecken(spel.hotel.LobbyRuimte, GameTijd, Convert.ToInt32(kamernummer));
                        }
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
                gast.Draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
