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
        public ContentManager tempmanager { get; set; }
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


        public void GaNaarRuimte<T>(ref T ruimte)
        {
            if(Naam == "Gast1" || Naam == "gast1")
            {
                Console.WriteLine();
            }
            // Zoek het kortste pad naar de bestemming
            if (BestemmingLijst == null)
            {
                DijkstraAlgoritme dijkstra = new DijkstraAlgoritme();
                BestemmingLijst = dijkstra.MaakAlgoritme(this, HuidigeRuimte, ruimte as HotelRuimte);
                Bestemming = BestemmingLijst.First();
                BestemmingLijst.Remove(Bestemming);
            }
            else if (Beweeg())
            {
                // Aangekomen in de volgende ruimte
                if (BestemmingLijst.Count > 0 || Bestemming != null)
                {
                    if (HuidigeRuimte is Liftschacht && Bestemming is Liftschacht || HuidigeRuimte is Trappenhuis && Bestemming is Trappenhuis)
                    {
                        // Zet laatste liftschacht of trappenhuis als bestemming, verwijder ze dan allemaal
                        // Het kan voorkomen dat de bestemming die enige liftschacht of trappenhuis is, dan is de bestemminglijst niet gevuld
                        if (BestemmingLijst.Contains(Bestemming))
                        {
                            Bestemming = BestemmingLijst.Last(o => o.GetType() == HuidigeRuimte.GetType());
                            BestemmingLijst.RemoveAll(o => o.GetType() == HuidigeRuimte.GetType());
                        }
                        HuidigeRuimte.VoegPersoonToe(this);
                    }
                    else
                    {
                        // Haal de volgende ruimte op, in het geval van een liftschacht of trappenhuis hoeft dit echter niet
                        if (Bestemming.GetType() != typeof(Liftschacht) && Bestemming.GetType() != typeof(Trappenhuis))
                        {
                            HuidigeRuimte = Bestemming;
                            if (BestemmingLijst.Count != 0)
                            {
                                Bestemming = BestemmingLijst.First();

                                BestemmingLijst.Remove(Bestemming);
                            }
                            else
                            {
                                // Aangekomen op bestemming
                                gaRuimteIn(Bestemming);
                            }
                        }
                        else
                        {
                            HuidigeRuimte = Bestemming;
                        }
                    }
                }
            }
        }

        public bool Beweeg()
        {
            // Laat persoon bewegen d.m.v. animatie
            if (Positie.X < Bestemming.EventCoordinaten.X)
            {
                SpriteAnimatie = new GeanimeerdeTexture(tempmanager, Texturelijst[textureIndex], 3);
                Positie = new Vector2(Positie.X + loopSnelheidHTE, Positie.Y);
                return false;
            }
            else if (Positie.X > Bestemming.EventCoordinaten.X)
            {
                SpriteAnimatie = new GeanimeerdeTexture(tempmanager, Texturelijst[textureIndex], 3);
                Positie = new Vector2(Positie.X - loopSnelheidHTE, Positie.Y);
                return false;
            }

            // Als persoon in een nieuwe ruimte aankomt ( bestemming bereikt )
            else
            {
                return true;
            }
        }

        private void gaRuimteIn(HotelRuimte ruimte)
        {
            // Koppelt ruimte aan persoon
            HuidigeRuimte = ruimte;
            Bestemming = null;
            BestemmingLijst = null;
            HuidigEvent.NEvent = HotelEventAdapter.NEventType.NONE;

            if (this is Gast)
            {
                ruimte.VoegPersoonToe((Gast)this);
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