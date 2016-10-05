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
        public Kamer EersteKamer { get; set; }
        public Matrix matrix { get; set; }

        public Spel(Hotel _hotel)
        {
            graphics = new GraphicsDeviceManager(this);
            Window.Title = "Hotel Simulator";
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            hotel = _hotel;
            spelCamera = new SpelCamera(540, 750);
            //spelCamera = new SpelCamera(hotel.HotelLayout.GetLength(0) * 90, hotel.HotelLayout.GetLength(1) * 150);
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

            matrix = Matrix.CreateTranslation(new Vector3(0, 40, 0));
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, spelCamera.TransformeerMatrix(GraphicsDevice));
            spriteBatch.Draw(Content.Load<Texture2D>("Background1"), Vector2.Zero, Color.White);
            

            foreach (HotelRuimte hotelRuimte in hotel.NodeLijst)
            {
                hotelRuimte.LoadContent(Content);

                // Tekent de textures op het scherm
                spriteBatch.Draw(hotelRuimte.Texture, new Rectangle((Int32)hotelRuimte.CoordinatenInSpel.X, (Int32)hotelRuimte.CoordinatenInSpel.Y, (Int32)hotelRuimte.Afmetingen.X, (Int32)hotelRuimte.Afmetingen.Y), Color.White);

                // Zet de coordinaten van de hotelruimte goed
                hotelRuimte.CoordinatenInSpel = new Vector2((Int32)hotelRuimte.CoordinatenInSpel.X, (Int32)hotelRuimte.CoordinatenInSpel.Y);

                // Zet overige posities goed
                if (hotelRuimte is Lobby)
                {
                    hotel.LobbyRuimte = (Lobby)hotelRuimte;
                    hotel.LobbyRuimte.hotel = hotel;
                    hotel.LobbyRuimte.LobbyRectangle = new Rectangle((Int32)hotelRuimte.CoordinatenInSpel.X, (Int32)hotelRuimte.CoordinatenInSpel.Y, (Int32)hotelRuimte.Afmetingen.X, (Int32)hotelRuimte.Afmetingen.Y);
                    GastSpawnLocatie = new Vector2((Int32)hotelRuimte.CoordinatenInSpel.X, (Int32)hotelRuimte.CoordinatenInSpel.Y + 20);
                    hotel.LobbyRuimte.EventCoordinaten = new Vector2((Int32)hotelRuimte.CoordinatenInSpel.X + 10, (Int32)hotelRuimte.CoordinatenInSpel.Y + 20);
                }
                if (hotelRuimte is Trap)
                {
                    hotelRuimte.EventCoordinaten = new Vector2((Int32)hotelRuimte.CoordinatenInSpel.X + 53, (Int32)hotelRuimte.CoordinatenInSpel.Y + 20);
                }

            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}