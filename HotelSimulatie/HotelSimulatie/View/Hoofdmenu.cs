using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HotelSimulatie.View
{
    public partial class Hoofdmenu : Form
    {
        public Hoofdmenu()
        {
            InitializeComponent();
            try
            {
                LogoPbx.Load("http://i.imgur.com/5vQC9XM.png");
            }
            catch(System.Net.WebException ex)
            {
                Console.WriteLine("Logo cannot be found - " + ex);
                LogoPbx.Image = LogoPbx.ErrorImage;
                LogoPbx.SizeMode = PictureBoxSizeMode.Normal;
            }
            
        }

        private void quitBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
