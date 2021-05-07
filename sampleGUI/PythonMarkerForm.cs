using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronPython.Hosting;

namespace sampleGUI
{
    public partial class PythonMarkerForm : Form
    {
        public PythonMarkerForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            var py = Python.CreateEngine();


            try
            {

                py.ExecuteFile("C:\\Users\\Administrator\\source\\repos\\sampleGUI\\sampleGUI\\cortex.py");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            

//Bu da 4-5 kez basınca çalıştırıyor console fakat yine de hello world yazısı gelmiyor
System.Diagnostics.Process.Start(@"C:\\Users\\Administrator\\source\\repos\\sampleGUI\\sampleGUI\\marker.py");


        }
    }
}
