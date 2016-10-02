using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace HotelSimulatie.Model
{
    public abstract class HotelRuimte
    {
        public string Naam { get; set; }
        public int Hoogte { get; set; }
        public int Breedte { get; set; }
        public int Capaciteit { get; set; }
        public HotelRuimte[] Buren { get; set; }
        public Vector2 CoordinatenInSpel { get; set; }
        public Vector2 EventCoordinaten { get; set; }
        public Texture2D Texture { get; set; }

        public abstract void LoadContent(ContentManager contentManager);
        public void VoegBurenToe(HotelRuimte buur1, HotelRuimte buur2 = null)
        {
            Buren = new HotelRuimte[2];
            Buren[0] = buur1;
            Buren[1] = buur2;
        }
    }
}
