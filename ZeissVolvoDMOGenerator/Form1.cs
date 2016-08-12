using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZeissImporter;
using VolvoDMOOutput;

namespace ZeissVolvoDMOGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var test = new DPImporter(Properties.Settings.Default.DPFile);
            var output = VolvoDMOResult.ParseCalypsoDP(test.DPResult);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Properties.Settings.Default.DMOOutputFile);
            sw.WriteLine(output.ToString());
            sw.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Close();
        }
    }
}
