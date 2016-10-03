using HotelSimulatie.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelSimulatie.Model
{
    public class Hotel
    {
        public List<HotelRuimte> NodeLijst { get; set; }
        public Lobby LobbyRuimte { get; set; }
        public List<Gast> Gastenlijst { get; set; }
        public List<Kamer> KamerLijst { get; set; }
        public Schoonmaker Schoonmaker_A { get; set; }
        public Schoonmaker Schoonmaker_B { get; set; }
        public Lift lift { get; set; }
        public Hotel()
        {
            NodeLijst = new List<HotelRuimte>();
            Gastenlijst = new List<Gast>();
            KamerLijst = new List<Kamer>();
            Schoonmaker_A = new Schoonmaker();
            Schoonmaker_B = new Schoonmaker();
            lift = new Lift(0);
            Addgasten();                        // tijdelijke testcode

            

            // This is seriously way to long -.- something for iteration 3?
            HotelRuimteFactory hotelRuimteFabriek = new HotelRuimteFactory();

            HotelRuimte Trap00  = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            NodeLijst.Add(Trap00);
            HotelRuimte Kamer01 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            NodeLijst.Add(Kamer01);
            KamerLijst.Add((Kamer)Kamer01);
            HotelRuimte Lobby02 = hotelRuimteFabriek.MaakHotelRuimte("Lobby");
            NodeLijst.Add(Lobby02);
            HotelRuimte Kamer03 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            NodeLijst.Add(Kamer03);
            KamerLijst.Add((Kamer)Kamer03);
            HotelRuimte Lift04 = hotelRuimteFabriek.MaakHotelRuimte("Lift", 0);
            NodeLijst.Add(Lift04);

            Kamer01.VoegBurenToe(Trap00, Lobby02);
            Lobby02.VoegBurenToe(Kamer01, Kamer03);
            Kamer03.VoegBurenToe(Kamer03, Lift04);

            HotelRuimte Trap10 = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            NodeLijst.Add(Trap10);
            HotelRuimte Kamer11 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            NodeLijst.Add(Kamer11);
            KamerLijst.Add((Kamer)Kamer11);
            HotelRuimte Kamer12 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            NodeLijst.Add(Kamer12);
            KamerLijst.Add((Kamer)Kamer12);
            HotelRuimte Kamer13 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            NodeLijst.Add(Kamer13);
            KamerLijst.Add((Kamer)Kamer13);
            HotelRuimte Lift14 = hotelRuimteFabriek.MaakHotelRuimte("Lift", 1);
            NodeLijst.Add(Lift14);

            Kamer11.VoegBurenToe(Trap10, Kamer12);
            Kamer12.VoegBurenToe(Kamer11, Kamer13);
            Kamer13.VoegBurenToe(Kamer13, Lift14);

            HotelRuimte Trap20 = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            NodeLijst.Add(Trap20);
            HotelRuimte Kamer21 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            NodeLijst.Add(Kamer21);
            KamerLijst.Add((Kamer)Kamer21);
            HotelRuimte Kamer22 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            NodeLijst.Add(Kamer22);
            KamerLijst.Add((Kamer)Kamer22);
            HotelRuimte Kamer23 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            NodeLijst.Add(Kamer23);
            KamerLijst.Add((Kamer)Kamer23);
            HotelRuimte Lift24 = hotelRuimteFabriek.MaakHotelRuimte("Lift", 2);
            NodeLijst.Add(Lift24);

            Kamer21.VoegBurenToe(Trap20, Kamer22);
            Kamer22.VoegBurenToe(Kamer21, Kamer23);
            Kamer23.VoegBurenToe(Kamer23, Lift24);

            HotelRuimte Trap30 = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            NodeLijst.Add(Trap30);
            HotelRuimte Kamer31 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            NodeLijst.Add(Kamer31);
            KamerLijst.Add((Kamer)Kamer31);
            HotelRuimte Kamer32 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            NodeLijst.Add(Kamer32);
            KamerLijst.Add((Kamer)Kamer32);
            HotelRuimte Kamer33 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            NodeLijst.Add(Kamer33);
            KamerLijst.Add((Kamer)Kamer33);
            HotelRuimte Lift34 = hotelRuimteFabriek.MaakHotelRuimte("Lift", 3);
            NodeLijst.Add(Lift34);

            Kamer31.VoegBurenToe(Trap30, Kamer32);
            Kamer32.VoegBurenToe(Kamer31, Kamer33);
            Kamer33.VoegBurenToe(Kamer33, Lift34);

            HotelRuimte Trap40 = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            NodeLijst.Add(Trap40);
            HotelRuimte Kamer41 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)Kamer41);
            NodeLijst.Add(Kamer41);
            HotelRuimte Kamer42 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            NodeLijst.Add(Kamer42);
            KamerLijst.Add((Kamer)Kamer42);
            HotelRuimte Kamer43 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            NodeLijst.Add(Kamer43);
            KamerLijst.Add((Kamer)Kamer43);
            HotelRuimte Lift44 = hotelRuimteFabriek.MaakHotelRuimte("Lift", 4);
            NodeLijst.Add(Lift44);

            Kamer41.VoegBurenToe(Trap40, Kamer42);
            Kamer42.VoegBurenToe(Kamer41, Kamer43);
            Kamer43.VoegBurenToe(Kamer43, Lift44);


            HotelRuimte Trap50 = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            NodeLijst.Add(Trap50);
            HotelRuimte Kamer51 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)Kamer51);
            NodeLijst.Add(Kamer51);
            HotelRuimte Kamer52 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)Kamer52);
            NodeLijst.Add(Kamer52);
            HotelRuimte Kamer53 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)Kamer53);
            NodeLijst.Add(Kamer53);
            HotelRuimte Lift54 = hotelRuimteFabriek.MaakHotelRuimte("Lift", 5);
            NodeLijst.Add(Lift54);

            Kamer51.VoegBurenToe(Trap50, Kamer52);
            Kamer52.VoegBurenToe(Kamer51, Kamer53);
            Kamer53.VoegBurenToe(Kamer53, Lift54);


            HotelRuimte Trap60 = hotelRuimteFabriek.MaakHotelRuimte("Trap");
            NodeLijst.Add(Trap60);
            HotelRuimte Kamer61 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)Kamer61);
            NodeLijst.Add(Kamer61);
            HotelRuimte Kamer62 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)Kamer62);
            NodeLijst.Add(Kamer62);
            HotelRuimte Kamer63 = hotelRuimteFabriek.MaakHotelRuimte("Kamer");
            KamerLijst.Add((Kamer)Kamer63);
            NodeLijst.Add(Kamer63);
            HotelRuimte Lift64 = hotelRuimteFabriek.MaakHotelRuimte("Lift", 6);
            NodeLijst.Add(Lift64);

            Kamer61.VoegBurenToe(Trap60, Kamer62);
            Kamer62.VoegBurenToe(Kamer61, Kamer63);
            Kamer63.VoegBurenToe(Kamer63, Lift64);

            Trap00.VoegBurenToe(Trap10, Kamer01);
            Trap10.VoegBurenToe(Trap20, Kamer11);
            Trap20.VoegBurenToe(Trap30, Kamer21);
            Trap30.VoegBurenToe(Trap40, Kamer31);
            Trap40.VoegBurenToe(Trap50, Kamer41);
            Trap50.VoegBurenToe(Trap60, Kamer51);
            Trap60.VoegBurenToe(Kamer61);

            Lift04.VoegBurenToe(Lift14, Kamer03);
            Lift14.VoegBurenToe(Lift24, Kamer13);
            Lift24.VoegBurenToe(Lift34, Kamer23);
            Lift34.VoegBurenToe(Lift44, Kamer33);
            Lift44.VoegBurenToe(Lift54, Kamer43);
            Lift54.VoegBurenToe(Lift64, Kamer53);
            Lift64.VoegBurenToe(Kamer63);
        }

        public void Addgasten()
        {
            // test gast
            Gast gast = new Gast();
            gast.HuidigeRuimte = LobbyRuimte;
            gast.Gastnummer = Gastenlijst.Count + 1;
            Gastenlijst.Add(gast);
            
            /*Gast gast1 = new Gast();
            gast.HuidigeRuimte = LobbyRuimte;
            gast.Gastnummer = Gastenlijst.Count + 1;
            Gastenlijst.Add(gast1);

            Gast gast2 = new Gast();
            gast.HuidigeRuimte = LobbyRuimte;
            gast.Gastnummer = Gastenlijst.Count + 1;
            Gastenlijst.Add(gast2);*/
        }
    }
}
