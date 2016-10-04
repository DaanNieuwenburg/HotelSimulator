using HotelSimulatie.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HotelSimulatie
{
    public class LayoutLezer
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        List<HotelRuimte> hotelRuimte = new List<HotelRuimte>();
        
        public LayoutLezer()
        {
            LeesLayoutUit();
            //MaxX = bepaalMaxX();
            //MaxY = bepaalMaxY();
        }
        
        public List<object> LeesLayoutUit()
        {
            List<object> ruimteLijst = null;
            try
            {
                using (StreamReader reader = new StreamReader(@"C:\Users\niels\Hotel3.layut"))
                {
                    string content = reader.ReadToEnd();
                    ruimteLijst = JsonConvert.DeserializeObject<List<object>>(content);
                }
            }
            catch (Exception ex) when (ex is JsonException || ex is IOException)
            {
                Console.WriteLine("Exception bij het uitlezen van de layout " + ex);
            }
            return ruimteLijst;
        }


        public int bepaalMaxX()
        {
            return (Int32)hotelRuimte.Max(obj => obj.CoordinatenInSpel.X);
        }

        public int bepaalMaxY()
        {
            return (Int32)hotelRuimte.Max(obj => obj.CoordinatenInSpel.Y);
        }

        public void maakLift()
        {

        }

        public void maakTrap()
        {

        }

        public void maakLobby()
        {

        }
    }
}
