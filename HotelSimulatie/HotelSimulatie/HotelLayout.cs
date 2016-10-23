using HotelSimulatie.Model;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HotelSimulatie
{
    public class HotelLayout
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public List<HotelRuimte> HotelRuimteLijst { get; set; }
        public List<Kamer> KamerLijst { get; set; }
        public Bioscoop bioscoop { get; set; }
        public Fitness fitness { get; set; }
        public Lift lift { get; set; }
        public Lobby lobby { get; set; }
        private HotelRuimteFactory hotelRuimteFactory { get; set; }
        public Eetzaal[] eetzalen { get; set; }
        public HotelLayout()
        {
            hotelRuimteFactory = new HotelRuimteFactory();
            HotelRuimteLijst = LeesLayoutUit();
            bepaalMaxXY();
            zetLiftschachtenInLayout();
            zetTrapInLayout();
            zetLobbyInLayout();
            zetGangenInLayout();
            geefLayoutNodesBuren();
            zetLayoutPositiesGoed();
            bioscoop = (Bioscoop)HotelRuimteLijst.OfType<Bioscoop>().First();
            fitness = (Fitness)HotelRuimteLijst.OfType<Fitness>().First();
            KamerLijst = (from kamer in HotelRuimteLijst where kamer is Kamer select kamer as Kamer).ToList();
            eetzalen = new Eetzaal[2];
            eetzalen[0] = (Eetzaal)HotelRuimteLijst.OfType<Eetzaal>().First();
            eetzalen[1] = (Eetzaal)HotelRuimteLijst.OfType<Eetzaal>().Last();
        }

        private List<HotelRuimte> LeesLayoutUit()
        {
            List<HotelRuimte> ruimteLijst = null;
            try
            {
                JsonConverter converter = new HotelRuimteJsonConverter();
                using (StreamReader reader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Hotel3.layout"))
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

        public void bepaalMaxXY()
        {
            MaxX = (Int32)HotelRuimteLijst.Max(obj => obj.CoordinatenInSpel.X) + 1;
            MaxY = (Int32)HotelRuimteLijst.Max(obj => obj.CoordinatenInSpel.Y);
        }

        private void zetLiftschachtenInLayout()
        {
            lift = (Lift)hotelRuimteFactory.MaakHotelRuimte("Lift",MaxY);
            List<Liftschacht> liftlijst = new List<Liftschacht>();
            for (int y = 0; y <= MaxY; y++)
            {
                Liftschacht liftschacht = (Liftschacht)hotelRuimteFactory.MaakHotelRuimte("Liftschacht", y);
                liftschacht.CoordinatenInSpel = new Vector2(0, y);
                liftschacht.Afmetingen = new Vector2(1, 1);
                liftschacht.lift = lift;
                liftlijst.Add(liftschacht);
                HotelRuimteLijst.Add(liftschacht);
            }
            lift.Liftschachtlijst = liftlijst;
        }

        private void zetTrapInLayout()
        {
            for (int y = 0; y <= MaxY; y++)
            {
                HotelRuimte trap = hotelRuimteFactory.MaakHotelRuimte("Trap", y);
                trap.CoordinatenInSpel = new Vector2(MaxX, y);
                trap.Afmetingen = new Vector2(1, 1);
                HotelRuimteLijst.Add(trap);
            }
        }

        private void zetLobbyInLayout()
        {
            // Bepaal lobby positie
            int x = MaxX;
            int ruimtesLinks = 1;       // meest linkerruimtes die er al zijn 
            int ruimtesRechts = MaxX;
            foreach (HotelRuimte hotelRuimte in HotelRuimteLijst)
            {
                if (hotelRuimte.CoordinatenInSpel.Y == 0)
                {
                    if (hotelRuimte.CoordinatenInSpel.X == 0)
                    {
                        ruimtesLinks++;
                    }
                }
            }

            // Vervang rest van de lege lobby met gangen
            for (int i = 2; i < MaxX; i++)
            {
                HotelRuimte gang = hotelRuimteFactory.MaakHotelRuimte("Gang");
                gang.Afmetingen = new Vector2(2, 1);
                gang.CoordinatenInSpel = new Vector2(i, 0);
                HotelRuimteLijst.Add(gang);
            }

            // Voeg lobby toe aan lijst
            HotelRuimte lobbyRuimte = hotelRuimteFactory.MaakHotelRuimte("Lobby");
            lobbyRuimte.Afmetingen = new Vector2(1, 1);
            lobbyRuimte.CoordinatenInSpel = new Vector2(1, 0);
            HotelRuimteLijst.Add(lobbyRuimte);
            lobby = (Lobby)lobbyRuimte;

        }

        private void zetGangenInLayout()
        {
            for (int i = 0; i < HotelRuimteLijst.Count; i++)
            {
                HotelRuimte hotelRuimte = HotelRuimteLijst[i];
                if(hotelRuimte.Afmetingen.Y > 1)
                {
                    HotelRuimte gang = hotelRuimteFactory.MaakHotelRuimte("Gang");
                    gang.Afmetingen = new Vector2(hotelRuimte.Afmetingen.X, 1);
                    gang.CoordinatenInSpel = new Vector2(hotelRuimte.CoordinatenInSpel.X, hotelRuimte.CoordinatenInSpel.Y + 1);
                    gang.Texture = hotelRuimte.Texture;
                    HotelRuimteLijst.Add(gang);
                }
            }
        }

        private void geefLayoutNodesBuren()
        {
            // Sorteer lijst bij op y en daarna op x
            HotelRuimteLijst = HotelRuimteLijst.OrderBy(x => x.CoordinatenInSpel.Y).ThenBy(x => x.CoordinatenInSpel.X).ToList();

            int teller = 0;
            foreach (HotelRuimte hRuimte in HotelRuimteLijst)
            {
                if (hRuimte is Liftschacht || hRuimte is Trap)
                {
                    HotelRuimte gevondenBovenBuur = zoekLiftOfTrapBuur(hRuimte.GetType(), "volgende", teller);
                    HotelRuimte gevondenBenedenBuur = zoekLiftOfTrapBuur(hRuimte.GetType(), "vorige", teller);
                    if (teller + 1 < HotelRuimteLijst.Count() && HotelRuimteLijst[teller + 1].GetType() != typeof(Liftschacht) && HotelRuimteLijst[teller + 1].GetType() != typeof(Trap))
                    {
                        hRuimte.VoegBurenToe(gevondenBenedenBuur, gevondenBovenBuur, HotelRuimteLijst[teller + 1]);
                    }
                    else
                    {
                        hRuimte.VoegBurenToe(gevondenBenedenBuur, gevondenBovenBuur);
                    }
                }
                else
                {
                    hRuimte.VoegBurenToe(HotelRuimteLijst[teller - 1], HotelRuimteLijst[teller + 1]);
                }
                teller++;
            }
        }

        // Returnt de volgende lift of trap buur, als die er is.
        private HotelRuimte zoekLiftOfTrapBuur(Type teVindenType, string volgendeofvorige, int zoekTeller)
        {
            bool gevonden = false;
            if (volgendeofvorige == "volgende")
            {
                zoekTeller++;   // voorkomt dat de zoekteller de huidige lift of trap vind
            }
            else
            {
                zoekTeller--;   // voorkomt dat de zoekteller de huidige lift of trap vind
            }
            HotelRuimte gevondenRuimte = null;
            while (gevonden == false)
            {
                if (zoekTeller < HotelRuimteLijst.Count && zoekTeller > -1)
                {
                    if (HotelRuimteLijst[zoekTeller].GetType() == teVindenType)
                    {
                        gevondenRuimte = HotelRuimteLijst[zoekTeller];
                        break;
                    }
                    else
                    {
                        if (volgendeofvorige == "volgende")
                        {
                            zoekTeller++;
                        }
                        else if (volgendeofvorige == "vorige" && zoekTeller > -1)
                        {
                            zoekTeller--;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            return gevondenRuimte;
        }

        private void zetLayoutPositiesGoed()
        {
            foreach (HotelRuimte hotelRuimte in HotelRuimteLijst)
            {
                int xPos = 150 * (Int32)hotelRuimte.CoordinatenInSpel.X;
                int yPos = 678 - ((Int32)hotelRuimte.CoordinatenInSpel.Y * 90) - 90;
                yPos = (Int32)(yPos - hotelRuimte.Afmetingen.Y * 90 + 90);
                int breedte = (Int32)hotelRuimte.Afmetingen.X * 150;
                int hoogte = (Int32)hotelRuimte.Afmetingen.Y * 90;

                // Bind aan properties
                Vector2 coordinaten = new Vector2(xPos, yPos);
                Vector2 afmetingen = new Vector2(breedte, hoogte);
                hotelRuimte.CoordinatenInSpel = coordinaten;
                hotelRuimte.Afmetingen = afmetingen;
            }
        }
    }
}
