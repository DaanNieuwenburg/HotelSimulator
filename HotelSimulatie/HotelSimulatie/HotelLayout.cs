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
        public List<HotelRuimte> HotelRuimteLijst { get; set; }
        public List<Kamer> KamerLijst { get; set; }
        public Bioscoop bioscoop { get; set; }
        public Fitness fitness { get; set; }
        public Lift lift { get; set; }
        public Trap trap { get; set; }
        public Lobby lobby { get; set; }
        private HotelRuimteFactory hotelRuimteFactory { get; set; }
        public Eetzaal[] eetzalen { get; set; }
        private int hotelHoogte { get; set; }
        private int hotelBreedte { get; set; }
        public HotelRuimte test { get; set; }
        public HotelLayout()
        {
            hotelRuimteFactory = new HotelRuimteFactory();
            HotelRuimteLijst = LeesLayoutUit();

            // Bepaal hotel hoogte, - 1 omdat coordinaten van 1 + afmetingen van 1 = 2, terwijl hoogte 1 is 
            hotelHoogte = HotelRuimteLijst.Max(o => (Int32)o.CoordinatenInSpel.Y + (Int32)o.Afmetingen.Y) - 1;
            // Bepaal hotel breedte, trap en lift niet meegenomen
            hotelBreedte = HotelRuimteLijst.Max(o => (Int32)o.CoordinatenInSpel.X + (Int32)o.Afmetingen.X) - 1;

            zetLobbyInLayout();
            ZetLiftenEnTrappenInLayout();
            zetGangenInLayout();
            geefLayoutNodesBuren();
            zetLayoutPositiesGoed();
            test = HotelRuimteLijst.OfType<Kamer>().First(o => o.AantalSterren == 3);
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
                using (StreamReader reader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Hotel5.layout"))
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

        private void ZetLiftenEnTrappenInLayout()
        {
            // Bepaal lift zijn X coordinaten, - 1 want op de huidige coordinaten staat al een ruimte
            int liftX = HotelRuimteLijst.Min(o => (Int32)o.CoordinatenInSpel.X) - 1;

            // Maak een nieuwe lift aan, hier hebben alle schachten kennis van
            lift = (Lift)hotelRuimteFactory.MaakHotelRuimte("Lift", hotelHoogte);

            // Maak de liftschachten
            List<Liftschacht> liftSchachtenLijst = new List<Liftschacht>();
            for (int y = 0; y <= hotelHoogte; y++)
            {
                Liftschacht liftschacht = (Liftschacht)hotelRuimteFactory.MaakHotelRuimte("Liftschacht", y);
                liftschacht.CoordinatenInSpel = new Vector2(liftX, y);
                liftschacht.Afmetingen = new Vector2(1, 1);
                liftschacht.lift = lift;
                liftSchachtenLijst.Add(liftschacht);
                HotelRuimteLijst.Add(liftschacht);
            }
            lift.Liftschachtlijst = liftSchachtenLijst;
            lift.HuidigeVerdieping = liftSchachtenLijst.First(); // to remove

            // Maak trap
            trap = new Trap();
            for (int y = 0; y <= hotelHoogte; y++)
            {
                Trappenhuis trapppenhuis = (Trappenhuis)hotelRuimteFactory.MaakHotelRuimte("Trappenhuis", y);
                trapppenhuis.CoordinatenInSpel = new Vector2(hotelBreedte + 1, y);
                trapppenhuis.Afmetingen = new Vector2(1, 1);
                trapppenhuis.trap = trap;
                HotelRuimteLijst.Add(trapppenhuis);
            }
        }

        private void zetLobbyInLayout()
        {
            // Bepaal lobby positie
            int lobbyX = hotelBreedte / 2;

            // Vervang rest van de lege lobby met gangen
            for (int i = 0; i < hotelBreedte; i++)
            {
                HotelRuimte gang = hotelRuimteFactory.MaakHotelRuimte("Gang");
                gang.Afmetingen = new Vector2(2, 1);
                gang.CoordinatenInSpel = new Vector2(i, 0);
                HotelRuimteLijst.Add(gang);
            }

            // Voeg lobby toe aan lijst
            HotelRuimte lobbyRuimte = hotelRuimteFactory.MaakHotelRuimte("Lobby");
            lobbyRuimte.Afmetingen = new Vector2(1, 1);

            // - 1 want anders staat de lobby + 1 van het midden
            lobbyRuimte.CoordinatenInSpel = new Vector2(lobbyX, 0);
            HotelRuimteLijst.Add(lobbyRuimte);
            lobby = (Lobby)lobbyRuimte;
        }


        private void zetGangenInLayout()
        {
            int liftX = (Int32)HotelRuimteLijst.OfType<Liftschacht>().First().CoordinatenInSpel.X;
            int trapX = (Int32)HotelRuimteLijst.OfType<Trappenhuis>().First().CoordinatenInSpel.X;

            // Zet waar nodig gangen in de layout over bestaande ruimtes heen.
            for (int i = 0; i < HotelRuimteLijst.Count; i++)
            {
                HotelRuimte hotelRuimte = HotelRuimteLijst[i];
                if (hotelRuimte.Afmetingen.Y > 1)
                {
                    Type typeRuimte = hotelRuimte.GetType();
                    if (typeRuimte != typeof(Kamer))
                    {
                        HotelRuimte gang = hotelRuimteFactory.MaakHotelRuimte("Gang");
                        gang.Afmetingen = new Vector2(hotelRuimte.Afmetingen.X, 1);
                        gang.CoordinatenInSpel = new Vector2(hotelRuimte.CoordinatenInSpel.X, hotelRuimte.CoordinatenInSpel.Y + 1);
                        gang.Texture = hotelRuimte.Texture;
                        HotelRuimteLijst.Add(gang);
                    }
                    else
                    {
                        // Bij een kamer haal de 2de verdieping weg en vervang door een gang
                        hotelRuimte.Afmetingen = new Vector2(hotelRuimte.Afmetingen.X, 1);

                        Gang gang = new Gang();
                        gang.CoordinatenInSpel = new Vector2(hotelRuimte.CoordinatenInSpel.X, hotelRuimte.CoordinatenInSpel.Y + 1);
                        gang.Afmetingen = new Vector2(hotelRuimte.Afmetingen.X, 1);
                        gang.Texture = hotelRuimte.Texture;
                        HotelRuimteLijst.Add(gang);
                    }
                }
            }

            // Als een verdieping enkel uit gangen bestaat, vul de hele verdieping dan met gangen
            for (int i = 0; i <= hotelHoogte; i++)
            {
                List<HotelRuimte> checkLijst = HotelRuimteLijst.FindAll(o => o.CoordinatenInSpel.Y == i && o.GetType() != typeof(Liftschacht) && o.GetType() != typeof(Trappenhuis));
                List<HotelRuimte> gevondenGangen = checkLijst.FindAll(o => o.GetType() == typeof(Gang));

                if (gevondenGangen.Count == checkLijst.Count)
                {
                    // Maak gangen links
                    Gang gang = new Gang();
                    gang.CoordinatenInSpel = new Vector2(0, gevondenGangen.First().CoordinatenInSpel.Y);
                    gang.Afmetingen = new Vector2(gevondenGangen.First().CoordinatenInSpel.X, 1);
                    HotelRuimteLijst.Add(gang);

                    // Maak gangen rechts
                    Gang gangr = new Gang();
                    gangr.CoordinatenInSpel = new Vector2(gevondenGangen.Last().CoordinatenInSpel.X, gevondenGangen.Last().CoordinatenInSpel.Y);
                    gangr.Afmetingen = new Vector2(hotelBreedte - gevondenGangen.Last().CoordinatenInSpel.X, 1);
                    HotelRuimteLijst.Add(gangr);
                }
            }
        }

        private void zetLayoutPositiesGoed()
        {
            foreach (HotelRuimte hotelRuimte in HotelRuimteLijst)
            {
                // Als er sprake is van een 

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

        private void geefLayoutNodesBuren()
        {
            // Sorteer lijst op y en daarna op x
            HotelRuimteLijst = HotelRuimteLijst.OrderBy(x => x.CoordinatenInSpel.Y).ThenBy(x => x.CoordinatenInSpel.X).ToList();

            int i = 0;
            foreach (HotelRuimte hRuimte in HotelRuimteLijst)
            {
                // In het geval van een liftschacht uiterst onder
                if (hRuimte is Liftschacht && hRuimte.CoordinatenInSpel.Y == 0)
                {

                    HotelRuimte liftschachtBovenDeze = HotelRuimteLijst.OfType<Liftschacht>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y + 1);
                    List<HotelRuimte> buren = new List<HotelRuimte>();
                    buren.Add(liftschachtBovenDeze);
                    buren.Add(HotelRuimteLijst[i + 1]);
                    hRuimte.VoegBurenToe(buren);
                }
                // In het geval van een liftschacht uiterst boven
                else if (hRuimte is Liftschacht && hRuimte.CoordinatenInSpel.Y == hotelHoogte)
                {
                    HotelRuimte liftschachtOnderDeze = HotelRuimteLijst.OfType<Liftschacht>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y - 1);
                    List<HotelRuimte> buren = new List<HotelRuimte>();
                    buren.Add(liftschachtOnderDeze);
                    buren.Add(HotelRuimteLijst[i + 1]);
                    hRuimte.VoegBurenToe(buren);
                }
                // In het geval van een trap uiterst onder
                else if (hRuimte is Trappenhuis && hRuimte.CoordinatenInSpel.Y == 0)
                {
                    HotelRuimte trapBovenDeze = HotelRuimteLijst.OfType<Trappenhuis>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y + 1);
                    List<HotelRuimte> buren = new List<HotelRuimte>();
                    buren.Add(trapBovenDeze);
                    buren.Add(HotelRuimteLijst[i - 1]);
                    hRuimte.VoegBurenToe(buren);
                }
                // In het geval van een trap uiterst boven
                else if (hRuimte is Trappenhuis && hRuimte.CoordinatenInSpel.Y == hotelHoogte)
                {
                    HotelRuimte trapOnderDeze = HotelRuimteLijst.OfType<Trappenhuis>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y - 1);
                    List<HotelRuimte> buren = new List<HotelRuimte>();
                    buren.Add(trapOnderDeze);
                    buren.Add(HotelRuimteLijst[i - 1]);
                    hRuimte.VoegBurenToe(buren);
                }
                // Bij alle overige trappen en liften
                else if (hRuimte is Trappenhuis)
                {
                    HotelRuimte trapBovenDeze = HotelRuimteLijst.OfType<Trappenhuis>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y + 1);
                    HotelRuimte trapOnderDeze = HotelRuimteLijst.OfType<Trappenhuis>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y - 1);
                    List<HotelRuimte> buren = new List<HotelRuimte>();
                    buren.Add(trapBovenDeze);
                    buren.Add(trapOnderDeze);
                    buren.Add(HotelRuimteLijst[i - 1]);
                    hRuimte.VoegBurenToe(buren);
                }
                else if (hRuimte is Liftschacht)
                {
                    HotelRuimte liftSchachtBovenDeze = HotelRuimteLijst.OfType<Liftschacht>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y + 1);
                    HotelRuimte liftSchachtOnderDeze = HotelRuimteLijst.OfType<Liftschacht>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y - 1);
                    List<HotelRuimte> buren = new List<HotelRuimte>();
                    buren.Add(liftSchachtBovenDeze);
                    buren.Add(liftSchachtOnderDeze);
                    buren.Add(HotelRuimteLijst[i + 1]);
                    hRuimte.VoegBurenToe(buren);
                }
                else
                {
                    List<HotelRuimte> buren = new List<HotelRuimte>();
                    buren.Add(HotelRuimteLijst[i - 1]);
                    buren.Add(HotelRuimteLijst[i + 1]);
                    hRuimte.VoegBurenToe(buren);
                }
                i++;
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
    }
}
