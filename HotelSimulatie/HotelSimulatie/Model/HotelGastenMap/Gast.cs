using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HotelSimulatie.Model
{
    public class Gast : Persoon
    {
        public int Gastnummer { get; set; }
        public bool Honger { get; set; }
        public int? Kamernummer { get; set; }
        public bool Wacht { get; set; }

        public Gast()
        {
            Honger = false;
            Wacht = false;
        }

        public void Inchecken(Lobby lobby, GameTime gameTime, Lift tempTestLift)
        {
            BestemmingBereikt = LoopNaarRuimte(lobby);
            if(BestemmingBereikt == true)
            {
                HotelRuimte kamer = lobby.GastInChecken(this, gameTime, tempTestLift);
                if(kamer != null)
                {
                    Algortime alg = new Algortime();
                    List<HotelRuimte> ruimteLijst = alg.MaakAlgoritme(HuidigeRuimte, kamer);

                    LoopNaarRuimte(kamer);
                }
            }
        }
    }
}
