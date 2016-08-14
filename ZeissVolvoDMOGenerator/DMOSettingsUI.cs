using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZeissVolvoDMOGenerator
{
    public partial class DMOSettingsUI : UserControl
    {
        DMOSettingsClass dmosc;
        public DMOSettingsClass DMOSC
        {
            get
            {
                if (dmosc == null)
                    dmosc = new DMOSettingsClass();
                dmosc.PR = textBox1.Text;
                dmosc.LI = textBox2.Text;
                dmosc.PL = textBox3.Text;
                dmosc.PN = textBox4.Text;
                dmosc.PS = textBox5.Text;
                dmosc.Q = textBox6.Text;
                return dmosc;
            }
            set
            {
                dmosc = value;
                textBox1.Text = dmosc.PR;
                textBox2.Text = dmosc.LI;
                textBox3.Text = dmosc.PL;
                textBox4.Text = dmosc.PN;
                textBox5.Text = dmosc.PS;
                textBox6.Text = dmosc.Q;
            }

        }
        public DMOSettingsUI()
        {
            InitializeComponent();
        }

        internal void ResetContent()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "ThyssenKrupp/Shanghai";
            textBox5.Text = "";
            textBox6.Text = "";

        }
    }
}
