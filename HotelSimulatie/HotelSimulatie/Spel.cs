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

namespace HotelSimulatie
{
    public class Spel : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Texture2D> tegelTextureLijst;
        Hotel hotel { get; set; }
        int tegelBreedte = 150;
        int tegelHoogte = 90;
        private SpelCamera spelcamera { get; set; }

        public Spel(Hotel _hotel)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            Window.Title = "Hotel Simulator";
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            spelcamera = new SpelCamera();
            hotel = _hotel;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tegelTextureLijst = new List<Texture2D>();                          // --  Nummering in 2d array
            tegelTextureLijst.Add(Content.Load<Texture2D>("Lobby"));            // 0
            tegelTextureLijst.Add(Content.Load<Texture2D>("1SterHotelKamer"));  // 1
            tegelTextureLijst.Add(Content.Load<Texture2D>("2SterHotelKamer"));  // 2
            tegelTextureLijst.Add(Content.Load<Texture2D>("3SterHotelKamer"));  // 3
            tegelTextureLijst.Add(Content.Load<Texture2D>("4SterHotelKamer"));  // 4
            tegelTextureLijst.Add(Content.Load<Texture2D>("5SterHotelKamer"));  // 5
            tegelTextureLijst.Add(Content.Load<Texture2D>("Lift"));             // 6
            tegelTextureLijst.Add(Content.Load<Texture2D>("Trap"));             // 7
            tegelTextureLijst.Add(Content.Load<Texture2D>("Eetzaal"));          // 8
            tegelTextureLijst.Add(Content.Load<Texture2D>("Fitness"));          // 9
            tegelTextureLijst.Add(Content.Load<Texture2D>("Bioscoop"));         // 10
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            // Zorgt ervoor dat de camera bediend kan worden
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Up))
            {
                Vector2 nieuweVector = spelcamera.Positie;
                nieuweVector.Y = nieuweVector.Y + 1;
                spelcamera.Beweeg(nieuweVector);
            }
            if(ks.IsKeyDown(Keys.Down))
            {
                Vector2 nieuweVector = spelcamera.Positie;
                nieuweVector.Y = nieuweVector.Y - 1;
                spelcamera.Beweeg(nieuweVector);
            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            int hoogte = 678;

            Matrix matrix = Matrix.CreateTranslation(new Vector3(0, 40, 0));
            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        spelcamera.TransformeerMatrix(GraphicsDevice));
            for (int y = 0; y < hotel.HotelLayout.GetLength(0); y++)
            {
                for (int x = 0; x < hotel.HotelLayout.GetLength(1); x++)
                {
                    spriteBatch.Draw(tegelTextureLijst[hotel.HotelLayout[y, x].TextureCode], new Vector2(x * tegelBreedte, hoogte), Color.White);
                }

                hoogte = hoogte - 90;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
