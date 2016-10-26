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
        public Kamer ToegewezenKamer { get; set; }
        public int AantalSterrenKamer { get; set; }
        public bool isDood { get; set; }
        public bool heeftHonger { get; set; }
        public Gast()
        {
            isDood = false;
            Wachtteller = new System.Diagnostics.Stopwatch();
            Texturelijst = new List<string>();
            //Texturelijst.Add(@"Gasten\AnimatedRob");
            Texturelijst.Add(@"Gasten\AnimatedGast1");
            Texturelijst.Add(@"Gasten\AnimatedGast2");
            Texturelijst.Add(@"Gasten\AnimatedGast3");
            Texturelijst.Add(@"Gasten\AnimatedGast4");
            Texturelijst.Add(@"Gasten\AnimatedGast5");
            Texturelijst.Add(@"Gasten\AnimatedGast6");
        }

        public void Inchecken(Lobby lobby, GameTime gameTime)
        {
            // Ga naar lobby en vraag om een kamer
            if (ToegewezenKamer == null)
            {
                if (LoopNaarRuimte())
                {
                    // Voeg gast aan wachtrij toe
                    if (!lobby.Wachtrij.Contains(this))
                    {
                        lobby.Wachtrij.Enqueue(this);
                    }

                    // koppel de toegewezenkammer
                    Kamer gevondenKamer = lobby.GastInChecken(this, gameTime);

                    // Als er een kamer is toegewezen
                    if (gevondenKamer != null)
                    {
                        if (gevondenKamer.AantalSterren == 0)
                        {
                            // Ga uitchecken, gevraagde kamer is niet beschikbaar
                            HuidigEvent.NEvent = HotelEventAdapter.NEventType.CHECK_OUT;
                        }
                        else
                        {
                            Bestemming = gevondenKamer;
                            ToegewezenKamer = gevondenKamer;
                            HuidigEvent.NEvent = HotelEventAdapter.NEventType.GOTO_ROOM;
                        }
                    }
                }
            }
        }
    }
}


