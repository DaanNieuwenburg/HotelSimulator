using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using HotelSimulatie.Model;
using HotelSimulatie.View;

namespace HotelSimulatie
{
    public class Spel : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        public Hotel hotel { get; set; }
        public SpelCamera spelCamera { get; set; }
        private bool muisKlik { get; set; }
        private Gast gastRob { get; set; }
        private Schoonmaker schoonmaker_A { get; set; }
        private Schoonmaker schoonmaker_B { get; set; }
        private Lift eersteLift { get; set; }
        public Spel(Hotel _hotel)
        {
            graphics = new GraphicsDeviceManager(this);
            Window.Title = "Hotel Simulator";
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            hotel = _hotel;
            spelCamera = new SpelCamera(hotel.HotelLayout.GetLength(0) * 90, hotel.HotelLayout.GetLength(1) * 150);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
        }

        protected override void Initialize()
        {
            gastRob = new Gast();
            schoonmaker_A = new Schoonmaker();
            schoonmaker_A.Texturenaam = "AnimatedSchoonmaker";
            schoonmaker_B = new Schoonmaker();
            schoonmaker_B.Texturenaam = "AnimatedTim";
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Components.Add(new InputHandler(this));
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //schoonmaker_A.LoadContent(Content);
            //schoonmaker_B.LoadContent(Content);
            gastRob.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            // Verplaatst gast over het scherm
            if (eersteLift != null && hotel.LobbyRuimte != null)
            {
                gastRob.LoopNaarRuimte(eersteLift, hotel.LobbyRuimte);
                gastRob.UpdateFrame(gameTime);
                //schoonmaker_A.LoopNaarRuimte(eersteLift, hotel.LobbyRuimte);
                //schoonmaker_A.UpdateFrame(gameTime);
                //schoonmaker_B.LoopNaarRuimte(hotel.LobbyRuimte, eersteLift);
                //schoonmaker_B.UpdateFrame(gameTime);
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            int hoogte = 678;
            int tegelBreedte = 150;

            Matrix matrix = Matrix.CreateTranslation(new Vector3(0, 40, 0));
            spriteBatch.Begin(SpriteSortMode.Immediate,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        spelCamera.TransformeerMatrix(GraphicsDevice));

            for (int y = 0; y < hotel.HotelLayout.GetLength(0); y++)
            {
                for (int x = 0; x < hotel.HotelLayout.GetLength(1); x++)
                {
                    // Koppelt de coordinaten aan de hotelruimte coordinaten property
                    hotel.HotelLayout[y, x].CoordinatenInSpel = new Vector2(x * tegelBreedte, hoogte);

                    if (hotel.HotelLayout[y, x] is Lobby)
                    {
                        // -Temp code-
                        hotel.LobbyRuimte = (Lobby)hotel.HotelLayout[y, x];  // temp
                        hotel.Gastenlijst[0].HuidigeRuimte = hotel.LobbyRuimte;
                        hotel.LobbyRuimte.LobbyRectangle = new Rectangle(x * tegelBreedte, hoogte, 150, 90);
                    }
                    else if (hotel.HotelLayout[y, x] is Lift)
                    {
                        // -Temp code-
                        eersteLift = (Lift)hotel.HotelLayout[y, x];
                    }
                    // Toont de hotelruimte op het bord
                    hotel.HotelLayout[y, x].LoadContent(Content);
                    spriteBatch.Draw(hotel.HotelLayout[y, x].Texture, new Rectangle(x * tegelBreedte, hoogte, 150, 90), Color.White);
                }
                hoogte = hoogte - 90;
            }

            // Toon schoonmaker
            if (schoonmaker_A.HuidigeRuimte != null)
            {
                ///schoonmaker_A.Draw(spriteBatch);
            }
            if (schoonmaker_B.HuidigeRuimte != null)
            {
                ///schoonmaker_B.Draw(spriteBatch);
            }
            // Probeer gast Rob te tonen
            if (gastRob.HuidigeRuimte != null)
            {
                gastRob.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
