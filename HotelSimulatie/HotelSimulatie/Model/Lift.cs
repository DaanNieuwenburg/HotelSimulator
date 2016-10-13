using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
        float snelheid;

        public Lift(int Aantalverdiepingen)
        {
            Naam = "Lift";
            texturepath = "Naamloos";
            Bestemmingslijst = new List<int>();
            Huidigeverdieping = 0;
            snelheid = 0.1f;
            GasteninLift = new Dictionary<Persoon, int>();
            Bovensteverdieping = Aantalverdiepingen + 1;
        }

        public override void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(texturepath);
        }

        public void UpdateLift(List<Persoon> persoonlist)
        {
            int? a = null;
            int? b = null;
            foreach (Persoon persoon in persoonlist)
            {
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
            }
            if(liftschachtlist[Huidigeverdieping].Wachtrij.Count <= 0)
                GenerateList();
        }
        public void GenerateList()
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
            
            // Zorg dat elke bestemming maar 1x in de lijst staan om duplicaten te voorkomen
            Bestemmingslijst = Bestemmingslijst.Distinct().ToList();

            // Kijk wat de volgende bestemming van de lift is
            int j = 0;
            for (int i = 0; i < Bestemmingslijst.Count(); i++)
            {
                j = Bestemmingslijst[i];
            }

            Volgendeverdieping = j;
            //System.Diagnostics.Debugger.Break();

            Console.WriteLine("Verplaats lift naar " + j);
            /*if (Huidigeverdieping == 0)
            {
                Verplaats(liftschachtlist.Last());
            }
            else
            {*/
                Verplaats(liftschachtlist[j]);
            //}
        }
        public void Verplaats(Liftschacht volgendeBestemming)
        {
            if (EventCoordinaten == new Vector2(0, 0))
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
            //else if (Bestemmingslijst.Count > 0)
            else
            {
                {
                    Huidigeverdieping = Volgendeverdieping;
                    // aangekomen
                    if (volgendeBestemming.Verdieping == 0)
                        volgendeBestemming.texturepath = @"Lift\Lift_Beneden_Open";
                    else
                        volgendeBestemming.texturepath = @"Lift\Lift_Open";

                    for (int i = 0; i < GasteninLift.Count; i++)
                    {
                        var p = GasteninLift.ElementAt(i);
                        p.Key.Positie = volgendeBestemming.EventCoordinaten;
                        if (p.Key.Positie == EventCoordinaten)
                        {
                            if (p.Key.BestemmingLijst.OfType<Liftschacht>().Count() > 0)
                            {
                                p.Key.HuidigeRuimte = p.Key.BestemmingLijst.OfType<Liftschacht>().Last();
                                p.Key.BestemmingLijst.Remove(p.Key.BestemmingLijst.First());
                                p.Key.Bestemming = p.Key.BestemmingLijst.First();
                                GasteninLift.Remove(p.Key);
                            }
                        }
                    }

                    Bestemmingslijst.Remove(volgendeBestemming.Verdieping);

                    if (volgendeBestemming.Wachtrij.Count() > 0)
                        liftschachtlist[Huidigeverdieping].LeegWachtrij(volgendeBestemming.Bestemming);
                    else
                        GenerateList();
                }
            }
        }
    }
}

