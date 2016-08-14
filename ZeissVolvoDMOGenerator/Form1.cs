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
using System.IO;
using System.Xml.Serialization;

namespace ZeissVolvoDMOGenerator
{
    public partial class Form1 : Form
    {
        dic_data_class dictionary_DMOSettings;
        CalypsoDPResult DPResult;
        public Form1()
        {
            InitializeComponent();
            ParseDMOSettings();


        }

        private void ParseDMOSettings()
        {
            string path = Properties.Settings.Default.DMOSettingFile;
            FileInfo fi = new FileInfo(path);
            var sr = fi.Open(FileMode.OpenOrCreate);
            try
            {


                //声明序列化对象实例serializer
                XmlSerializer serializer = new XmlSerializer(typeof(dic_data_class));
                //反序列化，并将反序列化结果值赋给变量i
                dictionary_DMOSettings = serializer.Deserialize(sr) as dic_data_class;
            }
            catch (System.Exception ex)
            {
                dictionary_DMOSettings = new dic_data_class();
            }
            finally
            {
                sr.Close();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = Properties.Settings.Default.DPFolder;

            //Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            string old_path = label1.Text;
            if (System.IO.Directory.Exists(old_path))
            {
                fbd.SelectedPath = old_path;
            }

            var j = fbd.ShowDialog();
            if (j == DialogResult.OK && System.IO.Directory.Exists(fbd.SelectedPath))
            {
                label1.Text = fbd.SelectedPath;
                Properties.Settings.Default.DPFolder = fbd.SelectedPath;
                Properties.Settings.Default.Save();
            }

        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
            refreshList();
        }

        private void refreshList()
        {
            string path = label1.Text;
            string filter_text = filterControl1.FilterString;
            DirectoryInfo di = new DirectoryInfo(path);
            var file_list = di.GetFiles("*.dp").Select(n => n.Name).ToList();
            var filtered_list = from u in file_list
                                where u.Contains(filter_text)
                                select u;
            listBox1.DataSource = filtered_list.ToList();

        }

        private void filterControl1_FilterStringUpdated(object sender, SeanUserControl.FilterStringEventArgs fsArgs)
        {
            refreshList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string file_name = listBox1.SelectedItem.ToString();
            string file_path = System.IO.Path.Combine(label1.Text, file_name);
            var test = new DPImporter(file_path);
            DPResult = test.DPResult;

            UpdateSettingUI();
            //var output = VolvoDMOResult.ParseCalypsoDP(test.DPResult);
            //System.IO.StreamWriter sw = new System.IO.StreamWriter(Properties.Settings.Default.DMOOutputFile);
            //sw.WriteLine(output.ToString());
            //sw.Close();
        }

        private void UpdateSettingUI()
        {
            string planid = DPResult.PLANID;

            if (dictionary_DMOSettings.Keys.Contains(planid))
            {
                DMOSettingsClass dsc = dictionary_DMOSettings[planid];
                dmoSettingsUI1.DMOSC = dsc;
            }
            else
            {
                dmoSettingsUI1.ResetContent();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string planid = DPResult.PLANID;
            var output = VolvoDMOResult.ParseCalypsoDP(DPResult);
            DMOSettingsClass dmosc = new DMOSettingsClass()
            {
                PLANID = planid,
                PR = dmoSettingsUI1.DMOSC.PR,
                LI = dmoSettingsUI1.DMOSC.LI,
                PL = dmoSettingsUI1.DMOSC.PL,
                PN = dmoSettingsUI1.DMOSC.PN,
                PS = dmoSettingsUI1.DMOSC.PS,
                Q = dmoSettingsUI1.DMOSC.Q
            };

            output.PR = dmosc.PR;
            output.LI = dmosc.LI;
            output.PL = dmosc.PL;
            output.PN = dmosc.PN;
            output.PS = dmosc.PS;
            output.Q = dmosc.Q;

            if (dictionary_DMOSettings.Keys.Contains(planid))
            {
                dictionary_DMOSettings.Remove(planid);
            }
            dictionary_DMOSettings.Add(dmosc);
            saveDMOSettingstoFile();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = Properties.Settings.Default.DMOOutputPath;
            sfd.Filter = "dmo file（*.dmo）|*.dmo|all file（*.*）|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName);
                sw.WriteLine(output.ToString());
                sw.Close();
            }

        }

        private void saveDMOSettingstoFile()
        {
            StreamWriter sw = new StreamWriter(Properties.Settings.Default.DMOSettingFile);
            XmlSerializer serializer = new XmlSerializer(typeof(dic_data_class));
            serializer.Serialize(sw, dictionary_DMOSettings);
            sw.Close();
        }
    }
}
