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

namespace pythondeneme
{
    public partial class Admin : MetroSetForm
    {
        public Admin()
        {
            InitializeComponent();
        }

        string myConnectionString = "Server=localhost;Database=python;uid=root;pwd=2469";
        MySqlConnection connection;
        private void Admin_Load(object sender, EventArgs e)
        {
            connection = new MySqlConnection(myConnectionString);
            veriListele();
        }

        public void veriListele()
        {
            connection.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from users", connection);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
            connection.Close();
        }

        public void veriEkleme()
        {
            MySqlCommand command = new MySqlCommand("insert into users(username, useremail, userpassword, userstartday, userendday) values ('"+txtUsername.Text+"'," +
                "'"+txtMail.Text+"','"+txtPassword.Text+"','"+dtpStart.Text+"','"+dtpEnd.Text+"')",connection);
            connection.Open();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Kayıt başarıyla eklendi !", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kayıt eklenemedi !", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        public void veriGuncelleme()
        {
            MySqlCommand command = new MySqlCommand("update users set username='" + txtUsername.Text + "', useremail='" + txtMail.Text + "', userpassword='" + txtPassword.Text + "'," +
            "userstartday='" + dtpStart.Text + "', userendday='" + dtpEnd.Text + "' where id = '"+label7.Text+"'", connection);
            connection.Open();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Kayıt başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kayıt güncellenemedi !", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                connection.Close();
        }

        public void veriSilme()
        {
            MySqlCommand command = new MySqlCommand("delete from users where id='" + label7.Text + "'", connection);
            connection.Open();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Kayıt başarıyla silindi !", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kayıt silinemedi !", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            label7.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtUsername.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtMail.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtPassword.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dtpStart.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            dtpEnd.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            veriEkleme();
            veriListele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Bu kayıtı silmek istediğinize emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                veriSilme();
                veriListele();
            }
            else
            {
                return;
            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            veriGuncelleme();
            veriListele();
        }
    }
}
