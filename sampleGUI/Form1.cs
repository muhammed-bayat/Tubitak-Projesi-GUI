using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sampleGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

     

       

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {

            /// panel içinde baslat

            panelOrta.Controls.Clear();
            AnketForm anketForm = new AnketForm();
            anketForm.TopLevel = false;
            panelOrta.Controls.Add(anketForm);
            anketForm.Show();

            anketForm.Dock = DockStyle.Fill;
            anketForm.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelOrta.Controls.Clear();
            PythonMarkerForm pythonMarker = new PythonMarkerForm();
            pythonMarker.TopLevel = false;
            panelOrta.Controls.Add(pythonMarker);
            pythonMarker.Show();

            pythonMarker.Dock = DockStyle.Fill;
            pythonMarker.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /// panel içinde baslat

            panelOrta.Controls.Clear();
            Baseline bsline = new Baseline();
            bsline.TopLevel = false;
            panelOrta.Controls.Add(bsline);
            bsline.Show();

            bsline.Dock = DockStyle.Fill;
            bsline.BringToFront();

            /////////////////////////////////

            altPanel.BackColor = Color.Transparent;
            ustPanel.BackColor = Color.Transparent;

        }
    }
}
