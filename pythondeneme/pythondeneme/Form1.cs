using MetroSet_UI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace pythondeneme
{
    public partial class Form1 : MetroSetForm
    {

        public Form1()
        {
            InitializeComponent();
        }

        string myConnectionString = "Server=localhost;Database=python;uid=root;pwd=2469";
        MySqlConnection connection;
        
        MySqlDataReader reader;

        private void Form1_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '*';
        }            

        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new MySqlConnection(myConnectionString);
                connection.Open();
                MySqlCommand command;
                command = new MySqlCommand("select * from users where username = '" + txtUsername.Text + "'", connection);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader["id"].ToString() == "1" && reader["username"].ToString() == txtUsername.Text && reader["userpassword"].ToString() == txtPassword.Text)
                        {
                            this.Hide();
                            Admin admin = new Admin();
                            admin.Closed += (s, args) => this.Close();
                            admin.Show();

                        }
                        else if (txtPassword.Text == reader["userpassword"].ToString() && txtUsername.Text == reader["username"].ToString())
                        {
                            string baslangic = reader["userstartday"].ToString();
                            DateTime baslangic1 = Convert.ToDateTime(baslangic);
                            string abonelik = reader["userendday"].ToString();
                            DateTime abonelik1 = Convert.ToDateTime(abonelik);
                            this.Hide();
                            Form2 form2 = new Form2();
                            form2.username = reader["username"].ToString();
                            form2.email = reader["useremail"].ToString();
                            form2.tarih = baslangic1;
                            form2.uye = abonelik1;
                            form2.Closed += (s, args) => this.Close();
                            form2.Show();
                        }
                        else
                        {
                            MessageBox.Show("şifreniz yanlış !");
                        }
                    }        
                }
                else
                {
                    MessageBox.Show("Bir hata oluştu !");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
            
        }
    }
}
