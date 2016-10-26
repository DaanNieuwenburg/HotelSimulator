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
        public LobbyMenu(Hotel _hotel)
        {
            InitializeComponent();
            tabs.Size = this.Size;
            tabs.ItemSize = new Size(tabPage1.Width / 3, 20);
            lvGasten.Size = tabPage3.Size;

            hotel = _hotel;

            // voeg gasten toe aan de lijst
            foreach (Gast gast in hotel.GastenLijst)
            {
                if (gast.ToegewezenKamer == null)
                    lvGasten.Items.Add(new ListViewItem(new string[] { gast.Naam.ToString(), gast.HuidigeRuimte.Naam, "n.v.t", gast.Wacht.ToString(), gast.heeftHonger.ToString() }));
                else
                    lvGasten.Items.Add(new ListViewItem(new string[] { gast.Naam.ToString(), gast.HuidigeRuimte.Naam, gast.ToegewezenKamer.Code.ToString(), gast.Wacht.ToString(), gast.heeftHonger.ToString() }));
            }
            #region
            //Ken waardes voor schoonmakers toe aan labels
            //lbPositieA.Text = hotel.Schoonmakers[0].SchoonmaakPositie.Naam;
            //lbPositieB.Text = hotel.Schoonmakers[1].SchoonmaakPositie.Naam;

            // Ken waardes toe voor de lift
            //lbBestemmingLift.Text = "Verdieping: " + hotel.hotelLayout.lift.LiftBestemming.Verdieping.ToString() ;
            //lbPersonenLift.Text = hotel.hotelLayout.lift.GasteninLift.Count().ToString() ;
            //lbPositieLift.Text ="Verdieping: " + hotel.hotelLayout.lift.HuidigeVerdieping.Verdieping.ToString();
            #endregion
        }
        public void RefreshInfo()
        {
            if (tabs.SelectedTab == tabPage1)
            {
                //lbPositieA.Text = hotel.Schoonmaker_A.SchoonmaakPositie.Naam;
                //lbPositieB.Text = hotel.Schoonmaker_B.SchoonmaakPositie.Naam;
            }
            else if (tabs.SelectedTab == tabPage2)
            {
                lbBestemmingLift.Text = "Verdieping: " + hotel.hotelLayout.lift.LiftBestemming.Verdieping.ToString();
                lbPersonenLift.Text = hotel.hotelLayout.lift.PersonenInLift.Count().ToString();
                lbPositieLift.Text = "Verdieping: " + hotel.hotelLayout.lift.HuidigeVerdieping.Verdieping.ToString();
            }
            else if (tabs.SelectedTab == tabPage3)
            {
                lvGasten.Items.Clear();
                foreach (Gast gast in hotel.GastenLijst)
                {
                    if (gast.ToegewezenKamer == null)
                        lvGasten.Items.Add(new ListViewItem(new string[] { gast.Naam.ToString(), gast.HuidigeRuimte.Naam, "n.v.t", gast.Wacht.ToString(), gast.heeftHonger.ToString() }));
                    else
                        lvGasten.Items.Add(new ListViewItem(new string[] { gast.Naam.ToString(), gast.HuidigeRuimte.Naam, gast.ToegewezenKamer.Code.ToString(), gast.Wacht.ToString(), gast.heeftHonger.ToString() }));
                }
            }

        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if(tabs.SelectedTab == tabPage1)
            {
                //lbPositieA.Text = hotel.Schoonmaker_A.SchoonmaakPositie.Naam;
                //lbPositieB.Text = hotel.Schoonmaker_B.SchoonmaakPositie.Naam;
            }
            else if(tabs.SelectedTab == tabPage2)
            {
                lbBestemmingLift.Text = "Verdieping: " + hotel.hotelLayout.lift.LiftBestemming.Verdieping.ToString();
                lbPersonenLift.Text = hotel.hotelLayout.lift.PersonenInLift.Count().ToString();
                lbPositieLift.Text = "Verdieping: " + hotel.hotelLayout.lift.HuidigeVerdieping.Verdieping.ToString();
            }
            else if(tabs.SelectedTab == tabPage3)
            {
                lvGasten.Items.Clear();
                foreach (Gast gast in hotel.GastenLijst)
                {
                    if (gast.ToegewezenKamer == null)
                        lvGasten.Items.Add(new ListViewItem(new string[] { gast.Naam.ToString(), gast.HuidigeRuimte.Naam, "n.v.t", gast.Wacht.ToString(), gast.heeftHonger.ToString() }));
                    else
                        lvGasten.Items.Add(new ListViewItem(new string[] { gast.Naam.ToString(), gast.HuidigeRuimte.Naam, gast.ToegewezenKamer.Code.ToString(), gast.Wacht.ToString(), gast.heeftHonger.ToString() }));
                }
            }
            
        }
    }
}
