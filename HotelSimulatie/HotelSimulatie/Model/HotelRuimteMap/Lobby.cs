using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{ 
    public class Lobby : HotelRuimte
    {
        public Lobby()
        {
            Naam = "Lobby";
            TextureCode = 0;
        }

        public void GasteInChecken()
        {
            
        }
    }
}
