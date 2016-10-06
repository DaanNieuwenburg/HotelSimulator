using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HotelSimulatie.Model;

namespace HotelSimulatie.View
{
    public partial class LobbyMenu : Form
    {
        private Hotel hotel { get; set; }
        public LobbyMenu(Hotel _hotel)
        {
            InitializeComponent();
            tabs.Size = this.Size;
            tabs.ItemSize = new Size(tabPage1.Width / 3, 20);
            lvGasten.Size = tabPage3.Size;
            
            hotel = _hotel;
            
            // voeg gasten toe aan de lijst
            foreach(Gast gast in hotel.GastenLijst)
            {
                lvGasten.Items.Add(new ListViewItem(new string[] { gast.Naam.ToString(), gast.HuidigeRuimte.Naam, gast.Kamernummer.ToString(), gast.Wacht.ToString(), gast.Honger.ToString() }));
            }
            #region
            // Ken waardes voor schoonmakers toe aan labels
            lbKamerA.Text = hotel.Schoonmaker_A.InKamer.ToString();
            lbPositieA.Text = hotel.Schoonmaker_A.SchoonmaakPositie.Naam;

            lbKamerB.Text = hotel.Schoonmaker_B.InKamer.ToString();
            lbPositieB.Text = hotel.Schoonmaker_B.SchoonmaakPositie.Naam;

            // Ken waardes toe voor de lift
            lbBestemmingLift.Text = hotel.lift.Bestemming.ToString() ;
            lbPersonenLift.Text = hotel.lift.AantalPersonen.ToString() ;
            lbPositieLift.Text = hotel.lift.Positie.ToString();
            #endregion
        }
    }
}
