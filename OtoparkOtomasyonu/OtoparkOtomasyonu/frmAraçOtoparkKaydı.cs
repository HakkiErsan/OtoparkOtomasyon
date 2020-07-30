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

namespace OtoparkOtomasyonu
{
    public partial class frmAraçOtoparkKaydı : Form
    {
        public frmAraçOtoparkKaydı()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = DESKTOP-KKRCQCS\\SQL2019; Initial Catalog = Koç_Otopark; Integrated Security = True");
        private void frmAraçOtoparkKaydı_Load(object sender, EventArgs e)
        {
            BoşAraçlar();
            Marka();
     
        }

        private void Marka()
        {
            
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka from Marka_Bilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboMarka.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void BoşAraçlar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from AraçDurumu WHERE durumu='BOŞ'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboParkYeri.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtTelefon.Clear();
            txtTC.Clear();
            txtAd.Clear();
            txtSoyad.Clear();
            txtEmail.Clear();
            txtPlakam.Clear();
            txtRenk.Clear();
            comboMarka.Text = "";
            comboSeri.Text = "";
            comboParkYeri.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO [Araç_Otopark_Kaydı](tc,ad,soyad,telefon,email,plaka,marka,seri,renk,parkyeri,tarih) values(@tc,@ad,@soyad,@telefon,@email,@plaka,@marka,@seri,@renk,@parkyeri,@tarih)", baglanti);
            komut.Parameters.AddWithValue("@telefon",txtTelefon.Text);
            komut.Parameters.AddWithValue("@tc", txtTC.Text);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@email", txtEmail.Text);
            komut.Parameters.AddWithValue("@plaka", txtPlakam.Text);
            komut.Parameters.AddWithValue("@marka", comboMarka.Text);
            komut.Parameters.AddWithValue("@seri", comboSeri.Text);
            komut.Parameters.AddWithValue("@renk", txtRenk.Text);
            komut.Parameters.AddWithValue("@parkyeri", comboParkYeri.Text);
            komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
            
            komut.ExecuteNonQuery();

            SqlCommand komut2 = new SqlCommand("update AraçDurumu set durumu='DOLU' where parkyeri='"+comboParkYeri.SelectedItem+"'",baglanti);
            komut2.ExecuteNonQuery();


            baglanti.Close();
            MessageBox.Show("Araç Kaydı gerçekleştirildi...", "Kayıt");
            comboParkYeri.Items.Clear();
            BoşAraçlar();

            comboMarka.Items.Clear();
            Marka();
            comboSeri.Items.Clear();

            foreach(Control item in grupKişi.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in grupAraç.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            foreach (Control item in grupAraç.Controls)
            {
                if (item is ComboBox)
                {
                    item.Text = "";
                }
            }
        }

        private void btnMarka_Click(object sender, EventArgs e)
        {
            FrmMarka marka = new FrmMarka();
            marka.ShowDialog();
        }

        private void btnSeri_Click(object sender, EventArgs e)
        {
            FrmSeri seri = new FrmSeri();
            seri.ShowDialog();
        }

        private void comboMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboSeri.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka,seri from Seri_Bilgileri where marka='"+comboMarka.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while(read.Read())
            {
                comboSeri.Items.Add(read["seri"].ToString());

            }
            baglanti.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
