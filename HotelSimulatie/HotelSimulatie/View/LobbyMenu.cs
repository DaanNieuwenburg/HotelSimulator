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
            foreach(Gast gast in hotel.Gastenlijst)
            {
                lvGasten.Items.Add(new ListViewItem(new string[] { gast.Gastnummer.ToString(), gast.Positie, gast.Kamernummer.ToString(), gast.Wacht.ToString(), gast.Honger.ToString() }));
            }
            
            // Ken waardes voor schoonmakers toe aan labels
            
            // Ken waardes toe voor de lift
        }
    }
}
