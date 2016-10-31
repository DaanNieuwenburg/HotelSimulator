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
            #region
            //Ken waardes voor schoonmakers toe aan labels
            //lbPositieA.Text = hotel.PersonenInHotelLijst.OfType<Schoonmaker>().First().SchoonmaakPositie.Naam;
            //lbPositieB.Text = hotel.PersonenInHotelLijst.OfType<Schoonmaker>().Last().SchoonmaakPositie.Naam;

            // Ken waardes toe voor de lift
            //lbBestemmingLift.Text = "Verdieping: " + hotel.hotelLayout.lift.LiftBestemming.Verdieping.ToString() ;
            //lbPersonenLift.Text = hotel.hotelLayout.lift.GasteninLift.Count().ToString() ;
            //lbPositieLift.Text ="Verdieping: " + hotel.hotelLayout.lift.HuidigeVerdieping.Verdieping.ToString();
            #endregion
        }
        public void RefreshInfo()
        {
            LvTimer.Start();
            if (tabs.SelectedTab == tabPage1)
            {
                lbPositieA.Text = hotel.PersonenInHotelLijst.OfType<Schoonmaker>().First().HuidigeRuimte.Naam;
                lbPositieB.Text = hotel.PersonenInHotelLijst.OfType<Schoonmaker>().Last().HuidigeRuimte.Naam;
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
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LvTimer.Start();
            if(tabs.SelectedTab == tabPage1)
            {
                lbPositieA.Text = hotel.PersonenInHotelLijst.OfType<Schoonmaker>().First().SchoonmaakLijst.First().Naam;
                lbPositieB.Text = hotel.PersonenInHotelLijst.OfType<Schoonmaker>().Last().SchoonmaakLijst.First().Naam;
            }
            else if(tabs.SelectedTab == tabPage2)
            {
                lbBestemmingLift.Text = "Verdieping: " + hotel.hotelLayout.lift.LiftBestemming.Verdieping.ToString();
                lbPersonenLift.Text = hotel.hotelLayout.lift.PersonenInLift.Count().ToString();
                lbPositieLift.Text = "Verdieping: " + hotel.hotelLayout.lift.HuidigeVerdieping.Verdieping.ToString();
            }
            else if(tabs.SelectedTab == tabPage3)
            {
                if(LvTimer.Elapsed.TotalSeconds > 10)
                {
                    LvTimer.Reset();
                    lvGasten.Items.Clear();
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

        /*private void lvGasten_ItemActivate(object sender, EventArgs e)
        {
            string gastnaam = lvGasten.SelectedItems[0].Text;
            foreach(Gast g in hotel.PersonenInHotelLijst)
            {
                if (gastnaam == g.Naam)
                    MessageBox.Show(g.HuidigEvent.ToString());
            }
            
        }*/
    }
}
