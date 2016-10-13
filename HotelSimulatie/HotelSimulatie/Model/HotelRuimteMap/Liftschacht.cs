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
                if (lift.HuidigeVerdieping.Verdieping == Bestemming)
                    texture = @"Lift\Lift_Beneden_Open";
                else
                    texture = @"Lift\Lift_Beneden";
            }
            else
            {
                if (lift.HuidigeVerdieping.Verdieping == Bestemming)
                    texture = @"Lift\Lift_Open";
                else
                    texture = @"Lift\Lift_Gesloten";
            }

            Texture = contentManager.Load<Texture2D>(texture);
        }

        public bool VraagOmLift(Persoon persoon)
        {
            bool komtAl = true;
            if (!Wachtrij.Contains(persoon))
            {
                Wachtrij.Enqueue(persoon);
                isWachtrij = true;
                lift.VoegLiftStopToe(this);
                komtAl = false;
            }
            return komtAl;
        }

        public void LaatGastenLiftInGaan()
        {
            //if (lift.BovensteLiftschachtBereikt == true || lift.HuidigeVerdieping == lift.Liftschachtlijst[0])
                int a = Wachtrij.Count();
                for (int i = 0; i < a; i++)
                {
                    Persoon persoon = Wachtrij.Dequeue();
                    persoon.inLift = true;
                    persoon.Bestemming = persoon.BestemmingLijst.OfType<Liftschacht>().Last();
                    persoon.BestemmingLijst.RemoveAll(o => o is Liftschacht);
                    lift.GasteninLift.Add(persoon);

                    // Voegt de verdieping van de personen aan de lijst toe
                   // lift.VoegLiftStopToe(temp.bestemmingslift);
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
