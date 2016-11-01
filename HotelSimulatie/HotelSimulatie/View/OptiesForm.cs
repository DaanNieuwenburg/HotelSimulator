using System;
using System.Windows.Forms;
using HotelEvents;
using System.Collections.Generic;

namespace HotelSimulatie.View
{
    public partial class OptiesForm : Form
    {
        private bool allgood { get; set; }
        public OptiesForm()
        {
            InitializeComponent();
            tbHTE.Text = HotelEventManager.HTE_Factor.ToString();
            tbTijdsduur1.Text = HotelTijdsEenheid.eetzaalHTE.ToString();
            tbTijdsduur2.Text = HotelTijdsEenheid.bioscoopHTE.ToString();
            tbTijdsduur3.Text = HotelTijdsEenheid.fitnessHTE.ToString();
            tbTijdsduur4.Text = HotelTijdsEenheid.schoonmakenHTE.ToString();
            allgood = true;

        }

        private void btOpslaan_Click(object sender, EventArgs e)
        { 
            // Controleer waardes van de textboxes
            #region
            allgood = true;
            if (Convert.ToDouble(tbHTE.Text) < 0.1)
            {
                allgood = false;
                MessageBox.Show("De tijdsduur van een seconde mag niet lager zijn dan 0,1 HTE", "Waarschuwing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Convert.ToDouble(tbHTE.Text) > 50)
            {
                allgood = false;
                MessageBox.Show("De tijdsduur niet hoger zijn dan 50 HTE per seconde", "Waarschuwing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            foreach (Control x in this.Controls)
            {
                if (x is TextBox && x.Name.Contains("tbTijdsduur"))
                {
                    if (x.Text.Length <= 0)
                    {
                        allgood = false;
                    }
                    else
                    {
                        
                        if (Convert.ToDouble(x.Text.Replace(".", ",")) < 0.6)
                            allgood = false;
                    }
                }
            }
            #endregion

            if (allgood == true)
            {
                HotelEventManager.HTE_Factor = (float)Convert.ToDouble(tbHTE.Text.Replace('.', ','));
                HotelTijdsEenheid.fitnessHTE = (Int32)Math.Round(Convert.ToDouble(tbTijdsduur3.Text.Replace('.', ',')));
                HotelTijdsEenheid.eetzaalHTE = (Int32)Math.Round(Convert.ToDouble(tbTijdsduur1.Text.Replace('.', ',')));
                HotelTijdsEenheid.bioscoopHTE = (Int32)Math.Round(Convert.ToDouble(tbTijdsduur2.Text.Replace('.', ',')));
                HotelTijdsEenheid.schoonmakenHTE = (Int32)Math.Round(Convert.ToDouble(tbTijdsduur4.Text.Replace('.', ',')));
                HotelTijdsEenheid.doodgaanHTE = (Int32)Math.Round(Convert.ToDouble(tbTijdsduur5.Text.Replace('.', ',')));
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Een of meerdere waarden zijn onjuist, Pas deze aan en probeer het opnieuw\nLet op: een waarde mag niet kleiner zijn dan 0.6 of leeg zijn");
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
