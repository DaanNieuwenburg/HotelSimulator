using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;

namespace HotelSimulatie.Model
{
    public abstract class HotelRuimte
    {
        [JsonProperty("ID")]
        public int Code { get; set; }
        public string Naam { get; set; }
        public string texturepath { get; set; }
        [JsonProperty("Dimension")]
        public Vector2 Afmetingen { get; set; }
        
        public int Afstand { get; set; }
        [JsonProperty("Capacity")]
        public int Capaciteit { get; set; }
        public Dictionary<HotelRuimte, int> Buren { get; set; }
        [JsonProperty("Position")]
        public Vector2 CoordinatenInSpel { get; set; }
        public Vector2 EventCoordinaten { get; set; }
        public Texture2D Texture { get; set; }
        public int Verdieping { get; set; }
        public HotelRuimte Vorige { get; set; }

        public HotelRuimte()
        {
            Afstand = Int32.MaxValue / 2;
            Vorige = null;
        }
        public abstract void LoadContent(ContentManager contentManager);
        public void VoegBurenToe(HotelRuimte buur1, HotelRuimte buur2 = null, HotelRuimte buur3 = null)
        {
            Buren = new Dictionary<HotelRuimte, int>();
            if (buur1 != null)
            {
                if (buur1 is Liftschacht || buur1 is Trap)
                {
                    Buren.Add(buur1, 2);
                }
                else
                {
                    Buren.Add(buur1, 1);
                }
            }

            if (buur2 != null)
            {
                if (buur2 is Liftschacht || buur2 is Trap)
                {
                    Buren.Add(buur2, 2);
                }
                else
                {
                    Buren.Add(buur2, 1);
                }
            }

            if (buur3 != null)
            {
                if (buur3 is Liftschacht || buur3 is Trap)
                {
                    Buren.Add(buur3, 2);
                }
                else
                {
                    Buren.Add(buur3, 1);
                }
            }
        }

        public virtual void voegPersoonToe(Gast gast)
        {
        }
    }
}
