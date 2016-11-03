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
        public override void LoadContent(ContentManager contentManager)=> Texture = contentManager.Load<Texture2D>(texturepath);

        public override void VoegPersoonToe(Persoon persoon) => inBioscoopLijst.Add((Gast)persoon);

        public override void Update(int verlopenTijdInSeconden)
        {
            if(HuidigEvent.Event == HotelEventAdapter.EventType.START_CINEMA)
            {
                StartCinema(verlopenTijdInSeconden);
                HuidigEvent.Event = HotelEventAdapter.EventType.NONE;
            }
            if (verlopenTijdInSeconden > HuidigEvent.Tijd && HuidigEvent.Tijd != 0)
            {
                StopCinema();
            }
        }

        private void StartCinema(int verlopenTijdInSeconden)
        {
            // Koppel de tijd
            HuidigEvent.Tijd = verlopenTijdInSeconden + (HuidigEvent.Tijd / 60);
            texturepath = @"Kamers\Bioscoop_MetFilm";
        }

        private void StopCinema()
        {
            texturepath = @"Kamers\Bioscoop";
            foreach(Gast gast in inBioscoopLijst)
            {
                if(gast.HuidigEvent.Event != HotelEventAdapter.EventType.EVACUATE)
                {
                    gast.HuidigEvent.Event = HotelEventAdapter.EventType.GOTO_ROOM;
                }
            }
            inBioscoopLijst.Clear();
        }
    }
}
