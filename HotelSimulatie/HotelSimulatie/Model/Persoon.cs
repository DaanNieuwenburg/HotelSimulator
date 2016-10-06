using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public abstract class Persoon 
    {
        public HotelRuimte Bestemming { get; set; }
        public List<HotelRuimte> Bestemminglijst { get; set; }
        public bool BestemmingBereikt { get; set; }
        public HotelRuimte HuidigeRuimte { get; set; }
        public Vector2 Positie { get; set; }
        public GeanimeerdeTexture SpriteAnimatie { get; set; }
        private float loopSnelheid { get; set; }
        private List<string> Texturelijst { get; set; }
        private ContentManager tempmanager { get; set; }
        private int textureindex { get; set; }
        private bool LooptnaarLinks { get; set; }
        public Persoon()
        {
            LooptnaarLinks = false;
            /*Random random = new Random();
            int a = random.Next(1, 9);
            loopSnelheid = (float)a / 10;*/
            loopSnelheid = (float)0.5;
            BestemmingBereikt = false;

            Texturelijst = new List<string>();
            //Texturelijst.Add(@"Gasten\AnimatedRob");
            Texturelijst.Add(@"Gasten\AnimatedGast1");
            Texturelijst.Add(@"Gasten\AnimatedGast2");
            Texturelijst.Add(@"Gasten\AnimatedGast3");
        }

        public void LoadContent(ContentManager contentManager)
        {
            tempmanager = contentManager;
            Random randomgast = new Random();
            textureindex = randomgast.Next(0, Texturelijst.Count());
            SpriteAnimatie = new GeanimeerdeTexture(contentManager, Texturelijst[textureindex], 3);
        }

        public bool LoopNaarRuimte()
        {
            // In het geval van omhoog en omlaag gaan
            if (Bestemming is Trap && HuidigeRuimte is Trap || Bestemming is Liftschacht && HuidigeRuimte is Liftschacht)
            {
                int y = Convert.ToInt32(Positie.Y);
                if (y != Bestemming.EventCoordinaten.Y)
                {
                    if (Positie.Y > Bestemming.EventCoordinaten.Y)
                    {
                        Positie = new Vector2(Positie.X, Positie.Y - loopSnelheid);
                    }
                    else
                    {
                        Positie = new Vector2(Positie.X, Positie.Y + loopSnelheid);
                    }
                    return false;
                }
                else
                {
                    if (Bestemminglijst != null && Bestemminglijst.Count > 0)
                    {
                        // Zodra de gast bij lift komt word hij toegevoegd aan de wachtrij op de huidige verdieping
                        if (Bestemming is Liftschacht)
                        {
                            Liftschacht test = (Liftschacht)Bestemming;
                            test.UpdateWachtrij((Gast)this);
                        }
                        HuidigeRuimte = Bestemming;
                        Bestemming = Bestemminglijst.First();
                        Bestemminglijst.Remove(Bestemming);
                        
                    }
                    return true;
                }
            }

            // In het geval van rechts en naar links

            int x = Convert.ToInt32(Positie.X);
            if (x != Bestemming.EventCoordinaten.X)
            {
                if (Positie.X > Bestemming.EventCoordinaten.X)
                {
                    string texture = Texturelijst[textureindex] + "_Links";
                    if (LooptnaarLinks == false)
                    {
                        SpriteAnimatie = new GeanimeerdeTexture(tempmanager, texture, 3);
                    }
                    LooptnaarLinks = true;    
                    Positie = new Vector2(Positie.X - loopSnelheid, Positie.Y);
                }
                else
                {
                    LooptnaarLinks = false;
                    SpriteAnimatie = new GeanimeerdeTexture(tempmanager, Texturelijst[textureindex], 3);
                    Positie = new Vector2(Positie.X + loopSnelheid, Positie.Y);
                }
                return false;
            }
            else
            {
                if (Bestemminglijst != null && Bestemminglijst.Count > 0)
                {
                    HuidigeRuimte = Bestemming;
                    Bestemming = Bestemminglijst.First();
                    Bestemminglijst.Remove(Bestemming);
                }
                return true;
            }

        }

        public void GaKamerIn(HotelRuimte hotelRuimte)
        {
            Console.WriteLine("Ga kamer in");
        }

        public void UpdateFrame(GameTime spelTijd)
        {
            SpriteAnimatie.UpdateFrame(spelTijd);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            SpriteAnimatie.ToonFrame(spriteBatch, Positie);
        }
    }
}
