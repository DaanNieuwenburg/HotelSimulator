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
        private Lift lift { get; set; }
        public Queue<Gast> Wachtrij { get; set; }
        public Liftschacht(int verdieping)
        {
            Naam = "Lift";
            texturepath = "";
            lift = new Lift();
            Bestemming = verdieping;
            Verdieping = verdieping;
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
        public void UpdateWachtrij(Gast gast)
        {
            Wachtrij.Enqueue(gast);
        }
        public void LeegWachtrij(int verdieping)
        {
            foreach(Gast gast in Wachtrij)
            {
                Gast temp = Wachtrij.Dequeue();
                lift.UpdateLift(temp);
            }
            lift.Verplaats(2);       
        }
    }
}
