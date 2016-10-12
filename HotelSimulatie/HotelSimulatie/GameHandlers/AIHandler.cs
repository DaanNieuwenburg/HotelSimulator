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
                    if (gast.HuidigEvent.NEvent == HotelEventAdapter.NEventType.CHECK_IN)
                    {
                        gast.Inchecken(spel.hotel.LobbyRuimte, gameTime);
                    }
                    else if(gast.HuidigEvent.NEvent == HotelEventAdapter.NEventType.CHECK_OUT)
                    {
                        Lobby lobby = spel.hotel.LobbyRuimte;
                        gast.GaNaarKamer<Lobby>(ref lobby);
                    }
                    else if(gast.HuidigEvent.NEvent == HotelEventAdapter.NEventType.GOTO_CINEMA)
                    {
                        Bioscoop bioscoop = spel.hotel.bioscoop;
                        gast.GaNaarKamer<Bioscoop>(ref bioscoop);
                    }
                    else if(gast.HuidigEvent.NEvent == HotelEventAdapter.NEventType.GOTO_FITNESS)
                    {
                        Fitness fitness = spel.hotel.fitness;
                        gast.GaNaarKamer<Fitness>(ref fitness);
                    }
                    else if(gast.HuidigEvent.NEvent == HotelEventAdapter.NEventType.GOTO_ROOM)
                    {
                            Kamer kamer = gast.ToegewezenKamer;
                            gast.GaNaarKamer<Kamer>(ref kamer);
                    }
                    else if(gast.HuidigEvent.NEvent == HotelEventAdapter.NEventType.EVACUATE)
                    {
                        Lobby lobby = spel.hotel.LobbyRuimte;
                        gast.GaNaarKamer<Lobby>(ref lobby);
                        spel.hotel.Evacueer();
                    }
                    else if(gast.HuidigEvent.NEvent == HotelEventAdapter.NEventType.NEED_FOOD)
                    {
                        Eetzaal eetzaal = new Eetzaal();
                        gast.GaNaarKamer<Eetzaal>(ref eetzaal);
                    }
                }
            }

            // Update de lift
            if (spel.hotel.lift.EventCoordinaten.X != 0 && spel.hotel.lift.EventCoordinaten.Y != 0)
            {
                spel.hotel.lift.UpdateLift();
            }
            else
            {
                spel.hotel.lift.InitializeerLift();
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
