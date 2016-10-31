﻿using System;
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
        }

        private void btOpslaan_Click(object sender, EventArgs e)
        {
            if(Convert.ToDouble(tbHTE.Text) < 0.1)
            {
                MessageBox.Show("De tijdsduur van een seconde mag niet lager zijn dan 0,1 HTE", "Waarschuwing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            /*if(Convert.ToDouble(tbTijdsduur1.Text) < 0.1 || Convert.ToDouble(tbTijdsduur2.Text) < 0.1 || Convert.ToDouble(tbTijdsduur3.Text) < 0.1 || Convert.ToDouble(tbTijdsduur4.Text) < 0.1 || Convert.ToDouble(tbTijdsduur5.Text) < 0.1)
            {
                MessageBox.Show("De tijdsduur van een Activiteit mag niet lager zijn dan 0,1 HTE", "Waarschuwing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }*/
            else
            {
                HotelEventManager.HTE_Factor = (float)Convert.ToDouble(tbHTE.Text.Replace('.', ','));
                DialogResult = DialogResult.OK;
            }
            
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
