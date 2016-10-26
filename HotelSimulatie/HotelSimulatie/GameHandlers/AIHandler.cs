using HotelEvents;
using HotelSimulatie.Model;
using HotelSimulatie.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            for (int i = 0; i < spel.hotel.GastenLijst.Count(); i++)
            {
                Gast gast = spel.hotel.GastenLijst[i];
                if (gast.HuidigEvent != null && gast.inLift == false)
                {
                    if (gast.HuidigEvent.NEvent == HotelEventAdapter.NEventType.CHECK_IN)
                    {
                        gast.Inchecken(spel.hotel.hotelLayout.lobby, gameTime);
                    }
                    else if (gast.HuidigEvent.NEvent == HotelEventAdapter.NEventType.CHECK_OUT)
                    {
                        Lobby lobby = spel.hotel.hotelLayout.lobby;
                        gast.GaNaarKamer<Lobby>(ref lobby);
                    }
                    else if (gast.HuidigEvent.NEvent == HotelEventAdapter.NEventType.GOTO_CINEMA)
                    {
                        Bioscoop bioscoop = spel.hotel.hotelLayout.bioscoop;
                        gast.GaNaarKamer<Bioscoop>(ref bioscoop);
                    }
                    else if (gast.HuidigEvent.NEvent == HotelEventAdapter.NEventType.GOTO_FITNESS)
                    {
                        Fitness fitness = spel.hotel.hotelLayout.fitness;
                        gast.GaNaarKamer<Fitness>(ref fitness);
                    }
                    else if (gast.HuidigEvent.NEvent == HotelEventAdapter.NEventType.GOTO_ROOM)
                    {
                        Kamer kamer = gast.ToegewezenKamer;
                        gast.GaNaarKamer<Kamer>(ref kamer);
                    }
                    else if (gast.HuidigEvent.NEvent == HotelEventAdapter.NEventType.EVACUATE)
                    {
                        Lobby lobby = spel.hotel.hotelLayout.lobby;
                        gast.GaNaarKamer<Lobby>(ref lobby);
                        spel.hotel.Evacueer();
                    }
                    else if (spel.hotel.hotelLayout.bioscoop.filmbezig == true && spel.hotel.hotelLayout.bioscoop.HuidigEvent.NEvent == HotelEventAdapter.NEventType.START_CINEMA)
                    {
                        Bioscoop bioscoop = spel.hotel.hotelLayout.bioscoop;
                        bioscoop.Update(gameTime);
                    }
                    else if (gast.HuidigEvent.NEvent == HotelEventAdapter.NEventType.NEED_FOOD)
                    {
                        Eetzaal eetzaal = new Eetzaal();
                        gast.GaNaarKamer<Eetzaal>(ref eetzaal);
                    }
                }
            }

            // Update de schoonmakers
            foreach (Schoonmaker schoonmaker in spel.hotel.Schoonmakers)
            {
                if (schoonmaker.Positie == new Vector2(0, 0))
                {
                    schoonmaker.Positie = spel.GastSpawnLocatie;
                    schoonmaker.HuidigeRuimte = spel.hotel.hotelLayout.lobby;
                }
                else
                {
                    schoonmaker.Update(gameTime);
                }
            }

            // Update de lift
            if (spel.hotel.hotelLayout.lift.EventCoordinaten.X != 0 && spel.hotel.hotelLayout.lift.EventCoordinaten.Y != 0)
            {
                spel.hotel.hotelLayout.lift.UpdateLift();
            }
            else
            {
                spel.hotel.hotelLayout.lift.InitializeerLift();
            }

            // Update de eetzaal
            foreach (Eetzaal eetzaal in spel.hotel.hotelLayout.eetzalen)
            {
                eetzaal.Update(gameTime);
            }
            spel.hotel.hotelLayout.fitness.Update(gameTime);

            
            // Update Lobbymenu
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm is LobbyMenu)
                {
                    LobbyMenu temp = (LobbyMenu)frm;
                    temp.RefreshInfo();
                }
            }
            // Controleer dood van gast
            foreach (Gast gast in spel.hotel.GastenLijst)
            {
                if (gast.isDood == false)
                {
                    if (gast.Wachtteller.Elapsed.Seconds >= 6)
                    {
                        gast.isDood = true;
                        gast.SpriteAnimatie = new GeanimeerdeTexture(spel.Content, @"Gasten\spook", 1);
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
            int a = spel.hotel.GastenLijst.Count;
            for (int i = 0; i < a; i++)
            {
                Gast gast = spel.hotel.GastenLijst[i];
                if (gast.Bestemming != null && gast.inLift == false)
                {
                    gast.Draw(spriteBatch);
                }
            }

            // Toon schoonmakers
            spel.hotel.Schoonmakers[0].Draw(spriteBatch);
            spel.hotel.Schoonmakers[1].Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}