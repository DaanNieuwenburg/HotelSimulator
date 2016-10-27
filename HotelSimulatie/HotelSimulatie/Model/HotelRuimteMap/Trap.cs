using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Trap : HotelRuimte
    {
        private Dictionary<Persoon, int> personenInTrap { get; set; }
        public Trap()
        {
            personenInTrap = new Dictionary<Persoon, int>();
        }
        public override void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(texturepath);
        }
        public void VoegPersoonToe(Persoon persoon)
        {
            if (!personenInTrap.Keys.Contains(persoon))
            {
                personenInTrap.Add(persoon, 0);
                persoon.inLiftOfTrap = true;
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < personenInTrap.Count; i++)
            {
                KeyValuePair<Persoon, int> persoon = personenInTrap.ElementAt(i);

                // Bepaal wanneer persoon weer trap uit moet
                if (persoon.Value == 0)
                {
                    int aantalTrappen = persoon.Key.BestemmingLijst.OfType<Trappenhuis>().Count();
                    int eindTijd = gameTime.TotalGameTime.Seconds + aantalTrappen;
                    // Bepaal aantal verdiepingen die de persoon op moet
                    personenInTrap[persoon.Key] = eindTijd;

                    // Verwijder trappenhuis en zet de laatste trappenhuis als bestemming
                    persoon.Key.Bestemming = persoon.Key.BestemmingLijst.OfType<Trappenhuis>().Last();
                    persoon.Key.BestemmingLijst.RemoveAll(o => o is Trappenhuis);
                }
            }

            // Als persoon zijn wacht hte is gelijk aan de verlopen hte
            List<Persoon> personenDieTrapUitGaan = new List<Persoon>();
            foreach (KeyValuePair<Persoon, int> persoon in personenInTrap)
            {
                if (persoon.Value == gameTime.TotalGameTime.Seconds)
                {
                    persoon.Key.HuidigeRuimte = persoon.Key.Bestemming;
                    persoon.Key.Bestemming = persoon.Key.BestemmingLijst.First();
                    persoon.Key.Positie = persoon.Key.HuidigeRuimte.EventCoordinaten;
                    persoon.Key.inLiftOfTrap = false;
                    personenDieTrapUitGaan.Add(persoon.Key);
                }
            }

            // Verwijder personen die de trap uit gaan
            foreach(Persoon persoon in personenDieTrapUitGaan)
            {
                personenInTrap.Remove(persoon);
            }
        }
    }
}
