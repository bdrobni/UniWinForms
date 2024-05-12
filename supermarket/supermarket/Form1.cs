using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace supermarket
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(Konekcija.conString);
        BindingList<Artikal> artikli = new BindingList<Artikal>();
        public static Kasir ulogovani = null;
        public Popust popust = new Popust();
        int indexnumber;
        public float FindTotal(BindingList<Artikal> artikli)
        {
            float total = 0;
            foreach(Artikal a in artikli)
            {
                total += a.Cena * a.BrStavki;
            }

            return total;
        }

        public void CheckAddArtikal(Artikal art, BindingList<Artikal> artikli)
        {
            foreach(Artikal a in artikli)
            {
                if (art.Sifra == a.Sifra)
                {
                    a.BrStavki += 1;
                    break;
                }
                else
                {
                    if (popust.SifraArtikla == art.Sifra)
                    { art.Cena *= popust.Kolicina; }
                    artikli.Add(art);
                    break;
                }
            }
        }
        public void DoLogin()
        {
            Console.WriteLine("potvrda");
            tbKasir.Text = ulogovani.ToString();
            btnAddItem.Enabled = true;
            btnAddByCode.Enabled = true;
            btnPayment.Enabled = true;
            btnReset.Enabled = true;
            btnRemoveItem.Enabled = true;
            btnAddBonus.Enabled = true;
            btnLogout.Enabled = true;
            btnLogin.Enabled = false;
        }
        public Form1()
        {
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'radnjaDataSet.Artikal' table. You can move, or remove it, as needed.
            this.artikalTableAdapter.Fill(this.radnjaDataSet.Artikal);
            listBox1.DataSource = artikli;
            listBox1.ValueMember = ToString();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            Artikal a = new Artikal();
            a.Sifra = Int32.Parse(dataGridView1.SelectedRows[0].ToString());
            a.Naziv = dataGridView1.SelectedRows[1].ToString();
            a.Barkod = Int32.Parse(dataGridView1.SelectedRows[2].ToString());
            a.Cena = Int32.Parse(dataGridView1.SelectedRows[3].ToString());
            a.BrStavki = 1;

            CheckAddArtikal(a, artikli);
        }

        private void btnAddByCode_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbBarcode.Text))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("select * from Artikal where barkod = @barkod", conn);
                    sqlCommand.Parameters.AddWithValue("barkod", Int32.Parse(tbBarcode.Text.ToString()));
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        Artikal a = new Artikal(Int32.Parse(reader[0].ToString()), reader[1].ToString(), Int32.Parse(reader[2].ToString()), float.Parse(reader[3].ToString()));
                        a.BrStavki = 1;
                        if (a!=null)
                        {
                            CheckAddArtikal(a, artikli);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Došlo je do greške", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Error);                       
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { conn.Close(); }
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (artikli.Count > 0)
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("select count(*) from Racun", conn);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        indexnumber = Int32.Parse(reader[0].ToString()) + 1;
                    }
                    else
                    {
                        MessageBox.Show("Došlo je do greške", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { conn.Close(); }

                try
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("insert into racun(sifra,vreme,ukupno) values (@sifra,@vreme,@ukupno)", conn);
                    sqlCommand.Parameters.AddWithValue("sifra", indexnumber);
                    sqlCommand.Parameters.AddWithValue("vreme", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("ukupno", FindTotal(artikli));
                    sqlCommand.ExecuteNonQuery();
                    foreach(Artikal a in artikli)
                    {
                        SqlCommand innerCommand = new SqlCommand("insert into Stavka(sifracuna,sifartikla,brkomada) values (@sifrarac,@sifraart,@brkom)", conn);
                        innerCommand.Parameters.AddWithValue("sifrarac", indexnumber);
                        innerCommand.Parameters.AddWithValue("sifraart", a.Sifra);
                        innerCommand.Parameters.AddWithValue("brkom", a.BrStavki);
                        innerCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { conn.Close(); }

                artikli.Clear();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            artikli.Clear();
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            artikli.RemoveAt(listBox1.SelectedIndex);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            if(login.DialogResult == DialogResult.OK && ulogovani!=null)
            { Console.WriteLine("dialog_ok"); }
            login.LoginComplete += DoLogin;                       
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            ulogovani = null;
            MessageBox.Show("Odjavljeni ste sa kase", "Odjava", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnAddItem.Enabled = false;
            btnAddByCode.Enabled = false;
            btnPayment.Enabled = false;
            btnReset.Enabled = false;
            btnRemoveItem.Enabled = false;
            btnAddBonus.Enabled = false;
            btnLogout.Enabled = false;
            btnLogin.Enabled = true;
        }

        private void btnAddBonus_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbBonus.Text))
            {
                try
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("select * from popust where sifra = @sifra", conn);
                    sqlCommand.Parameters.AddWithValue("sifra", Int32.Parse(tbBonus.Text.ToString()));
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        Popust p = new Popust();
                        p.Sifra = Int32.Parse(reader[0].ToString());
                        p.Opis = reader[1].ToString();
                        p.Kolicina = Int32.Parse(reader[2].ToString());
                        p.SifraArtikla = Int32.Parse(reader[3].ToString());
                        popust = p;
                    }
                    else
                    {
                        MessageBox.Show("Došlo je do greške", "Obavestenje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { conn.Close(); }
            }
        }
    }
}
