using HotelSimulatie.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie
{
    public class TempClasse
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        List<HotelRuimte> hotelRuimte = new List<HotelRuimte>();
        
        public TempClasse()
        {
            MaxX = bepaalMaxX();
            MaxY = bepaalMaxY();
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
