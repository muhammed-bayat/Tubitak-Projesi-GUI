using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        private void run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "D:/qt5hatasi/portable/python/python.exe";
            start.Arguments = string.Format("{0} {1}", cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {


            var py = Python.CreateEngine();


            try
            {

                run_cmd("D:/qt5hatasi/portable/python/python.exe", "C:/Users/Administrator/source/repos/sampleGUI/sampleGUI/marker.py");
                // py.ExecuteFile("C:\\Users\\Administrator\\source\\repos\\sampleGUI\\sampleGUI\\marker.py");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            

//System.Diagnostics.Process.Start(@"C:\\Users\\Administrator\\source\\repos\\sampleGUI\\sampleGUI\\marker.py");


        }
    }
}
