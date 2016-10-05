using HotelSimulatie.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        public List<HotelRuimte> HotelRuimteLijst { get; set; }
        private ContentManager tempmanager { get; set; }
        public LayoutLezer()
        {
            
            HotelRuimteLijst = LeesLayoutUit();
            MaxX = bepaalMaxX();
            MaxY = bepaalMaxY();
            maakLift();
            maakTrap();
        }
        
        public List<HotelRuimte> LeesLayoutUit()
        {
            List<HotelRuimte> ruimteLijst = null;
            try
            {
                JsonConverter converter = new HotelRuimteJsonConverter();
                using (StreamReader reader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+@"\Hotel3.layout"))
                {
                    string content = reader.ReadToEnd();
                    ruimteLijst = JsonConvert.DeserializeObject<List<HotelRuimte>>(content, converter);
                }
            }
            catch (Exception ex) when (ex is JsonException || ex is IOException)
            {
                Console.WriteLine("Exception bij het uitlezen van de layout " + ex);
            }

            // Sorteer layout 
            return ruimteLijst;
        }
        


        public int bepaalMaxX()
        {
            return (Int32)HotelRuimteLijst.Max(obj => obj.CoordinatenInSpel.X);
        }

        public int bepaalMaxY()
        {
            return (Int32)HotelRuimteLijst.Max(obj => obj.CoordinatenInSpel.Y);
        }

        public void maakLift()
        {
            for (int y = 0; y < MaxY; y++)
            {
                Liftschacht liftschacht = new Liftschacht(y) { CoordinatenInSpel = new Vector2(0, y), Afmetingen = new Vector2(1, 1) };
                HotelRuimteLijst.Add(liftschacht);
            }
        }

        public void maakTrap()
        {
            for (int y = 0; y < MaxX; y++)
            {
                Liftschacht liftschacht = new Liftschacht(y) { CoordinatenInSpel = new Vector2(MaxX, y), Afmetingen = new Vector2(1, 1) };
                HotelRuimteLijst.Add(liftschacht);
            }
        }

        public void maakLobby()
        {

        }
    }
}
