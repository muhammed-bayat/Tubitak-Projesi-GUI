using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;

using System.Threading.Tasks;
using System.Windows.Forms;
namespace sampleGUI
{
    public partial class Baseline : Form
    {
        int minSec = 3;
        int sec = 15;

      
        bool baslat = false;

        SoundPlayer sound = new SoundPlayer("beep2.wav");

        SoundPlayer sound2 = new SoundPlayer("beep.wav");

        public Baseline()
        {
            InitializeComponent();
        }




        private void countDown_Tick(object sender, EventArgs e)
        {



            if (minSec != 0)
            {
                minSec = minSec - 1;
                lbl.Text = minSec.ToString();
                sound.PlaySync();
            }
              else 
                {
          
                Thread.Sleep(1000);

                pictureBox1.BackColor = Color.Transparent;
                lbl.BackColor = Color.Transparent;
                label1.BackColor = Color.Transparent;
                panel1.BackColor = Color.Transparent;

                lbl.Text = sec.ToString();
                sec = sec - 1;
        
            }

            if(sec <0)
            {
             
                countDown.Stop();
                  sound2.PlaySync(); 
                Console.WriteLine("buraya girdi");

                timer2.Start();
                minSec = 4;
                sec = 15;
            }


        

        }

 

        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;

            pictureBox2.Visible = true;

            pictureBox2.BackColor = Color.LightGray;
            lbl.BackColor = Color.LightGray;
            label1.BackColor = Color.LightGray;
            panel1.BackColor = Color.LightGray;
            if (minSec != 0)
            {    
                minSec = minSec - 1;
                lbl.Text = minSec.ToString();
                sound.PlaySync();
            }
            else
            {
                Thread.Sleep(1000);



                pictureBox2.BackColor = Color.Transparent;
                lbl.BackColor = Color.Transparent;
                label1.BackColor = Color.Transparent;
                panel1.BackColor = Color.Transparent;

                lbl.Text = sec.ToString();
                sec = sec - 1;

            }

            if (sec < 0)
            {
                sound2.PlaySync();
                countDown.Stop();
                Console.WriteLine("buraya girdi");

                timer2.Stop();
            }

        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            btnStart.Visible = false;

            pictureBox1.BackColor = Color.LightGray;
            lbl.BackColor = Color.LightGray;
            label1.BackColor = Color.LightGray;
            panel1.BackColor = Color.LightGray;
            countDown.Start();
        }
    }

}


