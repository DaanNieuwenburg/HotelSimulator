using HotelEvents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Schoonmaker : Persoon
    {
        public Schoonmaker Collega { get; set; }
        public List<HotelRuimte> SchoonmaakLijst { get; set; }
        private int verlopenTijd { get; set; }
        public string texturenaam { get; set; }
        private int startTijd { get; set; }
        public bool InRuimte { get; set; } = false;
        public Schoonmaker()
        {
            SchoonmaakLijst = new List<HotelRuimte>();
            Texturelijst = new List<string>();
            Texturelijst.Add(@"AnimatedSchoonmaker1");
            Texturelijst.Add(@"AnimatedSchoonmaker2");
            HotelEvent evt = new HotelEvent();
            HuidigEvent = new HotelEventAdapter(evt);
            HuidigEvent.Event = HotelEventAdapter.EventType.NONE;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            tempmanager = contentManager;
            if (Naam == "SchoonmakerTim")
            {
                SpriteAnimatie = new GeanimeerdeTexture(contentManager, Texturelijst[0], 3);
            }
            else
            {
                SpriteAnimatie = new GeanimeerdeTexture(contentManager, Texturelijst[1], 3);
            }
        }

        public void Update(GameTime spelTijd)
        {
            verlopenTijd = spelTijd.TotalGameTime.Seconds * (int)HotelEventManager.HTE_Factor;
            // Is de schoonmaker niets aan het doen, kijk dan of er een kamer schoongemaakt moet worden
            if (HuidigEvent.Event == HotelEventAdapter.EventType.NONE)
            {
                if (SchoonmaakLijst.Count > 0)
                {
                    Bestemming = SchoonmaakLijst.First();
                    HuidigEvent.Event = HotelEventAdapter.EventType.GOTO_ROOM;
                }
            }
            else if(HuidigEvent.Event == HotelEventAdapter.EventType.GOTO_ROOM)
            {
                // Als schoonmaker aangekomen is bij kamer
                if (HuidigeRuimte == SchoonmaakLijst.First())
                {
                    startTijd = spelTijd.TotalGameTime.Seconds * (int)HotelEventManager.HTE_Factor + HotelTijdsEenheid.schoonmakenHTE; 
                    HuidigEvent.Event = HotelEventAdapter.EventType.IS_CLEANING;
                    maakRuimteSchoon();
                }
                // Als schoonmaker onderweg is naar kamer
                else
                {
                    HotelRuimte ruimte = SchoonmaakLijst.First();
                    GaNaarRuimte(ref ruimte);
                }
            }
            else
            {
                maakRuimteSchoon();
            }
            SpriteAnimatie.UpdateFrame(spelTijd);
        }

        public void VoegSchoonmaakRuimteToe(HotelRuimte ruimte)
        {
            // Als kamer niet al schoongemaakt wordt
            if (!SchoonmaakLijst.Contains(ruimte) && !Collega.SchoonmaakLijst.Contains(ruimte))
            {
                // Bepaal de dichtbijzijnde schoonmaker
                DijkstraAlgoritme dijkstra = new DijkstraAlgoritme();
                int huidigeSchoonmakerAfstand = dijkstra.MaakAlgoritme(this, HuidigeRuimte, ruimte).Count;
                int collegaSchoonmakerAfstand = dijkstra.MaakAlgoritme(Collega, HuidigeRuimte, ruimte).Count;

                // Als deze schoonmaker dichterbij is dan collega, anders gaat de collega
                if (huidigeSchoonmakerAfstand > collegaSchoonmakerAfstand)
                {
                    SchoonmaakLijst.Add(ruimte);
                }
                else if (huidigeSchoonmakerAfstand < collegaSchoonmakerAfstand)
                {
                    Collega.SchoonmaakLijst.Add(ruimte);
                }
                else
                {
                    // De afstanden zijn gelijk aan elkaar, kies de schoonmaker met de minste opdrachten
                    if (Collega.SchoonmaakLijst.Count > SchoonmaakLijst.Count)
                    {
                        SchoonmaakLijst.Add(ruimte);
                    }
                    else
                    {
                        Collega.SchoonmaakLijst.Add(ruimte);
                    }
                }
            }
        }

        private void maakRuimteSchoon()
        {
            this.InRuimte = true;
            if (verlopenTijd - startTijd > HotelTijdsEenheid.schoonmakenHTE)
            {
                this.InRuimte = false;
                SchoonmaakLijst.Remove(HuidigeRuimte);
                HuidigEvent.Event = HotelEventAdapter.EventType.NONE;
            }
        }
    }
}
