using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEvents;
using System.Diagnostics;

namespace HotelSimulatie.Model
{
    public abstract class Persoon
    {
        public HotelRuimte Bestemming { get; set; }
        public List<HotelRuimte> BestemmingLijst { get; set; }
        public HotelEventAdapter HuidigEvent { get; set; }
        public HotelRuimte HuidigeRuimte { get; set; }
        public Liftschacht bestemmingslift { get; set; }
        public Vector2 Positie { get; set; }
        public GeanimeerdeTexture SpriteAnimatie { get; set; }
        public virtual List<string> Texturelijst { get; set; }
        private ContentManager tempmanager { get; set; }
        private float loopSnelheid { get; set; }
        private float loopSnelheidHTE { get; set; }
        public string Naam { get; set; }
        private int textureIndex { get; set; }
        public Stopwatch Wachtteller { get; set; }
        private bool LooptNaarLinks { get; set; }
        public bool inLiftOfTrap { get; set; }
        public bool Wacht { get; set; }

        public Persoon()
        {
            inLiftOfTrap = false;
            LooptNaarLinks = false;
            /*Random random = new Random();
            int a = random.Next(1, 9);
            loopSnelheid = (float)a / 10;*/
            loopSnelheid = (float)0.7;  // dit mag nooit minder dan 0,6 zijn
            loopSnelheidHTE = HotelEventManager.HTE_Factor * 0.5f;
        }

        public virtual void LoadContent(ContentManager contentManager)
        {
            tempmanager = contentManager;
            Random randomgast = new Random();
            textureIndex = randomgast.Next(0, Texturelijst.Count());
            SpriteAnimatie = new GeanimeerdeTexture(contentManager, Texturelijst[textureIndex], 3);
        }

        public bool LoopNaarRuimte()
        {
            bool bestemmingBereikt = false;

            if (Bestemming != null && bestemmingBereikt == false)
            {
                HuidigeRuimte = HuidigeRuimte;
                Bestemming = Bestemming;

                // Als persoon aan komt op bestemming
                if ((Int32)Positie.X == Bestemming.EventCoordinaten.X)
                {
                    bestemmingBereikt = true;
                    HuidigeRuimte = Bestemming;
                }

                // Als persoon naar links moet
                else if (Positie.X > Bestemming.EventCoordinaten.X)
                {
                    BeweegNaarLinks();
                }

                // Als persoon naar rechts moet
                else if (Positie.X < Bestemming.EventCoordinaten.X)
                {
                    BeweegNaarRechts();
                }
            }

            return bestemmingBereikt;
        }

        private bool BeweegNaarLinks()
        {
            SpriteAnimatie = new GeanimeerdeTexture(tempmanager, Texturelijst[textureIndex], 3);
            Positie = new Vector2(Positie.X - loopSnelheidHTE, Positie.Y); // gewijzigd
            return false;
        }

        private bool BeweegNaarBoven()
        {
            SpriteAnimatie = new GeanimeerdeTexture(tempmanager, Texturelijst[textureIndex], 3);
            Positie = new Vector2(Positie.X, Positie.Y + loopSnelheidHTE); //gewijzigd
            return false;
        }

        private bool BeweegNaarBeneden()
        {
            SpriteAnimatie = new GeanimeerdeTexture(tempmanager, Texturelijst[textureIndex], 3);
            Positie = new Vector2(Positie.X, Positie.Y - loopSnelheid);
            return false;
        }

        private bool BeweegNaarRechts()
        {
            SpriteAnimatie = new GeanimeerdeTexture(tempmanager, Texturelijst[textureIndex], 3);
            Positie = new Vector2(Positie.X + loopSnelheidHTE, Positie.Y); //gewijzigd
            return false;
        }

        public void GaNaarKamer<T>(ref T ruimte)
        {
            if (Bestemming == null && HuidigeRuimte != ruimte as HotelRuimte)
            {
                Bestemming = ruimte as HotelRuimte;
            }

            if (BestemmingLijst == null && Bestemming is T)
            {
                // Zoek kortste pad naar bestemming
                DijkstraAlgoritme pathfindingAlgoritme = new DijkstraAlgoritme();
                if (Bestemming is Eetzaal)
                {
                    pathfindingAlgoritme.zoekDichtbijzijnde = true;
                }
                BestemmingLijst = pathfindingAlgoritme.MaakAlgoritme(this, HuidigeRuimte, ruimte as HotelRuimte);

                // Koppel eerste node aan bestemming
                HuidigEvent = HuidigEvent;
                Bestemming = BestemmingLijst.First();
                BestemmingLijst.Remove(BestemmingLijst.First());
            }

            // Loop via pathfinding naar bestemming
            else if (BestemmingLijst != null)
            {
                if (LoopNaarRuimte() && BestemmingLijst.Count > 0)
                {
                    if (HuidigeRuimte is Liftschacht)
                    {
                        // Ga verder met de lift
                        Liftschacht liftschacht = (Liftschacht)HuidigeRuimte;
                        liftschacht.VraagOmLift(this);
                        Bestemming = HuidigeRuimte;
                    }
                    else if(HuidigeRuimte is Trappenhuis)
                    {
                        // Ga verder met de trap
                        Trappenhuis trappenHuis = (Trappenhuis)HuidigeRuimte;
                        trappenHuis.VoegPersoonToe(this);
                        Bestemming = HuidigeRuimte;
                    }
                    else if (HuidigeRuimte.GetType() != typeof(Liftschacht) || HuidigeRuimte.GetType() != typeof(Trappenhuis))
                    {
                        HuidigeRuimte = Bestemming;
                        Bestemming = BestemmingLijst.First();
                        BestemmingLijst.Remove(BestemmingLijst.First());
                    }
                }
                else if (LoopNaarRuimte() && BestemmingLijst.Count == 0)
                {
                    Bestemming = null;
                    BestemmingLijst = null;
                    HuidigEvent.NEvent = HotelEventAdapter.NEventType.NONE;
                    if (HuidigeRuimte is Eetzaal || HuidigeRuimte is Bioscoop || HuidigeRuimte is Fitness)
                    {
                        HuidigeRuimte.voegPersoonToe((Gast)this);
                        Gast persoon = (Gast)this;
                        HuidigeRuimte.voegPersoonToe(persoon);
                        if (HuidigeRuimte is Eetzaal)
                        {
                            persoon.heeftHonger = false;
                        }
                    }
                }
             }
        }

        public void UpdateFrame(GameTime spelTijd)
        {
            SpriteAnimatie.UpdateFrame(spelTijd);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            SpriteAnimatie.ToonFrame(spritebatch, Positie);
        }
    }
}