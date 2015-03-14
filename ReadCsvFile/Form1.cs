using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadCsvFile
{
    public partial class Form1 : Form
    {
        private CsvToEEG EEG = new CsvToEEG();
        private Dictionary<String, double> m_Data = new Dictionary<string, double>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (EEG.readData(""))
            {
                m_Data = EEG.GetEEG;
                updateGrafe(EEG.GetEEG);
            }
            try
            {
                String dataTime = DateTime.Now.ToShortDateString() + " | " + DateTime.Now.ToString("h:mm:ss tt") + "  ||\t";
                listBox1.Items.Add(dataTime);
                foreach (String key in m_Data.Keys)
                {
                    listBox1.Items.Add(key + ":\t" + m_Data[key]);
                }
                listBox1.Items.Add("-----------------------------------\n");
                int visibleItems = listBox1.ClientSize.Height / listBox1.ItemHeight;
                listBox1.TopIndex = Math.Max(listBox1.Items.Count - visibleItems + 1, 0);
            }
            catch (Exception)
            {
            }            
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

        private void updateGrafe(Dictionary<String, double> i_Data)
        {
            try
            {
                e1_lable.Text = i_Data["e1"].ToString("0.##");
                e2_lable.Text = i_Data["e2"].ToString("0.##");
                e3_lable.Text = i_Data["e3"].ToString("0.##");
                h1_lable.Text = i_Data["h1"].ToString("0.##");
                h2_lable.Text = i_Data["h2"].ToString("0.##");
                theta_lable.Text = i_Data["theta"].ToString("0.##");

                SetPicBoxHeight(e1_PB, clculateHeight(i_Data["e1"]));
                SetPicBoxHeight(e2_PB, clculateHeight(i_Data["e2"]));
                SetPicBoxHeight(e3_PB, clculateHeight(i_Data["e3"]));
                SetPicBoxHeight(h1_PB, clculateHeight(i_Data["h1"]));
                SetPicBoxHeight(h2_PB, clculateHeight(i_Data["h2"]));
                SetPicBoxHeight(theta_PB, clculateHeight(i_Data["theta"]));
            }
            catch (Exception)
            {
            }
            
        }
        
        private int clculateHeight(double i_Val)
        {
            return(int)(((i_Val + 6) / 12) * 100);
        }

        private delegate void SetPicBoxHeightDelegatePictureBox(PictureBox picBox, int nHeight);
        private void SetPicBoxHeight(PictureBox picBox, int nHeight)
        {
            Invoke(new SetPicBoxHeightDelegatePictureBox(
            delegate
            {
                if (nHeight < 0 || nHeight > 100)
                    return;

                picBox.Location = new Point(picBox.Location.X, picBox.Location.Y - nHeight + picBox.Height);
                picBox.Height = nHeight;
            }
            ), picBox, nHeight);
        }
    }
}