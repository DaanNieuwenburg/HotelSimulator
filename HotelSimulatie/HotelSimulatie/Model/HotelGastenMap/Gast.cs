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
        private Algortime alg { get; set; }

        public Gast()
        {
            Honger = false;
            Wacht = false;
        }

        public void Inchecken(Lobby lobby, GameTime gameTime, Trap tempTesttrap)
        {
            if(LoopNaarRuimte(lobby))
            {
                lobby.Naam = "lobby_Death";
                HotelRuimte kamer = lobby.GastInChecken(this, gameTime, tempTesttrap);
                
                if (kamer != null)
                {
                    alg = new Algortime();
                    List<HotelRuimte> ruimteLijst = alg.MaakAlgoritme(HuidigeRuimte, kamer);

                    lobby.Naam = "lobby_Normaal";
                    
                    LoopNaarRuimte(kamer);
                }
            }
        }
    }
}
