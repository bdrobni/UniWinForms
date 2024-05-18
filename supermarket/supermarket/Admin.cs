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
    public partial class Admin : Form
    {
        SqlConnection conn = new SqlConnection(Konekcija.conString);
        int selecteditemcode = -1;

        public delegate void Notify();

        public event Notify TablesChanged;
        //This event fires when changes occur in the database, allowing the table on the main form to be updated in real time
        public virtual void OnTablesChanged()
        {
            TablesChanged?.Invoke();
        }
        public Admin()
        {
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'radnjaDataSet.Artikal' table. You can move, or remove it, as needed.
            this.artikalTableAdapter.Fill(this.radnjaDataSet.Artikal);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                int selectedrow = dataGridView1.SelectedRows[0].Index;
                int id = int.Parse(dataGridView1[0, selectedrow].Value.ToString());
                string name = dataGridView1[1, selectedrow].Value.ToString();
                int barcode = int.Parse(dataGridView1[2, selectedrow].Value.ToString());
                int price = int.Parse(dataGridView1[3, selectedrow].Value.ToString());

                try
                {
                    this.artikalTableAdapter.Delete(id, name, barcode, price);
                    dataGridView1.Rows.RemoveAt(selectedrow);
                    OnTablesChanged();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //This function populates the text boxes with the details of an existing item, allowing us to edit it,
            //while also setting the variable to identify it for subsequently updating the database
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedrow = dataGridView1.SelectedRows[0].Index;
                int id = int.Parse(dataGridView1[0, selectedrow].Value.ToString());
                string name = dataGridView1[1, selectedrow].Value.ToString();
                int barcode = int.Parse(dataGridView1[2, selectedrow].Value.ToString());
                int price = int.Parse(dataGridView1[3, selectedrow].Value.ToString());

                selecteditemcode = id;

                tbName.Text = name;
                tbBarcode.Text = barcode.ToString();
                tbPrice.Text = price.ToString();
            }
        }

        private void btnSaveAdd_Click(object sender, EventArgs e)
        {
            //In order for the same UI to be usable for both adding new items and editing existing ones, we first check whether a
            //selectedItem exists (i.e. an existing item is being edited) and if it doesn't, insert a new item, otherwise we 
            //edit the item with an ID matching the selected item
            int sifra;
            string name = tbName.Text.ToString();
            int barcode = int.Parse(tbBarcode.Text.ToString());
            int price = int.Parse(tbPrice.Text.ToString());

            if(selecteditemcode == -1)
            {
                sifra = dataGridView1.Rows.Count;
                try
                {
                    this.artikalTableAdapter.Insert(sifra, name, barcode, price);
                    this.artikalTableAdapter.Fill(this.radnjaDataSet.Artikal);
                    OnTablesChanged();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                sifra = selecteditemcode;
                try
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand("update artikal set naziv = @naziv, barkod = @barkod, cena=@cena where sifra = @sifra", conn);
                    sqlCommand.Parameters.AddWithValue("naziv", name);
                    sqlCommand.Parameters.AddWithValue("barkod", barcode);
                    sqlCommand.Parameters.AddWithValue("cena", price);
                    sqlCommand.Parameters.AddWithValue("sifra", sifra);
                    sqlCommand.ExecuteNonQuery();

                    this.artikalTableAdapter.Fill(this.radnjaDataSet.Artikal);
                    OnTablesChanged();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
