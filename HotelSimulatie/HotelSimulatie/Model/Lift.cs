using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelSimulatie.Model
{
    public class Lift : HotelRuimte
    {
        public int Huidigeverdieping { get; set; }
        public int Volgendeverdieping { get; set; }
        public int Bovensteverdieping { get; set; }
        public Dictionary<Persoon, int> GasteninLift { get; set; }
        public List<int> Bestemmingslijst { get; set; }
        public List<Liftschacht> liftschachtlist { get; set; }
        //private Hotel hotel { get; }
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
            foreach (HotelRuimte ruimte in persoon.BestemmingLijst)
            {
                if (ruimte is Liftschacht)
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
            if ((a - b) < 0)
            {
                if (!GasteninLift.ContainsKey(persoon))
                {
                    GasteninLift.Add(persoon, persoon.Bestemming.Verdieping + persoon.BestemmingLijst.OfType<Liftschacht>().Count());
                    Bestemmingslijst.Add(persoon.Bestemming.Verdieping + persoon.BestemmingLijst.OfType<Liftschacht>().Count());
                }
            }
            else if (b == null)
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
            Bestemmingslijst.Sort((a, b) => -1 * a.CompareTo(b));
            // Kijk wat de volgende bestemming van de lift is
            int j = 0;
            /*if (Huidigeverdieping != 0)
            {
                for (int i = 0; i < Huidigeverdieping; i++)
                {
                    j = Bestemmingslijst[i];
                }
            }
            else
            {*/
                for (int i = 0; i < Bestemmingslijst.Count(); i++)
                {
                    j = Bestemmingslijst[i];
                }
            //}
            Huidigeverdieping = j;
            //System.Diagnostics.Debugger.Break();
            Verplaats(liftschachtlist[j]);
        }
        public void Verplaats(Liftschacht volgendeBestemming)
        {

            if (EventCoordinaten == new Vector2(0,0))
            {
                EventCoordinaten = liftschachtlist[Huidigeverdieping].EventCoordinaten;
            }

            if ((Int32)this.EventCoordinaten.Y != volgendeBestemming.EventCoordinaten.Y)
            {
                if (EventCoordinaten.Y < volgendeBestemming.EventCoordinaten.Y)
                {
                    EventCoordinaten = new Vector2(EventCoordinaten.X, EventCoordinaten.Y + snelheid);
            }
            else
            {
                    EventCoordinaten = new Vector2(EventCoordinaten.X, EventCoordinaten.Y - snelheid);
                }
            }
            else if(Bestemmingslijst.Count > 0)
            {
                // aangekomen
                if (volgendeBestemming.Verdieping == 0)
                    volgendeBestemming.texturepath = @"Lift\Lift_Beneden_Open";
                else
                volgendeBestemming.texturepath = @"Lift\Lift_Open";

                foreach (KeyValuePair<Persoon, int> p in GasteninLift)
                {
                    p.Key.Positie = volgendeBestemming.EventCoordinaten;
                    if(volgendeBestemming.EventCoordinaten == EventCoordinaten)
                    {
                        p.Key.HuidigeRuimte = p.Key.BestemmingLijst.OfType<Liftschacht>().Last();
                        p.Key.BestemmingLijst.RemoveAll(o => o.Naam == "Lift");
                        p.Key.Bestemming = p.Key.BestemmingLijst.First();
                    }
                }
                Bestemmingslijst.RemoveAll(o => o == volgendeBestemming.Verdieping);
                if (volgendeBestemming.Wachtrij.Count() > 0)
                volgendeBestemming.LeegWachtrij(volgendeBestemming.Bestemming);
                else
                    GenerateList();
            }
            else
            {
                volgendeBestemming.texturepath = @"Lift\Lift_Open";
                Huidigeverdieping = 0;
            }
        }

    }
}

