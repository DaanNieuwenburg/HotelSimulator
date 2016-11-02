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
        public HotelEventAdapter HuidigEvent { get; set; }
        public string Naam { get; set; }
        public string texturepath { get; set; }
        [JsonProperty("Dimension")]
        public Vector2 Afmetingen { get; set; }
        
        public int Afstand { get; set; }
        public int Gewicht { get; set; }
        [JsonProperty("Capacity")]
        public int Capaciteit { get; set; }
        public List<HotelRuimte> Buren { get; set; }
        [JsonProperty("Position")]
        public Vector2 CoordinatenInSpel { get; set; }
        public Vector2 EventCoordinaten { get; set; }
        public Texture2D Texture { get; set; }
        public int Verdieping { get; set; }
        public HotelRuimte Vorige { get; set; }

        public HotelRuimte()
        {
            HuidigEvent = new HotelEventAdapter(new HotelEvents.HotelEvent { EventType = HotelEvents.HotelEventType.NONE });
            Afstand = Int32.MaxValue / 2;

            // Bepaal gewicht, is afhankelijk van de 
            Gewicht = 1;
            Vorige = null;
        }
        public abstract void LoadContent(ContentManager contentManager);
        public void VoegBurenToe(List<HotelRuimte> burenLijst) => Buren = burenLijst;

        public virtual void Update(GameTime gameTime)
        {

        }

        public void UpdateGewicht()
        {
            if(Afmetingen.X > 1)
            {
                Gewicht = (int)Afmetingen.X;
            }
        }


        public virtual void VoegPersoonToe(Persoon persoon)
        {
        }
    }
}
