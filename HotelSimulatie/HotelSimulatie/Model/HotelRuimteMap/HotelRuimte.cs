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
        public int Afstand { get; set; }
        public int Capaciteit { get; set; }
        public Dictionary<HotelRuimte, int> Buren { get; set; }
        public Vector2 CoordinatenInSpel { get; set; }
        public Vector2 EventCoordinaten { get; set; }
        public Texture2D Texture { get; set; }
        public HotelRuimte Vorige { get; set; }

        public HotelRuimte()
        {
            Vorige = null;
        }
        public abstract void LoadContent(ContentManager contentManager);
        public void VoegBurenToe(HotelRuimte buur1, HotelRuimte buur2 = null)
        {
            Buren = new Dictionary<HotelRuimte, int>();
            if(buur1 is Lift || buur1 is Trap)
                Buren.Add(buur1, 2);
            else
                Buren.Add(buur1, 1);
            if(buur2 != null)
            {
                if (buur2 is Lift || buur2 is Trap)
                    Buren.Add(buur2, 2);
                else
                    Buren.Add(buur2, 1);
            }
        }
    }
}
