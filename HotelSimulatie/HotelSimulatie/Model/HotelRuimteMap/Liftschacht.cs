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
            //Laad textures voor verschillende verdiepingen
            #region 
            if (Bestemming == 0)
            {
                if (lift.HuidigeVerdieping.Verdieping == Bestemming)
                    texture = @"Lift\Lift_Beneden_Open";
                else
                    texture = @"Lift\Lift_Beneden";
            }
            else if(Bestemming == lift.BovensteVerdieping)
            {
                if (lift.HuidigeVerdieping.Verdieping == Bestemming)
                    texture = @"Lift\Lift_Bovenste_Open";
                else
                    texture = @"Lift\Lift_Bovenste_Gesloten";
            }
            else
            {
                if (lift.HuidigeVerdieping.Verdieping == Bestemming)
                    texture = @"Lift\Lift_Open";
                else
                    texture = @"Lift\Lift_Gesloten";
            }
            #endregion  

            Texture = contentManager.Load<Texture2D>(texture);
        }

        public void VraagOmLift(Persoon persoon)
        {
            if (!Wachtrij.Contains(persoon))
            {
                Wachtrij.Enqueue(persoon);
                isWachtrij = true;
                lift.VoegLiftStopToe(this);
            }
        }

        public void LaatGastenLiftInGaan()
        {
            if (lift.BovensteLiftschachtBereikt == true || lift.HuidigeVerdieping == lift.Liftschachtlijst[0])
            {
                int a = Wachtrij.Count();
                for (int i = 0; i < a; i++)
                {
                    Persoon temp = Wachtrij.Dequeue();
                    lift.GasteninLift.Add(temp);
                    //temp.Bestemming = null;
                    Console.WriteLine("Laat " + temp.Naam + " LiftInGaan op verdieping " + this.Verdieping);
                    // Voegt de verdieping van de personen aan de lijst toe
                    lift.VoegLiftStopToe(temp.bestemmingslift);
                }
                isWachtrij = false;
            }
        }
        public void LaatGastenUitLiftGaan()
        {
            for (int i = 0; i < lift.GasteninLift.Count(); i++)
            {
                Persoon temp = lift.GasteninLift[i];
                if(temp.bestemmingslift == this)
                {
                    lift.GasteninLift.Remove(temp);
                    temp.bestemmingslift = null;
                }
            }
        }
    }
}
