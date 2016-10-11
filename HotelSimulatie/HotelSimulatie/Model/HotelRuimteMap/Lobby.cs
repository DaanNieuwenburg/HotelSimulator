using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Lobby : HotelRuimte
    {
        public Rectangle LobbyRectangle { get; set; }
        public Queue<Gast> Wachtrij { get; set; }
        public Hotel hotel { get; set; }
        private float verlopenTijd { get; set; }
        public Lobby()
        {
            EventCoordinaten = new Vector2(LobbyRectangle.Left + 80, LobbyRectangle.Bottom);
            Wachtrij = new Queue<Gast>();
            Naam = "Lobby";
            texturepath = @"Kamers\lobby_Normaal";
        }

        public override void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(texturepath);
        }

        public Kamer GastInChecken(Gast gast, GameTime gameTime)
        {
            verlopenTijd += gameTime.ElapsedGameTime.Milliseconds;
            if (verlopenTijd > 100)
            {
                Gast gastAanDeBeurt = Wachtrij.Dequeue();

                // Geef een beschikbare kamer
                Kamer toegewezenKamer = null;
                try
                {
                    // Zoekt een beschikbare kamer en bij geen ga telkens 1 ster omhoog
                    while (toegewezenKamer == null && gast.HuidigEvent.aantalSterrenKamer <= 5)
                    {
                        toegewezenKamer = hotel.KamerLijst.First(o => o.Bezet == false && o.AantalSterren == gast.HuidigEvent.aantalSterrenKamer);
                        gast.HuidigEvent.aantalSterrenKamer++;
                    }
                    toegewezenKamer.Bezet = true;
                }
                catch(InvalidOperationException e)
                {
                    // Als er geen kamer beschikbaar is, return kamer van 0 sterren
                    Console.WriteLine("Checkout");
                    toegewezenKamer = new Kamer(0);
                }
                verlopenTijd = 0;
                return toegewezenKamer;
            }
            else
            {
                if(!Wachtrij.Contains(gast))
                {
                    Wachtrij.Enqueue(gast);
                }
            }
            return null;
        }

        public void GastUitchecken(Gast gast)
        {
            Kamer gastKamer = hotel.KamerLijst.Find(o => o.Kamernummer == gast.Kamernummer);
            gastKamer.Bezet = false;
            gast.Kamernummer = null;
            hotel.GastenLijst.Remove(gast);
        }
    }
}
