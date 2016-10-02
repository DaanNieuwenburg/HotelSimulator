using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{ 
    public class Lobby : HotelRuimte
    {
        public Rectangle LobbyRectangle { get; set; }
        public Queue<Gast> Wachtrij { get; set; }
        public Lobby()
        {
            EventCoordinaten = new Vector2(LobbyRectangle.Left + 80, LobbyRectangle.Bottom);
            Wachtrij = new Queue<Gast>();
            Naam = "lobby_Normaal";
            //Texture is lobby_Death als er een klant komt. Deze moet nog wel worden geimplementeerd
        }

        public override void LoadContent(ContentManager contentManager)
        {
            Texture = contentManager.Load<Texture2D>(Naam);
        }

        public void GastInChecken(Gast gast)
        {
            Wachtrij.Enqueue(gast);
        }
    }
}
