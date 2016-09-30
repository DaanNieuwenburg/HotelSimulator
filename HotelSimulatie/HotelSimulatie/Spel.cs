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
        public Vector2 GastSpawnLocatie { get; set; }
        public SpelCamera spelCamera { get; set; }
        private Gast gastRob { get; set; }
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
            foreach (Gast gast in hotel.Gastenlijst)
            {
                gast.LoadContent(Content);
            }

            Components.Add(new InputHandler(this));
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            // Zet nieuwe gasten op de spawnpoint
            if (GastSpawnLocatie != null)
            {
                foreach (Gast gast in hotel.Gastenlijst)
                {
                    Vector2 legePos = new Vector2(0, 0);

                    // In dit geval is er sprake van een nieuwe gast
                    if (gast.Positie == legePos && hotel.LobbyRuimte != null)
                    {
                        gast.Positie = GastSpawnLocatie;
                        gast.Bestemming = hotel.LobbyRuimte;
                        gast.LoopNaarRuimte(hotel.LobbyRuimte, hotel.LobbyRuimte);
                        gast.UpdateFrame(gameTime);
                    }
                    gast.LoadContent(Content);
                }
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
                        GastSpawnLocatie = new Vector2(hotel.LobbyRuimte.CoordinatenInSpel.X, hotel.LobbyRuimte.CoordinatenInSpel.Y + 20);
                        hotel.Gastenlijst[0].HuidigeRuimte = hotel.LobbyRuimte;
                        hotel.LobbyRuimte.LobbyRectangle = new Rectangle(x * tegelBreedte, hoogte, 150, 90);
                    }

                    // Toont de hotelruimte op het bord
                    hotel.HotelLayout[y, x].LoadContent(Content);
                    spriteBatch.Draw(hotel.HotelLayout[y, x].Texture, new Rectangle(x * tegelBreedte, hoogte, 150, 90), Color.White);
                }
                hoogte = hoogte - 90;
            }

            // Toon gasten
            foreach(Gast gast in hotel.Gastenlijst)
            {
                gast.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
