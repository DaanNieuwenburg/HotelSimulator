using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace HotelSimulatie.Model
{
    public class LiftTDD : HotelRuimte
    {
        public Liftschacht HuidigeVerdieping { get; set; }
        private Liftschacht LiftBestemming { get; set; }
        private int BovensteVerdieping { get; set; }
        private bool LiftOmhoog { get; set; }
        private List<Persoon> GasteninLift { get; set; }
        private List<Liftschacht> LiftStoppenlijst { get; set; }
        public List<Liftschacht> Liftschachtlijst { get; set; }
        private float snelheid { get; set; }

        public LiftTDD(int aantalVerdiepingen)
        {
            HuidigeVerdieping = Liftschachtlijst.First();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            throw new NotImplementedException();
        }
    }
}
