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
            int tegelBreedte = 150;
            
            matrix = Matrix.CreateTranslation(new Vector3(0, 40, 0));
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, spelCamera.TransformeerMatrix(GraphicsDevice));
            spriteBatch.Draw(Content.Load<Texture2D>("Background1"), Vector2.Zero, Color.White);

            int x = 0;
            int y = 678;

            foreach (HotelRuimte hotelRuimte in hotel.NodeLijst)
            {
                hotelRuimte.CoordinatenInSpel = new Vector2(x * tegelBreedte, y);
                hotelRuimte.LoadContent(Content);
                spriteBatch.Draw(hotelRuimte.Texture, new Rectangle(x * tegelBreedte, y, 150, 90), Color.White);
                x++;

                if(hotelRuimte is Lobby)
                {
                    hotel.LobbyRuimte = (Lobby)hotelRuimte;

                    hotel.LobbyRuimte.LobbyRectangle = new Rectangle((x - 1) * tegelBreedte, y, 150, 90);
                    GastSpawnLocatie = new Vector2(hotel.LobbyRuimte.CoordinatenInSpel.X, hotel.LobbyRuimte.CoordinatenInSpel.Y + 20);
                    hotel.LobbyRuimte.EventCoordinaten = new Vector2(GastSpawnLocatie.X + 10, hotel.LobbyRuimte.CoordinatenInSpel.Y + 20);
                }
                if(hotelRuimte is Trap)
                {
                    hotelRuimte.EventCoordinaten = new Vector2(hotelRuimte.CoordinatenInSpel.X + 53, hotelRuimte.CoordinatenInSpel.Y + 20);
                }

                if (hotelRuimte is Kamer && y == 408 && x == 3)
                {
                    EersteKamer = (Kamer)hotelRuimte;
                    EersteKamer.EventCoordinaten = new Vector2(EersteKamer.CoordinatenInSpel.X, y);
                }

                if (hotelRuimte is Kamer)
                {
                    hotelRuimte.EventCoordinaten = new Vector2(hotelRuimte.CoordinatenInSpel.X, hotelRuimte.CoordinatenInSpel.Y);
                }

                // Ga naar de volgende verdieping
                if (hotelRuimte is Liftschacht)
                {
                    x = 0;
                    y = y - 90;
                }
            }

            spriteBatch.End();
            Console.WriteLine(GastSpawnLocatie);
            base.Draw(gameTime);
        }
    }
}