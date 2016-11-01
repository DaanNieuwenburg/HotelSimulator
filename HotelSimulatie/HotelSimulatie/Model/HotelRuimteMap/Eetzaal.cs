using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Eetzaal : HotelRuimte
    {
        private List<Gast> inEetzaalLijst { get; set; }
        public static int MaxAantalGasten { get; set; } = 20;
        private Queue<Gast> Wachtrij { get; set; }
        public Eetzaal()
        {
            Naam = "Eetzaal";
            texturepath = @"Kamers\Eetzaal";
            inEetzaalLijst = new List<Gast>();
            Wachtrij = new Queue<Gast>();
        }
        public override void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(texturepath);
        }

        public override void VoegPersoonToe(Persoon persoon)
        {
            if(inEetzaalLijst.Count < MaxAantalGasten)
            {
                // Omdat er bij voegPersoonToe geen gameTime doorgegeven kan worden doen wij dit vanuit UpdateEetzaal
                inEetzaalLijst.Add((Gast)persoon);
            }
            else
            {
                Wachtrij.Enqueue((Gast)persoon);
                persoon.Wachtteller.Start();
            }
            
        }

        public override void Update(GameTime gameTijd)
        {
            if(inEetzaalLijst.Count < MaxAantalGasten && Wachtrij.Count > 0)
            {
                Gast temp = Wachtrij.Dequeue();
                if (temp.isDood == false)
                {
                    temp.Wachtteller.Reset();
                    inEetzaalLijst.Add(temp);
                }  
            }
            int totaleSpelTijd = gameTijd.TotalGameTime.Seconds;
            foreach (Gast gast in inEetzaalLijst)
            {
                if (gast.HuidigEvent.HuidigeDuurEvent == 0)
                {
                    // In dit geval is er nog geen tijd toegewezen
                    gast.HuidigEvent.HuidigeDuurEvent = totaleSpelTijd;
                }
                else if(totaleSpelTijd - gast.HuidigEvent.HuidigeDuurEvent > HotelTijdsEenheid.eetzaalHTE)
                {
                    gast.HuidigEvent.Event = HotelEventAdapter.EventType.GOTO_ROOM;
                    gast.HuidigEvent.HuidigeDuurEvent = 0;
                }
            }
        }
    }
}
