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
    public class Trap : HotelRuimte
    {
        private Dictionary<Persoon, List<object>> personenInTrap { get; set; }
        private int verlopenTijd { get; set; }
        public Trap()
        {
            personenInTrap = new Dictionary<Persoon, List<object>>();
            Naam = "Trap";
        }

        public override void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(TexturePad);
        }

        public override void VoegPersoonToe(Persoon persoon)
        {
            if (!personenInTrap.Keys.Contains(persoon))
            {
                // Bepaal hoe lang persoon er over doet om trap op of af te lopen
                int aantalTrappen = Math.Abs(persoon.Bestemming.Verdieping - persoon.HuidigeRuimte.Verdieping);
                // Bepaal bij welke tijd de persoon de trap weer uit gaat, (aantaltrappen * aantaltrappen) zorgt ervoor dat de persoon moe wordt
                int eindTijd = Convert.ToInt32((verlopenTijd) + (aantalTrappen * aantalTrappen));

                List<object> valuesDict = new List<object>();
                valuesDict.Add(persoon.Bestemming);
                valuesDict.Add(eindTijd);
                personenInTrap.Add(persoon, valuesDict);
                persoon.inLiftOfTrap = true;
            }
        }

        public override void Update(int verlopenTijdInSeconden)
        {
            verlopenTijd = verlopenTijdInSeconden;

            // Als persoon klaar is met het oplopen/aflopen van de trap, laat persoon trap uit gaan
            Dictionary<Persoon, List<object>> personenDieTrapUitGaan = (from persoon in personenInTrap
                                                                        where verlopenTijd >= (int)persoon.Value[1]
                                                                        select persoon).ToDictionary(o1 => o1.Key, o2 => o2.Value);
            foreach (KeyValuePair<Persoon, List<object>> persoon in personenDieTrapUitGaan)
            {
                persoon.Key.HuidigeRuimte = (HotelRuimte)persoon.Value[0];

                if(persoon.Key.BestemmingLijst != null)
                {
                    persoon.Key.Bestemming = persoon.Key.BestemmingLijst.First();
                }
                persoon.Key.Positie = persoon.Key.HuidigeRuimte.EventCoordinaten;
                persoon.Key.inLiftOfTrap = false;
                personenInTrap.Remove(persoon.Key);
            }
        }
    }
}