using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HotelSimulatie.Model
{
    public class Bioscoop : HotelRuimte
    {
        private List<Gast> inBioscoopLijst { get; set; }
        public Bioscoop()
        {
            Naam = "Bioscoop";
            texturepath = @"Kamers\Bioscoop";
            inBioscoopLijst = new List<Gast>();
        }
        public override void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(texturepath);
        }

        public override void VoegPersoonToe(Persoon persoon)
        {
            inBioscoopLijst.Add((Gast)persoon);
        }

        public override void Update(GameTime gameTijd)
        {
            if(HuidigEvent.NEvent == HotelEventAdapter.NEventType.START_CINEMA)
            {
                StartCinema(gameTijd);
                HuidigEvent.NEvent = HotelEventAdapter.NEventType.NONE;
            }
            if (gameTijd.TotalGameTime.Seconds > HuidigEvent.Time && HuidigEvent.Time != 0)
            {
                StopCinema();
            }
        }

        private void StartCinema(GameTime gameTijd)
        {
            // Koppel de tijd
            HuidigEvent.Time = gameTijd.TotalGameTime.Seconds + (HuidigEvent.Time / 60);
            texturepath = @"Kamers\Bioscoop_MetFilm";
        }

        private void StopCinema()
        {
            texturepath = @"Kamers\Bioscoop";
            foreach(Gast gast in inBioscoopLijst)
            {
                gast.HuidigEvent.NEvent = HotelEventAdapter.NEventType.GOTO_ROOM;
            }
            inBioscoopLijst.Clear();
        }
    }
}
