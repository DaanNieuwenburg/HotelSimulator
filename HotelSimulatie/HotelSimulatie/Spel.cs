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
    public class Spel : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private List<Texture2D> tegelTextureLijst;
        private Hotel hotel { get; set; }
        private SpelCamera spelCamera { get; set; }
        private Rectangle lobby { get; set; }
        private bool muisKlik { get; set; }
        private Gast gastRob { get; set; }

        public Spel(Hotel _hotel)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            Window.Title = "Hotel Simulator";
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            hotel = _hotel;
            spelCamera = new SpelCamera(hotel.HotelLayout.GetLength(0) * 90, hotel.HotelLayout.GetLength(1) * 150);
            
        }

        protected override void Initialize()
        {
            gastRob = new Gast();
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

            gastRob.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            // Zorgt ervoor dat de camera bediend kan worden
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Vector2 nieuweVector = spelCamera.Positie;
                nieuweVector.Y = nieuweVector.Y - 1;
                spelCamera.Beweeg(nieuweVector);
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                Vector2 nieuweVector = spelCamera.Positie;
                nieuweVector.Y = nieuweVector.Y + 1;
                spelCamera.Beweeg(nieuweVector);
            }

            // Voor links en rechts
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Vector2 nieuweVector = spelCamera.Positie;
                nieuweVector.X = nieuweVector.X - 1;
                spelCamera.Beweeg(nieuweVector);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                Vector2 nieuweVector = spelCamera.Positie;
                nieuweVector.X = nieuweVector.X + 1;
                spelCamera.Beweeg(nieuweVector);
            }

            // Kijk of muis op de lobby staat
            MouseState muisStatus = Mouse.GetState();
            Vector2 muisLocatie = new Vector2(muisStatus.X, muisStatus.Y);
            Console.WriteLine(muisLocatie.X + " " + muisLocatie.Y);
            muisLocatie = muisLocatie + spelCamera.Positie;
            if(lobby.Contains(Convert.ToInt32(muisLocatie.X), Convert.ToInt32(muisLocatie.Y)) && muisStatus.LeftButton == ButtonState.Pressed && muisKlik == false)
            {
                muisKlik = true;
                // Open een nieuw scherm met info over het spel
                LobbyMenu lobbyMenu = new LobbyMenu(hotel);
                lobbyMenu.ShowDialog();
                if(lobbyMenu.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    muisKlik = false;
                }
            }
            base.Update(gameTime);

            if (keyboardState.IsKeyDown(Keys.F1))
            {
                graphics.PreferredBackBufferHeight = 600;
                graphics.PreferredBackBufferWidth = 800;
                graphics.ApplyChanges();
            }
            else if (keyboardState.IsKeyDown(Keys.F2))
            {
                graphics.PreferredBackBufferHeight = 768;
                graphics.PreferredBackBufferWidth = 1024;
                graphics.ApplyChanges();
            }
            else if (keyboardState.IsKeyDown(Keys.F3))
            {
                graphics.PreferredBackBufferHeight = 700;
                graphics.PreferredBackBufferWidth = 1024;
                graphics.ApplyChanges();
            }
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
                    if (hotel.HotelLayout[y, x] is Lobby)
                    {
                        lobby = new Rectangle(x * tegelBreedte, hoogte, 150, 90);
                        spriteBatch.Draw(tegelTextureLijst[hotel.HotelLayout[y, x].TextureCode], new Rectangle(x * tegelBreedte, hoogte, 150, 90), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(tegelTextureLijst[hotel.HotelLayout[y, x].TextureCode], new Rectangle(x * tegelBreedte, hoogte,150,90), Color.White);
                    }
                }
                hoogte = hoogte - 90;
            }

            // Probeer gast Rob te tonen
            gastRob.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
