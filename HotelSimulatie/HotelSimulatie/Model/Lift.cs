using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Lift : HotelRuimte
    {
        public int Huidigeverdieping { get; set; }
        public int Volgendeverdieping { get; set; }
        public int Bovensteverdieping { get; set; }
        public Dictionary<Persoon, int> GasteninLift { get; set; }
        public List<int> Bestemmingslijst { get; set;}
        public List<Liftschacht> liftschachtlist { get; set; }
        //private Hotel hotel { get; }
        private Vector2 Positie { get; set; }
        float snelheid;

        public Lift(int Aantalverdiepingen)
        {
            Bestemmingslijst = new List<int>();
            Huidigeverdieping = 0;
            snelheid = 0.5f;
            GasteninLift = new Dictionary<Persoon, int>();
            Bovensteverdieping = Aantalverdiepingen + 1;
        }
        public override void LoadContent(ContentManager contentManager)
        {

        }
        public void UpdateLift(Persoon persoon)
        {
            int? a = null;
            int? b = null;
            foreach(HotelRuimte ruimte in persoon.BestemmingLijst)
            {
                if(ruimte is Liftschacht)
                {
                    if (a == null)
                        a = (int)ruimte.Verdieping;
                    else
                    {
                        b = (int)ruimte.Verdieping;
                        break;
                    }
                        
                }
            }
            // Kijk of de bestemming omhoog of omlaag is
            if((a - b) < 0)
            {
                if (!GasteninLift.ContainsKey(persoon))
                {
                    GasteninLift.Add(persoon, persoon.Bestemming.Verdieping + persoon.BestemmingLijst.OfType<Liftschacht>().Count());
                    Bestemmingslijst.Add(persoon.Bestemming.Verdieping + persoon.BestemmingLijst.OfType<Liftschacht>().Count());
                }
            }
            else if(b == null)
            {
                if (!GasteninLift.ContainsKey(persoon))
                {
                    GasteninLift.Add(persoon, persoon.Bestemming.Verdieping + persoon.BestemmingLijst.OfType<Liftschacht>().Count());
                    Bestemmingslijst.Add(persoon.Bestemming.Verdieping + persoon.BestemmingLijst.OfType<Liftschacht>().Count());
                }
            }
            else
            {
                if (!GasteninLift.ContainsKey(persoon))
                {
                    GasteninLift.Add(persoon, persoon.Bestemming.Verdieping - persoon.BestemmingLijst.OfType<Liftschacht>().Count());
                    Bestemmingslijst.Add(persoon.Bestemming.Verdieping - persoon.BestemmingLijst.OfType<Liftschacht>().Count());
                }
                
            }
            Huidigeverdieping = persoon.HuidigeRuimte.Verdieping;
            GenerateList();
        }
        private void GenerateList()
        {
            // Kijk op welke verdieping er mensen staan te wachten
            foreach (Liftschacht lift in liftschachtlist)
            {
                if (lift.isWachtrij == true && this.Huidigeverdieping != lift.Verdieping)
                {
                    Bestemmingslijst.Add(lift.Verdieping);
                }
            }
            // Sorteer de bestemmingen van de lift van boven naar beneden
            Bestemmingslijst = Bestemmingslijst.OrderBy(i => i).ToList();

            // Kijk wat de volgende bestemming van de lift is
            int j = 0;
            if (Huidigeverdieping != 0)
            {
                for (int i = 0; i < Huidigeverdieping; i++)
                {
                    j = Bestemmingslijst[i];
                }
            }
            else
            {
                for (int i = 0; i < Bestemmingslijst.Count(); i++)
                {
                    j = Bestemmingslijst[i];
                }
            }
            Verplaats(j);
        }
        public void Verplaats(int verdieping)
        {
            Liftschacht volgende = liftschachtlist[verdieping];
            Vector2 verplaatsnaar = volgende.CoordinatenInSpel;

            if (Positie == new Vector2(0, 0))
            {
                Positie = liftschachtlist[Huidigeverdieping].EventCoordinaten;
            }

            if(this.CoordinatenInSpel.Y > volgende.CoordinatenInSpel.Y)
            {
                Positie = new Vector2(Positie.X, Positie.Y + snelheid);
            }
            else
            {
                Positie = new Vector2(Positie.X, Positie.Y - snelheid);
            }
            if ((Int32)this.Positie.Y == volgende.CoordinatenInSpel.Y)
            {
                // aangekomen
                volgende.texturepath = @"Lift\Lift_Open";
                foreach(KeyValuePair<Persoon, int> p in GasteninLift)
                {
                    p.Key.Positie = volgende.EventCoordinaten;
                    if(p.Key.Bestemming.EventCoordinaten.Y == Positie.Y)
                    {
                        p.Key.BestemmingLijst.RemoveAll(o => o is Liftschacht);
                        p.Key.HuidigeRuimte = new Liftschacht(Huidigeverdieping);
                        p.Key.Bestemming = p.Key.BestemmingLijst.First();
                    }
                }

                volgende.LeegWachtrij(volgende.Bestemming);
            }
                
        }

    }
}
