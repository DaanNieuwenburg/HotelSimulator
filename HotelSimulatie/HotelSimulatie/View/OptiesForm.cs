using System;
using System.Windows.Forms;
using HotelEvents;

namespace HotelSimulatie.View
{
    public partial class OptiesForm : Form
    {
        public OptiesForm()
        {
            InitializeComponent();
            tbHTE.Text = HotelEventManager.HTE_Factor.ToString();
            tbTijdsduur1.Text = HotelTijdsEenheid.eetzaalHTE.ToString();
            tbTijdsduur2.Text = HotelTijdsEenheid.bioscoopHTE.ToString();
            tbTijdsduur3.Text = HotelTijdsEenheid.fitnessHTE.ToString();
            tbTijdsduur4.Text = HotelTijdsEenheid.schoonmakenHTE.ToString();

        }

        private void btOpslaan_Click(object sender, EventArgs e)
        {
            if(Convert.ToDouble(tbHTE.Text) < 0.1)
            {
                MessageBox.Show("De tijdsduur van een seconde mag niet lager zijn dan 0,1 HTE", "Waarschuwing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(Convert.ToDouble(tbHTE.Text) > 50)
            {
                MessageBox.Show("De tijdsduur niet hoger zijn dan 50 HTE per seconde", "Waarschuwing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            /*if(Convert.ToDouble(tbTijdsduur1.Text) < 0.1 || Convert.ToDouble(tbTijdsduur2.Text) < 0.1 || Convert.ToDouble(tbTijdsduur3.Text) < 0.1 || Convert.ToDouble(tbTijdsduur4.Text) < 0.1 || Convert.ToDouble(tbTijdsduur5.Text) < 0.1)
            {
                MessageBox.Show("De tijdsduur van een Activiteit mag niet lager zijn dan 0,1 HTE", "Waarschuwing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/
            else
            {
                HotelEventManager.HTE_Factor = (float)Convert.ToDouble(tbHTE.Text.Replace('.', ','));
                HotelTijdsEenheid.fitnessHTE = (Int32)Convert.ToDouble(tbTijdsduur3.Text.Replace('.', ','));
                HotelTijdsEenheid.eetzaalHTE = (Int32)Convert.ToDouble(tbTijdsduur1.Text.Replace('.', ','));
                HotelTijdsEenheid.bioscoopHTE = (Int32)Convert.ToDouble(tbTijdsduur2.Text.Replace('.', ','));
                HotelTijdsEenheid.schoonmakenHTE = (Int32)Convert.ToDouble(tbTijdsduur4.Text.Replace('.', ','));
                DialogResult = DialogResult.OK;
            }
            
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
