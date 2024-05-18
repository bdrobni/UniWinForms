using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace supermarket
{
    public partial class LoginForm : Form
    {
        SqlConnection conn = new SqlConnection(Konekcija.conString);       
        public LoginForm()
        {
            InitializeComponent();
            Console.WriteLine("login opened");
        }
        //This event fires when the user logs in
        public delegate void Notify();

        public event Notify LoginComplete;

        public virtual void OnLoginCompleted()
        {
            LoginComplete?.Invoke();
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(tbUsername.Text) && !string.IsNullOrEmpty(tbPassword.Text)) 
            { 
                try
                {
                    //We check whether the user's credentials match the ones in the database and, if they do, log them in
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("select * from Kasir where korisnickoime=@kime and lozinka=@pass", conn);
                    sqlCommand.Parameters.AddWithValue("kime", tbUsername.Text.ToString());
                    sqlCommand.Parameters.AddWithValue("pass", tbPassword.Text.ToString());
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    reader.Read();
                    if(reader.HasRows)
                    {
                        Kasir k = new Kasir(Int32.Parse(reader[0].ToString()), reader[3].ToString(), reader[4].ToString(), Int32.Parse(reader[5].ToString()));
                        if(k.Sifra!=0 && k.Ime!=null && k.Prezime!=null)
                        {   
                            Form1.ulogovani = k;                            
                            MessageBox.Show("Uspešno ste prijavljeni!", "Obaveštenje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Console.WriteLine("ulogovan");
                            OnLoginCompleted();
                        }              
                    }
                    else
                    {
                        MessageBox.Show("Došlo je do greške", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Console.WriteLine("nije ulogovan");
                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show("Logovanje nije uspelo " + ex.Message);               
                }
                finally { conn.Close(); }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
