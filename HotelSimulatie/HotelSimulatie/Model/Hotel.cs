using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Hotel
    {
        public HotelRuimte[,] HotelLayout { get; set; }
        public Lobby LobbyRuimte { get; set; }
        public List<Gast> Gastenlijst { get; set; }
        public Schoonmaker Schoonmaker_A { get; set; }
        public Schoonmaker Schoonmaker_B { get; set; }
        public Lift lift { get; set; }
        public Hotel()
        {
            Gastenlijst = new List<Gast>();
            Schoonmaker_A = new Schoonmaker();
            Schoonmaker_B = new Schoonmaker();
            lift = new Lift();
            Addgasten();                        // tijdelijke testcode


            HotelLayout = new HotelRuimte[7,5];
            HotelRuimteFactory hotelRuimteFabriek = new HotelRuimteFactory();

            HotelLayout[0, 0] = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            HotelLayout[0, 1] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[0, 2] = hotelRuimteFabriek.MaakHotelRuimte("Lobby");
            HotelLayout[0, 3] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[0, 4] = lift;

            HotelLayout[1, 0] = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            HotelLayout[1, 1] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[1, 2] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[1, 3] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[1, 4] = lift;

            HotelLayout[2, 0] = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            HotelLayout[2, 1] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[2, 2] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[2, 3] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[2, 4] = lift;


            HotelLayout[3, 0] = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            HotelLayout[3, 1] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[3, 2] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[3, 3] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[3, 4] = lift;

            HotelLayout[4, 0] = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            HotelLayout[4, 1] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[4, 2] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[4, 3] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[4, 4] = lift;

            HotelLayout[5, 0] = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            HotelLayout[5, 1] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[5, 2] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[5, 3] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[5, 4] = lift;

            HotelLayout[6, 0] = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            HotelLayout[6, 1] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[6, 2] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[6, 3] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            HotelLayout[6, 4] = lift;
        }

        public void Addgasten()
        {
            // test gast
            Gast gast = new Gast();
            gast.HuidigeRuimte = LobbyRuimte;
            gast.Gastnummer = Gastenlijst.Count + 1;
            Gastenlijst.Add(gast);
        }
    }
}
