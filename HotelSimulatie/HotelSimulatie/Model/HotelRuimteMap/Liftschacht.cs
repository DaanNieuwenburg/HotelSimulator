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
        public int Bestemming { get; set; }
        public int Positie { get; set; }
        public int AantalPersonen { get; set; }
        public bool isWachtrij { get; set; }
        public Lift lift { get; set; }
        public Queue<Persoon> Wachtrij { get; set; }
        public Liftschacht(int verdieping)
        {
            Naam = "Lift";
            texturepath = "";
            Bestemming = verdieping;
            Verdieping = verdieping;
            Wachtrij = new Queue<Persoon>();
            isWachtrij = false;

        }
        public override void LoadContent(ContentManager contentManager)
        {
            string texture;
            if (Bestemming == 0)
            {
                if (lift.Huidigeverdieping == Bestemming)
                    texture = @"Lift\Lift_Beneden_Open";
                else
                    texture = @"Lift\Lift_Beneden";
            }
            else
            {
                if (lift.Huidigeverdieping == Bestemming)
                    texture = @"Lift\Lift_Open";
                else
                    texture = @"Lift\Lift_Gesloten";
            }
                
            Texture = contentManager.Load<Texture2D>(texture);
        }
        public void UpdateWachtrij(Persoon persoon)
        {
            Wachtrij.Enqueue(persoon);
            isWachtrij = true;
            if(lift.Huidigeverdieping == this.Verdieping)
            {
                LeegWachtrij(this.Verdieping);
            }
        }
        public void LeegWachtrij(int verdieping)
        {
            for (int i = 0; i < Wachtrij.Count(); i++)
            {
                Persoon temp = Wachtrij.Dequeue();
                lift.UpdateLift(temp);
            }
            isWachtrij = false;
        }
    }
}
