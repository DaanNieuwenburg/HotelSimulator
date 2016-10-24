using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Schoonmaker : Persoon
    {
        public HotelRuimte SchoonmaakPositie { get; set; }
        public Dictionary<Kamer, HotelEventAdapter> SchoonTeMakenKamersLijst { get; set; }
        public bool InKamer { get; set; }
        public string Texturenaam { get; set; }
        private int startTijd { get; set; }
        public Schoonmaker(Lobby lobby)
        {
            HuidigeRuimte = lobby;
            Texturelijst = new List<string>();
            Texturelijst.Add(@"Gasten\AnimatedRob");
            SchoonTeMakenKamersLijst = new Dictionary<Kamer, HotelEventAdapter>();
        }

        public void NieuweSchoonTeMakenKamer(Kamer kamer, HotelEventAdapter evt, Schoonmaker collega)
        {
            int dezeTaken = SchoonTeMakenKamersLijst.Count;
            int collegaTaken = SchoonTeMakenKamersLijst.Count;
            if (dezeTaken == collegaTaken || dezeTaken < collegaTaken)
            {
                SchoonTeMakenKamersLijst.Add(kamer, evt);
            }
            else if (dezeTaken > collegaTaken)
            {
                collega.SchoonTeMakenKamersLijst.Add(kamer, evt);
            }
        }

        /*public void LoadContent(ContentManager contentManager)
        {
            if (Texturenaam == "AnimatedSchoonmaker")
            {
                SpriteAnimatie = new GeanimeerdeTexture(contentManager, Texturenaam, 2);
            }
            else
            {
                SpriteAnimatie = new GeanimeerdeTexture(contentManager, Texturenaam, 4);
            }
        }*/

        public void UpdateFrame(GameTime spelTijd)
        {
            SpriteAnimatie.UpdateFrame(spelTijd);
        }

        public void Update(GameTime spelTijd)
        {
            if (InKamer == false)
            {
                if (InKamer == true && SchoonTeMakenKamersLijst.Count > 0)
                {
                    Kamer kamer = SchoonTeMakenKamersLijst.Keys.First();
                    HuidigEvent.NEvent = HotelEventAdapter.NEventType.GOTO_ROOM;
                    Bestemming = kamer;
                    startTijd = spelTijd.TotalGameTime.Seconds;
                    SchoonTeMakenKamersLijst.Remove(kamer);
                }
                else if(HuidigeRuimte == Bestemming)
                {

                }
            }
            else if (InKamer == true && spelTijd.TotalGameTime.Seconds - startTijd > HotelTijdsEenheid.schoonmakenHTE)
            {

            }
        }
    }

}
