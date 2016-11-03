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
using HotelEvents;
using System.Windows.Forms;
using System.Diagnostics;

namespace HotelSimulatie
{
    public class Simulatie : Game
    {
        public GraphicsDeviceManager graphics { get; set; }
        private SpriteBatch spriteBatch { get; set; }
        public Hotel hotel { get; set; }
        private int HTEtijd { get; set; }
        public Vector2 GastSpawnLocatie { get; set; }
        public Camera spelCamera { get; set; }
        public Matrix matrix { get; set; }
        public SpriteFont font { get; set; }

        private Texture2D godzillaTexture { get; set; }
        private bool GodzillaEvent { get; set; }
        public Simulatie(Hotel _hotel)
        {
            graphics = new GraphicsDeviceManager(this);
            Window.Title = "Hotel Simulator";
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            hotel = _hotel;
            spelCamera = new Camera(540, 750);
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
            Components.Add(new HotelEventListener(this));
            Components.Add(new AIHandler(this));
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Font");

            // Laad de schoonmakers
            hotel.PersonenInHotelLijst.OfType<Schoonmaker>().First().LoadContent(Content);
            hotel.PersonenInHotelLijst.OfType<Schoonmaker>().Last().LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.F1))
            {
                graphics.ToggleFullScreen();
            }
            HTEtijd = Convert.ToInt32(gameTime.TotalGameTime.TotalSeconds * HotelEventManager.HTE_Factor);

            if (hotel.huidigEvent.Event == HotelEventAdapter.EventType.GODZILLA)
            {
                GodzillaEvent = true;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            matrix = Matrix.CreateTranslation(new Vector3(0, 40, 0));
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, spelCamera.TransformeerMatrix(GraphicsDevice));


            // Zet de tijd neer
            int tijd = 0;
            if (gameTime.TotalGameTime.Seconds > 59)
            {
                tijd = tijd + 60;
            }
            spriteBatch.DrawString(font, "Tijd: " + (HTEtijd/* + gameTime.TotalGameTime.Seconds * HotelEventManager.HTE_Factor*/) + " HTE", new Vector2(0, 700), Color.Red);

            foreach (HotelRuimte hotelRuimte in hotel.hotelLayout.HotelRuimteLijst)
            {
                hotelRuimte.LoadContent(Content);

                // Tekent de textures op het scherm
                spriteBatch.Draw(hotelRuimte.Texture, new Rectangle((Int32)hotelRuimte.CoordinatenInSpel.X, (Int32)hotelRuimte.CoordinatenInSpel.Y, (Int32)hotelRuimte.Afmetingen.X, (Int32)hotelRuimte.Afmetingen.Y), Color.White);

                // Zet de coordinaten van de hotelruimte goed
                hotelRuimte.CoordinatenInSpel = new Vector2((Int32)hotelRuimte.CoordinatenInSpel.X, (Int32)hotelRuimte.CoordinatenInSpel.Y);
                hotelRuimte.EventCoordinaten = new Vector2((Int32)hotelRuimte.CoordinatenInSpel.X, (Int32)hotelRuimte.CoordinatenInSpel.Y);

                // Zet overige posities goed
                if (hotelRuimte is Lobby)
                {
                    hotel.hotelLayout.lobby = (Lobby)hotelRuimte;
                    hotel.hotelLayout.lobby.hotel = hotel;
                    hotel.hotelLayout.lobby.LobbyRectangle = new Rectangle((Int32)hotelRuimte.CoordinatenInSpel.X, (Int32)hotelRuimte.CoordinatenInSpel.Y, (Int32)hotelRuimte.Afmetingen.X, (Int32)hotelRuimte.Afmetingen.Y);
                    GastSpawnLocatie = new Vector2((Int32)hotelRuimte.CoordinatenInSpel.X, (Int32)hotelRuimte.CoordinatenInSpel.Y + 20);
                    hotel.hotelLayout.lobby.EventCoordinaten = new Vector2((Int32)hotelRuimte.CoordinatenInSpel.X + 10, (Int32)hotelRuimte.CoordinatenInSpel.Y + 20);
                }
                if (hotelRuimte is Liftschacht)
                {
                    hotelRuimte.EventCoordinaten = new Vector2((Int32)hotelRuimte.CoordinatenInSpel.X + 45, (Int32)hotelRuimte.CoordinatenInSpel.Y + 20);
                }
                if (hotelRuimte is Trappenhuis)
                {
                    hotelRuimte.EventCoordinaten = new Vector2((Int32)hotelRuimte.CoordinatenInSpel.X + 45, (Int32)hotelRuimte.CoordinatenInSpel.Y + 20);
                }
                if (hotelRuimte is Bioscoop)
                {
                    hotelRuimte.EventCoordinaten = new Vector2((Int32)hotelRuimte.CoordinatenInSpel.X + 80, (Int32)hotelRuimte.CoordinatenInSpel.Y);
                }
            }
            if (GodzillaEvent == true)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("Godzilla"), Vector2.Zero, Color.White);

                Stopwatch Timer = new Stopwatch();
                Timer.Start();
                if (hotel.huidigEvent.Tijd == 0)
                {
                    hotel.huidigEvent.Tijd = gameTime.TotalGameTime.Seconds + 3;
                }
                if (gameTime.TotalGameTime.Seconds > hotel.huidigEvent.Tijd)
                    if (MessageBox.Show("Godzilla is terrorising your hotel.\n SIMULATION OVER") == DialogResult.OK)
                    {
                        Environment.Exit(0);
                    }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}