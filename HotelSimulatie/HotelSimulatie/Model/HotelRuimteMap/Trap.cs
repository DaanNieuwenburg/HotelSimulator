using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Trap : HotelRuimte
    {
        private Dictionary<Persoon, int> personenInTrap { get; set; }
        public Trap()
        {
            Naam = "Trap";
            texturepath = @"Kamers\Trap_gesloten";
        }
        public override void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(texturepath);
        }

        public void VoegPersoonToe(Persoon persoon)
        {
            personenInTrap.Add(persoon, 0);
        }

        public void Update(GameTime gameTime)
        {
            /*
            foreach(KeyValuePair<Persoon, int> persoon in personenInTrap)
            {
                if(persoon.Value == 0)
                {
                    persoon.Value = gameTime.TotalGameTime.Seconds;
                }
            }*/
        }
    }
}
