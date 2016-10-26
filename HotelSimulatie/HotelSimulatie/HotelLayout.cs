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
        public Lobby lobby { get; set; }
        private HotelRuimteFactory hotelRuimteFactory { get; set; }
        public Eetzaal[] eetzalen { get; set; }
        private int hotelHoogte { get; set; }
        private int hotelBreedte { get; set; }
        public HotelLayout()
        {
            hotelRuimteFactory = new HotelRuimteFactory();
            HotelRuimteLijst = LeesLayoutUit();
            ZetLiftenEnTrappenInLayout();
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
            // Bepaal hoogte
            hotelHoogte = (Int32)HotelRuimteLijst.Max(obj => obj.CoordinatenInSpel.Y) + 1;

            // Bepaal lift positie
            int LiftX = (Int32)HotelRuimteLijst.Min(obj => obj.CoordinatenInSpel.X) - 1;

            // Bepaal trap positie
            hotelBreedte = (Int32)HotelRuimteLijst.Max(obj => obj.CoordinatenInSpel.X + obj.Afmetingen.X);

            // Maak lift
            lift = (Lift)hotelRuimteFactory.MaakHotelRuimte("Lift", hotelHoogte);
            List<Liftschacht> liftlijst = new List<Liftschacht>();
            for (int y = 0; y <= hotelHoogte; y++)
            {
                Liftschacht liftschacht = (Liftschacht)hotelRuimteFactory.MaakHotelRuimte("Liftschacht", y);
                liftschacht.CoordinatenInSpel = new Vector2(LiftX, y);
                liftschacht.Afmetingen = new Vector2(1, 1);
                liftschacht.lift = lift;
                liftlijst.Add(liftschacht);
                HotelRuimteLijst.Add(liftschacht);
            }
            lift.Liftschachtlijst = liftlijst;

            // Maak trap
            for (int y = 0; y <= hotelHoogte; y++)
            {
                HotelRuimte trap = hotelRuimteFactory.MaakHotelRuimte("Trap", y);
                trap.CoordinatenInSpel = new Vector2(hotelBreedte, y);
                trap.Afmetingen = new Vector2(1, 1);
                HotelRuimteLijst.Add(trap);
            }
        }

        private void zetLobbyInLayout()
        {
            // Bepaal lobby positie
            int liftX = (Int32)HotelRuimteLijst.OfType<Liftschacht>().First().CoordinatenInSpel.X;
            int trapX = (Int32)HotelRuimteLijst.OfType<Trap>().First().CoordinatenInSpel.X;
            int lobbyXPos = trapX / 2;

            // Vervang rest van de lege lobby met gangen
            for (int i = liftX + 1; i < trapX - 1; i++)
            {
                HotelRuimte gang = hotelRuimteFactory.MaakHotelRuimte("Gang");
                gang.Afmetingen = new Vector2(2, 1);
                gang.CoordinatenInSpel = new Vector2(i, 0);
                HotelRuimteLijst.Add(gang);
            }

            // Voeg lobby toe aan lijst
            HotelRuimte lobbyRuimte = hotelRuimteFactory.MaakHotelRuimte("Lobby");
            lobbyRuimte.Afmetingen = new Vector2(1, 1);
            lobbyRuimte.CoordinatenInSpel = new Vector2(lobbyXPos, 0);
            HotelRuimteLijst.Add(lobbyRuimte);
            lobby = (Lobby)lobbyRuimte;
        }


        private void zetGangenInLayout()
        {
            int liftX = (Int32)HotelRuimteLijst.OfType<Liftschacht>().First().CoordinatenInSpel.X;
            int trapX = (Int32)HotelRuimteLijst.OfType<Trap>().First().CoordinatenInSpel.X;

            // Zet waar nodig gangen in de layout over bestaande ruimtes heen.
            for (int i = 0; i < HotelRuimteLijst.Count; i++)
            {
                HotelRuimte hotelRuimte = HotelRuimteLijst[i];
                if (hotelRuimte.Afmetingen.Y > 1)
                {
                    Type typeRuimte = hotelRuimte.GetType();
                    if(typeRuimte != typeof(Kamer))
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
                // Bepaal de hoogte nogmaals
                hotelHoogte = (Int32)HotelRuimteLijst.Max(obj => obj.CoordinatenInSpel.Y);
            }

            // Als een verdieping enkel uit gangen bestaat, vul de hele verdieping dan met gangen
            for(int i = 0; i <= hotelHoogte; i++)
            {
                List<HotelRuimte> checkLijst = HotelRuimteLijst.FindAll(o => o.CoordinatenInSpel.Y == i && o.GetType() != typeof(Liftschacht) && o.GetType() != typeof(Trap));
                List<HotelRuimte> gevondenGangen = checkLijst.FindAll(o => o.GetType() == typeof(Gang));

                if(gevondenGangen.Count == checkLijst.Count)
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
            foreach(HotelRuimte hRuimte in HotelRuimteLijst)
            {
                // In het geval van een liftschacht uiterst onder
                if (hRuimte is Liftschacht && hRuimte.CoordinatenInSpel.Y == 0)
                {

                    HotelRuimte liftschachtBovenDeze = HotelRuimteLijst.OfType<Liftschacht>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y + 1);
                    hRuimte.VoegBurenToe(HotelRuimteLijst[i + 1], liftschachtBovenDeze);
                }
                // In het geval van een liftschacht uiterst boven
                else if(hRuimte is Liftschacht && hRuimte.CoordinatenInSpel.Y == hotelHoogte)
                {
                    HotelRuimte liftschachtOnderDeze = HotelRuimteLijst.OfType<Liftschacht>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y - 1);
                    hRuimte.VoegBurenToe(HotelRuimteLijst[i + 1], liftschachtOnderDeze);
                }
                // In het geval van een trap uiterst onder
                else if(hRuimte is Trap && hRuimte.CoordinatenInSpel.Y == 0)
                {
                    HotelRuimte trapBovenDeze = HotelRuimteLijst.OfType<Trap>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y + 1);
                    hRuimte.VoegBurenToe(HotelRuimteLijst[i - 1], trapBovenDeze);
                }
                // In het geval van een trap uiterst boven
                else if (hRuimte is Trap && hRuimte.CoordinatenInSpel.Y == hotelHoogte)
                {
                    HotelRuimte trapOnderDeze = HotelRuimteLijst.OfType<Trap>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y - 1);
                    hRuimte.VoegBurenToe(HotelRuimteLijst[i - 1], trapOnderDeze);
                }
                // Bij alle overige trappen en liften
                else if(hRuimte is Trap)
                {
                    HotelRuimte trapBovenDeze = HotelRuimteLijst.OfType<Trap>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y + 1);
                    HotelRuimte trapOnderDeze = HotelRuimteLijst.OfType<Trap>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y - 1);
                    hRuimte.VoegBurenToe(HotelRuimteLijst[i - 1], trapBovenDeze, trapOnderDeze);
                }
                else if(hRuimte is Liftschacht)
                {
                    HotelRuimte liftSchachtBovenDeze = HotelRuimteLijst.OfType<Liftschacht>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y + 1);
                    HotelRuimte liftSchachtOnderDeze = HotelRuimteLijst.OfType<Liftschacht>().First(o => o.CoordinatenInSpel.Y == hRuimte.CoordinatenInSpel.Y - 1);
                    hRuimte.VoegBurenToe(HotelRuimteLijst[i + 1], liftSchachtBovenDeze, liftSchachtOnderDeze);
                }
                else
                {
                    hRuimte.VoegBurenToe(HotelRuimteLijst[i - 1], HotelRuimteLijst[i + 1]);
                }
                i++;
            }

            /*int teller = 0;
            foreach (HotelRuimte hRuimte in HotelRuimteLijst)
            {
                if (hRuimte is Liftschacht || hRuimte is Trap)
                {
                    if(hRuimte is Trap)
                    {
                        Console.WriteLine("");
                    }
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
                    if(teller + 1 < HotelRuimteLijst.Count)
                    {
                        hRuimte.VoegBurenToe(HotelRuimteLijst[teller - 1], HotelRuimteLijst[teller + 1]);
                    }
                    else
                    {
                        hRuimte.VoegBurenToe(HotelRuimteLijst[teller - 1]);
                    }
                }
                teller++;
            }*/
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
