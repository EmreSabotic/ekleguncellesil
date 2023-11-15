using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ekleguncellesil
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        public Form1()
        {
            InitializeComponent();
        }

        void MusteriGetir ()
        {

            baglanti= new SqlConnection ("server=.;Initial Catalog=exampleprojects;Integrated Security=SSPI");
            baglanti.Open ();
            da = new SqlDataAdapter("SELECT *FROM t_Students", baglanti);
            DataTable tablo = new DataTable ();
            da.Fill (tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close ();




        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MusteriGetir ();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtNo.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells["Surname"].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells["BirthdayDate"].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells["Phone"].Value.ToString();

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO t_Students(Name,Surname,BirthdayDate,Phone) VALUES (@Name,@Surname,@BirthdayDate,@Phone)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@Name", txtAd.Text);
            komut.Parameters.AddWithValue("@Surname", txtSoyad.Text);
            komut.Parameters.AddWithValue("@BirthdayDate", dateTimePicker1.Value.Date);
            komut.Parameters.AddWithValue("@Phone", txtTelefon.Text);
            baglanti.Open(); 
            komut.ExecuteNonQuery();
            MusteriGetir();
            baglanti.Close();
           
            


        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            string sorgu = " DELETE FROM t_Students WHERE ID=@ID";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@ID",Convert.ToInt32(txtNo.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            MusteriGetir();
            baglanti.Close();


        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE t_Students SET Name=@Name, Surname=@Surname, BirthdayDate=@BirthdayDate, Phone=@Phone WHERE ID=@ID";
            komut = new SqlCommand (sorgu, baglanti);

            komut.Parameters.AddWithValue("@ID", Convert.ToInt32(txtNo.Text));
            komut.Parameters.AddWithValue("@Name", txtAd.Text);
            komut.Parameters.AddWithValue("@Surname", txtSoyad.Text);
            komut.Parameters.AddWithValue("@BirthdayDate", dateTimePicker1.Value);
            komut.Parameters.AddWithValue("@Phone", Convert.ToInt64(txtTelefon.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            
            baglanti.Close();
            MusteriGetir();









            }
    }
}
