using System;
using System.Windows.Forms;

namespace HotelSimulatie.View
{
    public partial class OptiesForm : Form
    {
        public OptiesForm()
        {
            InitializeComponent();
        }

        private void btOpslaan_Click(object sender, EventArgs e)
        {
            if(Convert.ToDouble(tbHTE.Text) < 0.1)
            {
                MessageBox.Show("De tijdsduur van een HTE mag niet lager zijn dan 0,1 seconde", "Waarschuwing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if(Convert.ToDouble(tbTijdsduur1.Text) < 0.1 || Convert.ToDouble(tbTijdsduur2.Text) < 0.1 || Convert.ToDouble(tbTijdsduur3.Text) < 0.1 || Convert.ToDouble(tbTijdsduur4.Text) < 0.1 || Convert.ToDouble(tbTijdsduur5.Text) < 0.1)
            {
                MessageBox.Show("De tijdsduur van een Activiteit mag niet lager zijn dan 0,1 HTE", "Waarschuwing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
            
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
