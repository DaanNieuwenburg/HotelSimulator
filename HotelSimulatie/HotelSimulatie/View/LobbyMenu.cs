using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HotelSimulatie.Model;
using System.Diagnostics;

namespace HotelSimulatie.View
{
    public partial class LobbyMenu : Form
    {
        private Hotel hotel { get; set; }
        Stopwatch LvTimer { get; set; }
        public LobbyMenu(Hotel _hotel)
        {
            InitializeComponent();
            LvTimer = new Stopwatch();
            tabs.Size = this.Size;
            tabs.ItemSize = new Size(tabPage1.Width / 3, 20);
            lvGasten.Size = tabPage3.Size;

            hotel = _hotel;

            // voeg gasten toe aan de lijst
            foreach (Gast gast in hotel.PersonenInHotelLijst.OfType<Gast>())
            {
                if (gast.ToegewezenKamer == null)
                    lvGasten.Items.Add(new ListViewItem(new string[] { gast.Naam.ToString(), gast.HuidigeRuimte.Naam, "n.v.t", gast.Wacht.ToString(), gast.heeftHonger.ToString(), gast.isDood.ToString() }));
                else
                    lvGasten.Items.Add(new ListViewItem(new string[] { gast.Naam.ToString(), gast.HuidigeRuimte.Naam, gast.ToegewezenKamer.Code.ToString(), gast.Wacht.ToString(), gast.heeftHonger.ToString(), gast.isDood.ToString() }));
            }
        }
        public void RefreshInfo()
        {
            LvTimer.Start();
            if (tabs.SelectedTab == tabPage1)
            {
                lbPositieA.Text = hotel.PersonenInHotelLijst.OfType<Schoonmaker>().First().HuidigeRuimte.Naam;
                lbPositieB.Text = hotel.PersonenInHotelLijst.OfType<Schoonmaker>().Last().HuidigeRuimte.Naam;
                lbKamerA.Text = hotel.PersonenInHotelLijst.OfType<Schoonmaker>().First().InRuimte.ToString();
                lbKamerB.Text = hotel.PersonenInHotelLijst.OfType<Schoonmaker>().Last().InRuimte.ToString();
                if (hotel.PersonenInHotelLijst.OfType<Schoonmaker>().First().SchoonmaakLijst.Count > 0)
                    lbBestemmingA.Text = hotel.PersonenInHotelLijst.OfType<Schoonmaker>().First().SchoonmaakLijst.First().Naam;
                else
                    lbBestemmingA.Text = "n.v.t";
                if (hotel.PersonenInHotelLijst.OfType<Schoonmaker>().Last().SchoonmaakLijst.Count > 0)
                    lbBestemmingB.Text = hotel.PersonenInHotelLijst.OfType<Schoonmaker>().Last().SchoonmaakLijst.First().Naam;
                else
                    lbBestemmingB.Text = "n.v.t";
            }
            else if (tabs.SelectedTab == tabPage2)
            {
                lbBestemmingLift.Text = "Verdieping: " + hotel.hotelLayout.lift.LiftBestemming.Verdieping.ToString();
                lbPersonenLift.Text = hotel.hotelLayout.lift.PersonenInLift.Count().ToString();
                lbPositieLift.Text = "Verdieping: " + hotel.hotelLayout.lift.HuidigeVerdieping.Verdieping.ToString();
            }
            else if (tabs.SelectedTab == tabPage3)
            {
                if (LvTimer.Elapsed.TotalSeconds > 2)
                {
                    lvGasten.Items.Clear();
                    LvTimer.Reset();
                    foreach (Gast gast in hotel.PersonenInHotelLijst.OfType<Gast>())
                    {
                        if (gast.ToegewezenKamer == null)
                            lvGasten.Items.Add(new ListViewItem(new string[] { gast.Naam.ToString(), gast.HuidigeRuimte.Naam, "n.v.t", gast.Wacht.ToString(), gast.heeftHonger.ToString(), gast.isDood.ToString() }));
                        else
                            lvGasten.Items.Add(new ListViewItem(new string[] { gast.Naam.ToString(), gast.HuidigeRuimte.Naam, gast.ToegewezenKamer.Code.ToString(), gast.Wacht.ToString(), gast.heeftHonger.ToString(), gast.isDood.ToString() }));
                    }
                }
            }
        }
    }
}
