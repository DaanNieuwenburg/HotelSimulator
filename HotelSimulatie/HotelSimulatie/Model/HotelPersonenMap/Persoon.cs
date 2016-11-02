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
        public HotelRuimte EindBestemming { get; set; }
        public List<HotelRuimte> BestemmingLijst { get; set; }
        public HotelEventAdapter HuidigEvent { get; set; }
        public HotelRuimte HuidigeRuimte { get; set; }
        public Vector2 Positie { get; set; }
        public GeanimeerdeTexture SpriteAnimatie { get; set; }
        public List<string> Texturelijst { get; set; }
        public ContentManager tempmanager { get; set; }
        private float loopSnelheid { get; set; }
        private float loopSnelheidHTE { get; set; }
        public string Naam { get; set; }
        protected int textureIndex { get; set; }
        public Stopwatch Wachtteller { get; set; }
        private bool? LooptNaarLinks { get; set; }
        public bool inLiftOfTrap { get; set; }
        public bool Wacht { get; set; }

        public Persoon()
        {
            inLiftOfTrap = false;
            LooptNaarLinks = false;
            //loopSnelheidHTE = HotelEventManager.HTE_Factor * 2.0f;        --> Bewegen volgens juiste HTE snelheid, Veroorzaakt problemen
            loopSnelheidHTE = HotelEventManager.HTE_Factor * 1.0f;
        }

        public abstract void LoadContent(ContentManager contentManager);


        public void GaNaarRuimte<T>(ref T ruimte)
        {
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
                                if (HuidigeRuimte is Lobby)
                                {
                                    Lobby lobby = (Lobby)HuidigeRuimte;
                                    lobby.GaRuimteIn((Gast)this);
                                }
                                else
                                {
                                    // Aangekomen op bestemming
                                    gaRuimteIn(Bestemming);
                                }
                                
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
            // Kijkt welke kant persoon op loopt
            if (LooptNaarLinks == null)
            {
                if (Positie.X < Bestemming.EventCoordinaten.X)
                {
                    LooptNaarLinks = false;
                }
                else if (Positie.X > Bestemming.EventCoordinaten.X)
                {
                    LooptNaarLinks = true;
                }
            }

            // Laat persoon bewegen d.m.v. animatie
            if (Positie.X < Bestemming.EventCoordinaten.X && LooptNaarLinks == false)
            {
                if (this is Gast)
                {
                    SpriteAnimatie = new GeanimeerdeTexture(tempmanager, Texturelijst[textureIndex], 3);
                }
                Positie = new Vector2(Positie.X + loopSnelheidHTE, Positie.Y);
                return false;
            }
            else if (Positie.X > Bestemming.EventCoordinaten.X && LooptNaarLinks == true)
            {
                if(this is Gast)
                {
                    SpriteAnimatie = new GeanimeerdeTexture(tempmanager, Texturelijst[textureIndex]+"_Links", 3);
                }
                Positie = new Vector2(Positie.X - loopSnelheidHTE, Positie.Y);
                return false;
            }

            // Als persoon in een nieuwe ruimte aankomt ( bestemming bereikt )
            else
            {
                LooptNaarLinks = null;
                return true;
            }
        }

        private void gaRuimteIn(HotelRuimte ruimte)
        {
            // Koppelt ruimte aan persoon
            HuidigeRuimte = ruimte;
            Bestemming = null;
            BestemmingLijst = null;
            HuidigEvent.Event = HotelEventAdapter.EventType.NONE;

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