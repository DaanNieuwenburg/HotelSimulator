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
        public List<Kamer> KamerLijst { get; set; }
        public Schoonmaker Schoonmaker_A { get; set; }
        public Schoonmaker Schoonmaker_B { get; set; }
        public Lift lift { get; set; }
        public Hotel()
        {
            Gastenlijst = new List<Gast>();
            KamerLijst = new List<Kamer>();
            Schoonmaker_A = new Schoonmaker();
            Schoonmaker_B = new Schoonmaker();
            lift = new Lift(0);
            Addgasten();                        // tijdelijke testcode


            HotelLayout = new HotelRuimte[7,5];
            HotelRuimteFactory hotelRuimteFabriek = new HotelRuimteFactory();

            HotelLayout[0, 0] = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            HotelLayout[0, 1] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[0, 1]);
            HotelLayout[0, 2] = hotelRuimteFabriek.MaakHotelRuimte("Lobby");
            HotelLayout[0, 3] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[0, 3]);
            HotelLayout[0, 4] = hotelRuimteFabriek.MaakHotelRuimte("Lift", 0);

            HotelLayout[1, 0] = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            HotelLayout[1, 1] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[1, 1]);
            HotelLayout[1, 2] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[1, 2]);
            HotelLayout[1, 3] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[1, 3]);
            HotelLayout[1, 4] = hotelRuimteFabriek.MaakHotelRuimte("Lift", 1);

            HotelLayout[2, 0] = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            HotelLayout[2, 1] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[2, 1]);
            HotelLayout[2, 2] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[2, 2]);
            HotelLayout[2, 3] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[2, 3]);
            HotelLayout[2, 4] = hotelRuimteFabriek.MaakHotelRuimte("Lift", 2);


            HotelLayout[3, 0] = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            HotelLayout[3, 1] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[3, 1]);
            HotelLayout[3, 2] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[3, 2]);
            HotelLayout[3, 3] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[3, 3]);
            HotelLayout[3, 4] = hotelRuimteFabriek.MaakHotelRuimte("Lift", 3);

            HotelLayout[4, 0] = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            HotelLayout[4, 1] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[4, 1]);
            HotelLayout[4, 2] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[4, 2]);
            HotelLayout[4, 3] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[4, 3]);
            HotelLayout[4, 4] = hotelRuimteFabriek.MaakHotelRuimte("Lift", 4);

            HotelLayout[5, 0] = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            HotelLayout[5, 1] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[5, 1]);
            HotelLayout[5, 2] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[5, 2]);
            HotelLayout[5, 3] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[5, 3]);
            HotelLayout[5, 4] = hotelRuimteFabriek.MaakHotelRuimte("Lift", 5);

            HotelLayout[6, 0] = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            HotelLayout[6, 1] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[6, 1]);
            HotelLayout[6, 2] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[6, 2]);
            HotelLayout[6, 3] = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)HotelLayout[6, 3]);
            HotelLayout[6, 4] = hotelRuimteFabriek.MaakHotelRuimte("Lift", 6);
        }

        public void Addgasten()
        {
            // test gast
            Gast gast = new Gast();
            gast.HuidigeRuimte = LobbyRuimte;
            gast.Gastnummer = Gastenlijst.Count + 1;
            Gastenlijst.Add(gast);
            
            Gast gast1 = new Gast();
            gast.HuidigeRuimte = LobbyRuimte;
            gast.Gastnummer = Gastenlijst.Count + 1;
            Gastenlijst.Add(gast1);

            Gast gast2 = new Gast();
            gast.HuidigeRuimte = LobbyRuimte;
            gast.Gastnummer = Gastenlijst.Count + 1;
            Gastenlijst.Add(gast2);
        }
    }
}
