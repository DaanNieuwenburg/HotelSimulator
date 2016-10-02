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
       
        public Texture Texturelijst { get; set; }

        public Gast()
        {
            Honger = false;
            Wacht = false;
        }
    }
}
