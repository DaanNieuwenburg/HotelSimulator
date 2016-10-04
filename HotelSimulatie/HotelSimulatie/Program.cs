using System;
using System.Windows.Forms;
using HotelSimulatie.View;
using HotelSimulatie.Model;
using System.IO;
using Newtonsoft.Json;

namespace HotelSimulatie
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Hoofdmenu form = new Hoofdmenu();

            // Wordt er op start spel gedrukt, dan wordt in onderstaande code een nieuw spel gemaakt
            if (form.ShowDialog() == DialogResult.OK)
            {
                using (Spel game = new Spel(new Hotel()))
                {
                    // Dit stuk code is voor het uitlezen van de layout file (hernoem naar json). Het moet nog verandert worden van positie
                    /*string text = File.ReadAllText(@"C:\Users\daan1\Source\Repos\HotelSimulator\HotelSimulatie\HotelSimulatie\Hotel2.json");
                    HotelRuimte ruimte = JsonConvert.DeserializeObject<HotelRuimte>(text);*/


                    game.Run();
                }
            }
        }
    }
}

