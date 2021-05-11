using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
namespace sampleGUI
{
    public partial class AnketForm : Form
    {
        public ChromiumWebBrowser chrome;

        public AnketForm()
        {
            InitializeComponent();

        }

    

        private void AnketForm_Load(object sender, EventArgs e)
        {
   

            this.WindowState = FormWindowState.Maximized;
            CefSettings settings = new CefSettings();
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            
            chrome = new ChromiumWebBrowser("https://forms.gle/GqFxJWvZmEbdWJ3Z6");
   
            this.panel1.Controls.Add(chrome);
            chrome.Dock = DockStyle.Fill;

        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            this.Text = e.Url.ToString() + "Loding Page..";
            label1.Text = e.Url.ToString() + "Loding Page..";
        }
        int sayac = 100;

        private void timer1_Tick(object sender, EventArgs e)
        {if (sayac == 0)
            {
                timer1.Stop();
              
            
                panel1.Controls.Clear();
                Baseline bsline = new Baseline();
                bsline.TopLevel = false;
                panel1.Controls.Add(bsline);
                bsline.Show();

                bsline.Dock = DockStyle.Fill;
                bsline.BringToFront();

            }

            label1.Text = "kalan zaman: "+ sayac.ToString();
            progressBar1.Value = sayac;
            sayac--;

        }
 

        private void button1_Click_1(object sender, EventArgs e)
        {
            timer1.Start();
            button1.Visible = false;

        }
    }
}
