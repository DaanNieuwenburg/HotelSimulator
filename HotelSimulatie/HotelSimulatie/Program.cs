using System;
using System.Windows.Forms;
using HotelSimulatie.View;
using HotelSimulatie.Model;

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
                    game.Run();
                }
            }
        }
    }
}

