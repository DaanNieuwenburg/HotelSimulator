using HotelEvents;
using HotelSimulatie.Model;
using HotelSimulatie.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HotelSimulatie
{
    public class AIHandler : DrawableGameComponent
    {
        private Simulatie spel { get; set; }
        public AIHandler(Game game) : base(game)
        {
            spel = (Simulatie)game;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for (int i = 0; i < spel.hotel.PersonenInHotelLijst.Count; i++)
            {
                Persoon persoon = spel.hotel.PersonenInHotelLijst[i];
                if (persoon is Gast)
                {
                    Gast gast = (Gast)persoon;
                    if (gast.isDood == false)
                    {
                        if (gast.HuidigEvent != null && gast.inLiftOfTrap == false && gast.Wacht == false)
                        {
                            if (gast.HuidigEvent.Event == HotelEventAdapter.EventType.CHECK_IN)
                            {
                                gast.Inchecken(spel.hotel.hotelLayout, gameTime);
                            }
                            else if (gast.HuidigEvent.Event == HotelEventAdapter.EventType.CHECK_OUT)
                            {
                                // Voeg uitgecheckte kamer aan schoonmakers toe
                                Schoonmaker schoonmaker = spel.hotel.PersonenInHotelLijst.OfType<Schoonmaker>().First();
                                schoonmaker.VoegSchoonmaakRuimteToe(gast.ToegewezenKamer);

                                Lobby lobby = spel.hotel.hotelLayout.lobby;
                                gast.GaNaarRuimte<Lobby>(ref lobby);
                            }
                            else if (gast.HuidigEvent.Event == HotelEventAdapter.EventType.GOTO_CINEMA)
                            {
                                Bioscoop bioscoop = spel.hotel.hotelLayout.bioscoop;
                                gast.GaNaarRuimte<Bioscoop>(ref bioscoop);
                            }
                            else if (gast.HuidigEvent.Event == HotelEventAdapter.EventType.GOTO_FITNESS)
                            {
                                Fitness fitness = spel.hotel.hotelLayout.fitness;
                                gast.GaNaarRuimte<Fitness>(ref fitness);
                            }
                            else if (gast.HuidigEvent.Event == HotelEventAdapter.EventType.GOTO_ROOM)
                            {
                                Kamer kamer = gast.ToegewezenKamer;
                                gast.GaNaarRuimte<Kamer>(ref kamer);
                            }
                            else if (gast.HuidigEvent.Event == HotelEventAdapter.EventType.EVACUATE)
                            {
                                Lobby lobby = spel.hotel.hotelLayout.lobby;
                                gast.GaNaarRuimte<Lobby>(ref lobby);
                                spel.hotel.Update();
                            }
                            else if (gast.HuidigEvent.Event == HotelEventAdapter.EventType.NEED_FOOD)
                            {
                                List<List<HotelRuimte>> wegenNaarEetzalen = new List<List<HotelRuimte>>();
                                DijkstraAlgoritme dijkstra = new DijkstraAlgoritme();
                                foreach (Eetzaal eetzaal in spel.hotel.hotelLayout.eetzalen)
                                {
                                    wegenNaarEetzalen.Add(dijkstra.MaakAlgoritme(gast, gast.HuidigeRuimte, eetzaal));
                                }
                                if (wegenNaarEetzalen[0].Count > wegenNaarEetzalen[1].Count)
                                {
                                    Eetzaal eetzaal = (Eetzaal)wegenNaarEetzalen[1].Last();
                                    gast.GaNaarRuimte<Eetzaal>(ref eetzaal);
                                }
                                else
                                {
                                    Eetzaal eetzaal = (Eetzaal)wegenNaarEetzalen[0].Last();
                                    gast.GaNaarRuimte<Eetzaal>(ref eetzaal);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Schoonmaker schoonmaker = (Schoonmaker)persoon;
                    if (schoonmaker.Positie == new Vector2(0, 0))
                    {
                        schoonmaker.Positie = spel.GastSpawnLocatie;
                        schoonmaker.HuidigeRuimte = spel.hotel.hotelLayout.lobby;
                    }
                    else
                    {
                        if (schoonmaker.inLiftOfTrap == false && schoonmaker.Wacht == false)
                        {
                            schoonmaker.Update(gameTime, spel.hotel.hotelLayout.lobby);
                        }
                    }
                }

                // Update de hotelRuimtes
                foreach (HotelRuimte hotelRuimte in spel.hotel.hotelLayout.HotelRuimteLijst)
                {
                    hotelRuimte.Update((int)gameTime.TotalGameTime.TotalSeconds * (int)HotelEventManager.HTE_Factor);
                }

                // Update de lift
                if (spel.hotel.hotelLayout.LiftschachtenLijst.First().lift.EventCoordinaten.X != 0 && spel.hotel.hotelLayout.LiftschachtenLijst.First().lift.EventCoordinaten.Y != 0)
                {
                    spel.hotel.hotelLayout.LiftschachtenLijst.First().lift.Update((int)gameTime.TotalGameTime.TotalSeconds * (int)HotelEventManager.HTE_Factor);
                }
                else
                {
                    spel.hotel.hotelLayout.LiftschachtenLijst.First().lift.InitializeerLift(spel.hotel.hotelLayout.LiftschachtenLijst);
                }

                // Update de trap
                spel.hotel.hotelLayout.TrappenhuisLijst.First().trap.Update((int)gameTime.TotalGameTime.TotalSeconds * (int)HotelEventManager.HTE_Factor);

                // Update het hotel
                spel.hotel.Update();

                // Update Lobbymenu
                FormCollection fc = Application.OpenForms;

                foreach (Form frm in fc)
                {
                    if (frm is LobbyMenu)
                    {
                        LobbyMenu temp = (LobbyMenu)frm;
                        temp.RefreshInfo();
                    }
                }
            }

            // Controleer dood van gast
            for (int i = 0; i < spel.hotel.PersonenInHotelLijst.Count; i++)
            {
                if (spel.hotel.PersonenInHotelLijst[i] is Gast)
                {
                    Gast gast = (Gast)spel.hotel.PersonenInHotelLijst[i];
                    if (gast.isDood == false)
                    {
                        if (gast.Wachtteller.Elapsed.Seconds * HotelEventManager.HTE_Factor >= HotelTijdsEenheid.doodgaanHTE)
                        {
                            gast.isDood = true;
                            gast.HuidigEvent.Event = HotelEventAdapter.EventType.NONE;
                            gast.SpriteAnimatie = new GeanimeerdeTexture(spel.Content, @"Gasten\spook", 1);
                            gast.ToegewezenKamer.Bezet = false;
                        }
                    }
                    else
                    {
                        gast.HuidigEvent.Event = HotelEventAdapter.EventType.NONE;
                        gast.SpriteAnimatie = new GeanimeerdeTexture(spel.Content, @"Gasten\spook", 1);
                    }
                }

            }
        }


        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            spel.matrix = Matrix.CreateTranslation(new Vector3(0, 40, 0));

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, spel.spelCamera.TransformeerMatrix(this.Game.GraphicsDevice));
            base.Draw(gameTime);

            // Toon gasten
            for (int i = 0; i < spel.hotel.PersonenInHotelLijst.Count; i++)
            {
                Persoon persoon = spel.hotel.PersonenInHotelLijst[i];
                if (persoon is Gast)
                {
                    if (persoon.Bestemming != null && persoon.inLiftOfTrap == false && persoon.HuidigeRuimte != persoon.Bestemming)
                    {
                        persoon.Draw(spriteBatch);
                    }
                }
                else
                {
                    if (persoon.HuidigEvent.Event != HotelEventAdapter.EventType.IS_CLEANING && persoon.inLiftOfTrap == false)
                    {
                        persoon.Draw(spriteBatch);
                    }
                }
            }
            spriteBatch.End();
        }
    }
}