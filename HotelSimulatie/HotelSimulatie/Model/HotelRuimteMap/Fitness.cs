using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Fitness : HotelRuimte
    {
        private List<Gast> inFitnessLijst { get; set; }
        public Fitness()
        {
            inFitnessLijst = new List<Gast>();
            Naam = "Fitness";
            texturepath = @"Kamers\Fitness";
        }
        public override void LoadContent(ContentManager contentManager) => Texture = contentManager.Load<Texture2D>(texturepath);

        public override void VoegPersoonToe(Persoon persoon)
        {
            // Omdat er bij voegPersoonToe geen gameTime doorgegeven kan worden doen wij dit vanuit UpdateEetzaal
            inFitnessLijst.Add((Gast)persoon);
        }

        public override void Update(int verlopenTijdInSeconden)
        {
            foreach (Gast gast in inFitnessLijst)
            {
                if (gast.HuidigEvent.HuidigeDuurEvent == 0)
                {
                    // In dit geval is er nog geen tijd toegewezen
                    gast.HuidigEvent.HuidigeDuurEvent = verlopenTijdInSeconden;
                }
                else if (verlopenTijdInSeconden - gast.HuidigEvent.HuidigeDuurEvent > HotelTijdsEenheid.fitnessHTE)
                {
                    if (gast.HuidigEvent.Event != HotelEventAdapter.EventType.EVACUATE)
                    {
                        gast.HuidigEvent.Event = HotelEventAdapter.EventType.GOTO_ROOM;
                    }
                    gast.HuidigEvent.HuidigeDuurEvent = 0;
                }
            }
        }
    }
}
