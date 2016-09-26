using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HotelSimulatie
{
    public class SpelCamera
    {
        public Vector2 Positie { get; set; }
        public bool bodemBereikt { get; set; }
        public SpelCamera()
        {
            Positie = Vector2.Zero;
        }
        
        /// <summary>
        /// Beweegt de camera in de matrix
        /// </summary>
        /// <param name="waarde">De coordinaten waar naar toe bewogen is</param>
        public void Beweeg(Vector2 waarde)
        {
            // Als de bodem van het spel wordt bereikt, stop dan de camera van verder gaan
            if (Positie.Y < 0)
            {
                // Reset de Positie
                Vector2 tempVector = Positie;
                tempVector.Y = 0;
                Positie = tempVector;
                bodemBereikt = true;
            }
            else
            {
                Positie = waarde;
            }
        }

        /// <summary>
        /// Zet de matrix om naar een nieuwe positie
        /// </summary>
        /// <param name="graphicsdevice"></param>
        /// <returns>Matrix</returns>
        public Matrix TransformeerMatrix(GraphicsDevice graphicsdevice)
        {
            return Matrix.CreateTranslation(new Vector3(-Positie.X, Positie.Y, 0));
        }
    }
}
