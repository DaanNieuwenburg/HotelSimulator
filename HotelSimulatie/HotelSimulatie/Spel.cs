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
        public GraphicsDeviceManager graphics { get; set; }
        private SpriteBatch spriteBatch { get; set; }
        public Hotel hotel { get; set; }
        public Vector2 GastSpawnLocatie { get; set; }
        public SpelCamera spelCamera { get; set; }
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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Laad de inputhandler
            Components.Add(new InputHandler(this));
            Components.Add(new AiHandler(this));
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            int hoogte = 678;
            int tegelBreedte = 150;

            Matrix matrix = Matrix.CreateTranslation(new Vector3(0, 40, 0));
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, spelCamera.TransformeerMatrix(GraphicsDevice));

            for (int y = 0; y < hotel.HotelLayout.GetLength(0); y++)
            {
                for (int x = 0; x < hotel.HotelLayout.GetLength(1); x++)
                {
                    // Koppelt de coordinaten aan de hotelruimte coordinaten property
                    hotel.HotelLayout[y, x].CoordinatenInSpel = new Vector2(x * tegelBreedte, hoogte);

                    // Toont de hotelruimte op het bord
                    hotel.HotelLayout[y, x].LoadContent(Content);
                    spriteBatch.Draw(hotel.HotelLayout[y, x].Texture, new Rectangle(x * tegelBreedte, hoogte, 150, 90), Color.White);

                    // In het geval van een lobby, bind hem dan aan spel.Lobby
                    if (hotel.HotelLayout[y, x] is Lobby)
                    {
                        hotel.LobbyRuimte = (Lobby)hotel.HotelLayout[y, x];  // temp

                        hotel.LobbyRuimte.LobbyRectangle = new Rectangle(x * tegelBreedte, hoogte, 150, 90);
                        GastSpawnLocatie = new Vector2(hotel.LobbyRuimte.CoordinatenInSpel.X, hotel.LobbyRuimte.CoordinatenInSpel.Y + 20);
                        hotel.LobbyRuimte.EventCoordinaten = new Vector2(GastSpawnLocatie.X + 50, hotel.LobbyRuimte.CoordinatenInSpel.Y + 20);
                    }
                }
                hoogte = hoogte - 90;
            }

            spriteBatch.End();
            Console.WriteLine(GastSpawnLocatie);
            base.Draw(gameTime);
        }
    }
}
