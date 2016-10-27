using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Trappenhuis : HotelRuimte
    {
        public Trap trap { get; set; }
        public Trappenhuis()
        {
            // Test
            Naam = "Trap";
            texturepath = @"Kamers\Trap_gesloten";
        }
        public override void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(texturepath);
        }

        public void VoegPersoonToe(Persoon persoon)
        {
            trap.VoegPersoonToe(persoon);
        }
    }
}
