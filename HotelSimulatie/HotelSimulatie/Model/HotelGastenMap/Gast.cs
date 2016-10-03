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

        public void Inchecken(Lobby lobby, GameTime gameTime, Kamer tempTestKamer)
        {
            Bestemming = lobby;
            if(LoopNaarRuimte())
            {
                lobby.Naam = "lobby_Death";
                HotelRuimte kamer = lobby.GastInChecken(this, gameTime, tempTestKamer);
                
                if (kamer != null)
                {
                    alg = new Algortime();
                    Bestemminglijst = alg.MaakAlgoritme(HuidigeRuimte, kamer);

                    lobby.Naam = "lobby_Normaal";

                    Bestemming = kamer;
                    LoopNaarRuimte();
                }
            }
        }
    }
}
