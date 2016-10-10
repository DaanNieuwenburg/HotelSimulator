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
        public Dictionary<Gast, int> GasteninLift { get; set; }
        private Vector2 Positie { get; set; }
        float snelheid;

        public Lift()
        {
            Huidigeverdieping = 2;
            snelheid = 0.5f;
            GasteninLift = new Dictionary<Gast, int>();
        }
        public override void LoadContent(ContentManager contentManager)
        {

        }
        public void UpdateLift(Gast gast)
        {
            int? a = null; // huidige verdieping
            int? b = null; // bestemming
            foreach(HotelRuimte ruimte in gast.BestemmingLijst)
            {
                if(ruimte is Liftschacht)
                {
                    if (a == null)
                        a = (int)ruimte.CoordinatenInSpel.Y;
                    else
                    {
                        b = (int)ruimte.CoordinatenInSpel.Y;
                        break;
                    }
                        
                }
            }
            // Kijk of de bestemming omhoog of omlaag is
            if((a - b) < 0)
            {
                GasteninLift.Add(gast, gast.Bestemming.Verdieping + gast.BestemmingLijst.OfType<Liftschacht>().Count());
            }
            else
            {
                GasteninLift.Add(gast, gast.Bestemming.Verdieping - gast.BestemmingLijst.OfType<Liftschacht>().Count());
            }
            Huidigeverdieping = gast.Bestemming.Verdieping;
        }

        public void Verplaats(int verdieping)
        {
            Liftschacht volgende = new Liftschacht(verdieping);
            Vector2 verplaatsnaar = volgende.CoordinatenInSpel;

            if(this.CoordinatenInSpel.Y < volgende.CoordinatenInSpel.Y)
            {
                Positie = new Vector2(Positie.X, Positie.Y + snelheid);
            }
            else
            {
                Positie = new Vector2(Positie.X, Positie.Y - snelheid);
            }
            if (this.CoordinatenInSpel.Y == volgende.CoordinatenInSpel.Y)
            {
                volgende.LeegWachtrij(volgende.Bestemming);
            }
                
        }

    }
}
