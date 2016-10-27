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
        private bool isIdle { get; set; }
        private int verlopenTijd { get; set; }
        public string texturenaam { get; set; }
        private int startTijd { get; set; }
        public Schoonmaker()
        {
            isIdle = true;
            SchoonmaakLijst = new List<HotelRuimte>();
            Texturelijst = new List<string>();
            Texturelijst.Add(@"AnimatedSchoonmaker1");
            Texturelijst.Add(@"AnimatedSchoonmaker2");
        }

        public override void LoadContent(ContentManager contentManager)
        {
            tempmanager = contentManager;
            if (this.Naam.Contains("Tim"))
            {
                SpriteAnimatie = new GeanimeerdeTexture(contentManager, Texturelijst[1], 3);
            }
            else
            {
                SpriteAnimatie = new GeanimeerdeTexture(contentManager, Texturelijst[0], 3);
            }
        }

        public void Update(GameTime spelTijd)
        {
            // Is de schoonmaker niets aan het doen, kijk dan of er een kamer schoongemaakt moet worden
            if (isIdle == true)
            {
                if (SchoonmaakLijst.Count > 0)
                {
                    Bestemming = SchoonmaakLijst.First();
                    isIdle = false;
                }
            }
            else
            {
                // Als schoonmaker aangekomen is bij kamer
                if (HuidigeRuimte == SchoonmaakLijst.First())
                {
                    if (startTijd == 0)
                    {
                        startTijd = spelTijd.ElapsedGameTime.Seconds;
                    }
                    verlopenTijd = spelTijd.ElapsedGameTime.Seconds;
                    maakRuimteSchoon();
                }
                // Als schoonmaker onderweg is naar kamer
                else
                {
                    HotelRuimte ruimte = SchoonmaakLijst.First();
                    GaNaarRuimte(ref ruimte);
                }
            }
            SpriteAnimatie.UpdateFrame(spelTijd);
        }

        public void VoegRuimteToe(HotelRuimte ruimte)
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

        private void maakRuimteSchoon()
        {
            if (verlopenTijd - startTijd > HotelTijdsEenheid.schoonmakenHTE)
            {
                SchoonmaakLijst.Remove(HuidigeRuimte);
                isIdle = true;
            }
        }
    }
}
