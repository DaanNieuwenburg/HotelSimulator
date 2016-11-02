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

            #region Ken textures toe per verdieping
            //Laad textures voor verschillende verdiepingen
            if (Bestemming == 0)
            {
                if (lift.HuidigeVerdieping.Verdieping == Bestemming)
                    texture = @"Lift\Lift_Beneden_Open";
                else
                    texture = @"Lift\Lift_Beneden";
            }
            else if (Bestemming == lift.BovensteVerdieping)
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

        public override void VoegPersoonToe(Persoon persoon)
        {
            if (!Wachtrij.Contains(persoon))
            {
                Wachtrij.Enqueue(persoon);
                persoon.Wacht = true;
                if (persoon is Gast)
                {
                    persoon.Wachtteller.Start();
                }
                isWachtrij = true;
                lift.VoegLiftStopToe(this);
            }
        }

        public void LaatPersoonLiftInGaan()
        {
            int personenInWachtrij = Wachtrij.Count();
            for (int i = 0; i < personenInWachtrij; i++)
            {
                Persoon persoon = Wachtrij.Dequeue();
                persoon.Wacht = false;
                persoon.inLiftOfTrap = true;
                if (persoon is Gast)
                {
                    persoon.Wachtteller.Stop();
                    persoon.Wachtteller.Reset();
                }

                lift.PersonenInLift.Add(persoon);
                Liftschacht test = (Liftschacht)persoon.Bestemming;
                if (test != null)
                {
                    lift.VoegLiftStopToe(test);
                }
            }
        }

        public void LaatPersonenUitLiftGaan(List<Persoon> personenDieUitstappen)
        {
            foreach (Persoon persoon in personenDieUitstappen)
            {
                persoon.HuidigeRuimte = persoon.Bestemming;
                persoon.Bestemming = persoon.BestemmingLijst.First();
                persoon.BestemmingLijst.Remove(persoon.Bestemming);
                persoon.inLiftOfTrap = false;
                persoon.Positie = EventCoordinaten;
                lift.PersonenInLift.Remove(persoon);
            }
        }
    }
}