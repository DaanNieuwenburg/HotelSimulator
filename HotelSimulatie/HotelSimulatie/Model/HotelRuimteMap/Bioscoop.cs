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
        private int filmtijd { get; set; }
        public bool filmbezig { get; set; }
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
            // Omdat er bij voegPersoonToe geen gameTime doorgegeven kan worden doen wij dit vanuit UpdateEetzaal
            inBioscoopLijst.Add((Gast)persoon);
        }

        public void Start(GameTime gameTime)
        {
            filmtijd = gameTime.TotalGameTime.Seconds;
            filmbezig = true;
            Console.WriteLine("Film is gestart");
        }

        public void Update(GameTime gameTijd)
        {
            int totaleSpelTijd = gameTijd.TotalGameTime.Seconds;
            if (gameTijd.TotalGameTime.Seconds - filmtijd > HotelTijdsEenheid.filmHTE )
            {
                filmbezig = false;
                Console.WriteLine("Film is gestopt");
                foreach (Gast gast in inBioscoopLijst)
                {
                    gast.HuidigEvent.NEvent = HotelEventAdapter.NEventType.GOTO_ROOM;
                    gast.HuidigEvent.HuidigeDuurEvent = 0;
                }
            }        }
    }
}
