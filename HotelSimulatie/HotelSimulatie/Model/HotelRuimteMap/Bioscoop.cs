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
        private bool filmbezig { get; set; }
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
        public override void voegPersoonToe(Gast gast)
        {
            // Omdat er bij voegPersoonToe geen gameTime doorgegeven kan worden doen wij dit vanuit UpdateEetzaal
            inBioscoopLijst.Add(gast);
        }
        public void Start(GameTime gameTime)
        {
            filmtijd = gameTime.ElapsedGameTime.Seconds;
            filmbezig = true;
            if (gameTime.ElapsedGameTime.Seconds - filmtijd > HotelTijdsEenheid.filmHTE)
                filmbezig = false;
            
        }
        public void Update(GameTime gameTijd)
        {
            int totaleSpelTijd = gameTijd.TotalGameTime.Seconds;
            foreach (Gast gast in inBioscoopLijst)
            { 
                if (gast.HuidigEvent.HuidigeDuurEvent == 0)
                {
                    // In dit geval is er nog geen tijd toegewezen
                    gast.HuidigEvent.HuidigeDuurEvent = totaleSpelTijd;
                }
                else if (totaleSpelTijd - gast.HuidigEvent.HuidigeDuurEvent > HotelTijdsEenheid.bioscoopHTE)
                {
                    gast.HuidigEvent.NEvent = HotelEventAdapter.NEventType.GOTO_ROOM;
                    gast.HuidigEvent.HuidigeDuurEvent = 0;
                }
            }
        }
    }
}
