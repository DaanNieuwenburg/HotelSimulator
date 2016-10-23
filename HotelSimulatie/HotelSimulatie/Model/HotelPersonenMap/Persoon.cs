using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEvents;
using System.Reflection;

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
        private float loopSnelheid { get; set; }
        
        private ContentManager tempmanager { get; set; }
        public string Naam { get; set; }
        protected int textureIndex { get; set; }
        protected List<string> Texturelijst { get; set; }
        private bool LooptNaarLinks { get; set; }
        public Persoon()
        {
            LooptNaarLinks = false;
            /*Random random = new Random();
            int a = random.Next(1, 9);
            loopSnelheid = (float)a / 10;*/
            loopSnelheid = (float)0.7;  // dit mag nooit minder dan 0,6 zijn

            

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

                // Beweeg naar boven of beneden
                if (Bestemming.EventCoordinaten.X == HuidigeRuimte.EventCoordinaten.X && (Bestemming is Liftschacht || Bestemming is Trap))
                {
                    if (Bestemming is Liftschacht)
                    {
                        Liftschacht liftschacht = (Liftschacht)HuidigeRuimte;
                        liftschacht.VraagOmLift(this);

                        Bestemming = BestemmingLijst.First();
                        bestemmingBereikt = false;
                    }
                    else
                    {
                        if ((Int32)Positie.Y > Bestemming.EventCoordinaten.Y)
                        {
                            BeweegNaarBoven();
                        }
                        else if ((Int32)Positie.Y < Bestemming.EventCoordinaten.Y)
                        {
                            BeweegNaarOnder();
                        }
                        else
                        {
                            HuidigeRuimte = Bestemming;
                            bestemmingBereikt = true;
                        }
                    }
                }
                else
                {
                    if ((Int32)Positie.X > Bestemming.EventCoordinaten.X)
                    {
                        BeweegNaarLinks();
                    }
                    else if ((Int32)Positie.X < Bestemming.EventCoordinaten.X)
                    {
                        BeweegNaarRechts();
                    }
                    else
                    {
                        HuidigeRuimte = Bestemming;
                        bestemmingBereikt = true;
                    }
                }
            }
            return bestemmingBereikt;
        }

        private bool BeweegNaarLinks()
        {
            SpriteAnimatie = new GeanimeerdeTexture(tempmanager, Texturelijst[textureIndex], 3);
            Positie = new Vector2(Positie.X - loopSnelheid, Positie.Y);
            return false;
        }

        private bool BeweegNaarRechts()
        {
            SpriteAnimatie = new GeanimeerdeTexture(tempmanager, Texturelijst[textureIndex], 3);
            Positie = new Vector2(Positie.X + loopSnelheid, Positie.Y);
            return false;
        }

        private bool BeweegNaarBoven()
        {
            Positie = new Vector2(Positie.X, Positie.Y - loopSnelheid);
            return false;
        }

        private bool BeweegNaarOnder()
        {
            Positie = new Vector2(Positie.X, Positie.Y + loopSnelheid);
            return false;
        }


        public void GaKamerIn(HotelRuimte hotelRuimte)
        {
            Console.WriteLine("Ga kamer in");
            Bestemming = null;
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
                    if (Bestemming is Liftschacht)
                    {
                        Bestemming = BestemmingLijst.First();
                    }
                    else
                    {
                        Bestemming = BestemmingLijst.First();
                        BestemmingLijst.Remove(BestemmingLijst.First());
                    }
                }
                else if (LoopNaarRuimte() && BestemmingLijst.Count == 0)
                {
                    Bestemming = null;
                    BestemmingLijst = null;
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