using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Liftschacht : HotelRuimte
    {
        private int Bestemming { get; set; }
        public Lift lift { get; set; }
        public List<Persoon> Wachtrij { get; set; }
        public Liftschacht(int verdieping)
        {
            Naam = "Lift";
            Verdieping = verdieping;
            Wachtrij = new List<Persoon>();

        }
        public override void LoadContent(ContentManager contentManager)
        {
            string texture;

            #region Ken textures toe per verdieping
            //Laad textures voor verschillende verdiepingen
            if (Verdieping == 0)
            {
                if (lift.HuidigeVerdieping.Verdieping == Verdieping)
                    texture = @"Lift\Lift_Beneden_Open";
                else
                    texture = @"Lift\Lift_Beneden";
            }
            else if (Bestemming == lift.BovensteVerdieping)
            {
                if (lift.HuidigeVerdieping.Verdieping == Verdieping)
                    texture = @"Lift\Lift_Bovenste_Open";
                else
                    texture = @"Lift\Lift_Bovenste_Gesloten";
            }
            else
            {
                if (lift.HuidigeVerdieping.Verdieping == Verdieping)
                    texture = @"Lift\Lift_Open";
                else
                    texture = @"Lift\Lift_Gesloten";
            }
            #endregion  

            Texture = contentManager.Load<Texture2D>(texture);
        }

        public override void VoegPersoonToe(Persoon persoon)
        {
            if (!Wachtrij.Contains(persoon))
            {
                Wachtrij.Add(persoon);
                persoon.Wacht = true;
                if (persoon is Gast)
                {
                    persoon.Wachtteller.Start();
                }
                lift.VoegLiftStopToe(persoon, this);
            }
        }
    }
}