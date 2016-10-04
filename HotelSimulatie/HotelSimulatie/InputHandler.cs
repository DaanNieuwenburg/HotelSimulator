using HotelSimulatie.View;
using HotelSimulatie.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie
{
    public class InputHandler : Microsoft.Xna.Framework.GameComponent
    {
        public GraphicsDeviceManager graphics { get; set; }
        private Spel spel { get; set; }
        private Hotel hotel { get; set; }
        private KeyboardState keyboardStatus { get; set; }
        private static bool vorigeMuisKlik { get; set; }
        public InputHandler(Game game) : base(game)
        {
            spel = (Spel)game;
            hotel = spel.hotel;
            graphics = spel.graphics;
        }


        public override void Update(GameTime gameTime)
        {
            keyboardStatus = Keyboard.GetState();
            cameraInput();
            lobbyInput();
            resolutieInput();
            base.Update(gameTime);
        }


        private void cameraInput()
        {
            if (keyboardStatus.IsKeyDown(Keys.Up))
            {
                Vector2 nieuweVector = spel.spelCamera.Positie;
                nieuweVector.Y = nieuweVector.Y - 1;
                spel.spelCamera.Beweeg(nieuweVector);
            }
            if (keyboardStatus.IsKeyDown(Keys.Down))
            {
                Vector2 nieuweVector = spel.spelCamera.Positie;
                nieuweVector.Y = nieuweVector.Y + 1;
                spel.spelCamera.Beweeg(nieuweVector);
            }

            // Voor links en rechts
            if (keyboardStatus.IsKeyDown(Keys.Left))
            {
                Vector2 nieuweVector = spel.spelCamera.Positie;
                nieuweVector.X = nieuweVector.X - 1;
                spel.spelCamera.Beweeg(nieuweVector);
            }
            if (keyboardStatus.IsKeyDown(Keys.Right))
            {
                Vector2 nieuweVector = spel.spelCamera.Positie;
                nieuweVector.X = nieuweVector.X + 1;
                spel.spelCamera.Beweeg(nieuweVector);
            }
        }


        private void lobbyInput()
        {
            MouseState muisStatus = Mouse.GetState();
            Vector2 muisLocatie = new Vector2(muisStatus.X, muisStatus.Y);
            muisLocatie = muisLocatie + spel.spelCamera.Positie;
            if (hotel.LobbyRuimte != null)
            {
                if (hotel.LobbyRuimte.LobbyRectangle.Contains(Convert.ToInt32(muisLocatie.X), Convert.ToInt32(muisLocatie.Y)) && muisStatus.LeftButton == ButtonState.Pressed && vorigeMuisKlik == false)
                {
                    vorigeMuisKlik = true;

                    // Open een nieuw scherm met info over het spel
                    LobbyMenu lobbyMenu = new LobbyMenu(hotel);
                    lobbyMenu.ShowDialog();
                    if (lobbyMenu.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                    {
                        vorigeMuisKlik = false;
                    }
                }
            }
        }


        private void resolutieInput()
        {
            if (keyboardStatus.IsKeyDown(Keys.F1))
            {
                graphics.PreferredBackBufferHeight = 600;
                graphics.PreferredBackBufferWidth = 800;
                graphics.ApplyChanges();
            }
            else if (keyboardStatus.IsKeyDown(Keys.F2))
            {
                graphics.PreferredBackBufferHeight = 768;
                graphics.PreferredBackBufferWidth = 1024;
                graphics.ApplyChanges();
            }
            else if (keyboardStatus.IsKeyDown(Keys.F3))
            {
                graphics.PreferredBackBufferHeight = 700;
                graphics.PreferredBackBufferWidth = 1024;
                graphics.ApplyChanges();
            }
        }
    }
}
