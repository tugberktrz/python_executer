using MetroSet_UI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using pythondeneme;

namespace pythondeneme
{

    public partial class Form2 : MetroSetForm
    {
        public Form2()
        {
            InitializeComponent();
        }
        //datetime.now.tostring() tarih ve saat 
        public string username { get; set; }
        public string email { get; set; }
        public DateTime tarih {get ; set;}
        public DateTime uye { get; set; }

        public void Form2_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Title = "Dosya Seç";
            try
            {
                openFileDialog1.ShowDialog();
                string FileName = openFileDialog1.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            DateTime bugun = DateTime.Now;
            TimeSpan t = uye - bugun;
            label5.Text = username;
            label6.Text = email;
            label7.Text = tarih.ToString();
            label8.Text = string.Format("{0} Gün, {1} Saat, {2} Dakika, {3} Saniye ", t.Days, t.Hours, t.Minutes, t.Seconds);
            if (bugun > uye)
            {
                label8.Text = "Üyeliğiniz sona erdi !";
                button1.Enabled = false;
            }
        }
    }
}
