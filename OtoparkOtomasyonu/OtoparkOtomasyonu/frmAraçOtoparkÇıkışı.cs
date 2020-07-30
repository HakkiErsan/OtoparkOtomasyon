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
    public partial class frmAraçOtoparkÇıkışı : Form   
    {
        DateTime tarih;
        
        public frmAraçOtoparkÇıkışı()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source = DESKTOP-KKRCQCS\\SQL2019; Initial Catalog = Koç_Otopark; Integrated Security = True");
        private void frmAraçOtoparkÇıkışı_Load(object sender, EventArgs e)
        {
            DoluYerler();
            Plakalar();
          
            timer1.Enabled = true;
            lblTL.Visible = false;
        }


    

        private List<int> TarifeOku()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT* FROM tarifeler WHERE musteriler = '1'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            List<int> veriList = new List<int>();
            while (read.Read())
            {
                for (int i = 0; i < read.FieldCount; i++)
                {
                    veriList.Add(read.GetInt32(i));
                }
            }
            baglanti.Close();
            return veriList;
        }





        private void Plakalar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from Araç_Otopark_Kaydı", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboPlaka.Items.Add(read["plaka"].ToString());
            }
            baglanti.Close();
        }

        private void DoluYerler()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from AraçDurumu where durumu='DOLU'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboParkYeri.Items.Add(read["parkyeri"].ToString());
            }
            baglanti.Close();
        }

        private void comboPlaka_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from Araç_Otopark_Kaydı where plaka='"+comboPlaka.SelectedItem+"'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtParkYeri.Text = read["parkyeri"].ToString();
            }
            baglanti.Close();
        }

        private void comboParkYeri_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from Araç_Otopark_Kaydı where parkyeri='" + comboParkYeri.SelectedItem + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtPlakaYeri2.Text = read["parkyeri"].ToString();
                txtTc.Text = read["tc"].ToString();
                txtAd.Text = read["ad"].ToString();
                txtSoyad.Text = read["soyad"].ToString();
                txtMarka.Text = read["marka"].ToString();
                txtSeri.Text = read["seri"].ToString();
                txtRenk.Text = read["renk"].ToString();
                txtPlaka.Text = read["plaka"].ToString();
                lblGelişTarihi.Text = read["tarih"].ToString();
                txtTel.Text = read["telefon"].ToString();
            }
            baglanti.Close();

           
            DateTime geliş, çıkış;
           geliş = DateTime.Parse(lblGelişTarihi.Text);
           çıkış = DateTime.Parse(lblÇıkışTarihi.Text);
            TimeSpan fark,zaman;
            fark = çıkış - geliş;
            lblSüre.Text = fark.TotalHours.ToString("0.00");
           
            double farki = double.Parse(lblSüre.Text);
            DateTime sondeger = DateTime.Now;
            zaman = sondeger.Subtract(tarih);
            
            
            List<int> tarife = TarifeOku();

            if (farki < 1)
            {
                lblToplamTutar.Text = (tarife[0]).ToString();
                lblTL.Visible = true;


            }
            else if (farki > 1 && farki < 3)
            {
                lblToplamTutar.Text = tarife[1].ToString() ;
                lblTL.Visible = true;

            }
            else if (farki > 3 && farki < 6)
            {
                lblToplamTutar.Text = tarife[2].ToString() ;
                lblTL.Visible = true;
            }
            else if (farki > 6 && farki < 12)
            {
                lblToplamTutar.Text = tarife[3].ToString() ;
                lblTL.Visible = true;

            }
            else if (farki > 12 && farki < 24)
            {
                lblToplamTutar.Text = tarife[4].ToString() ;
                lblTL.Visible = true;

            }
            else if (farki > 24 && farki < 48)
            {
                lblToplamTutar.Text = tarife[5].ToString() ;
                lblTL.Visible = true;
            }
            else if (farki > 48 && farki < 72)
            {
                lblToplamTutar.Text = tarife[6].ToString() ;
                lblTL.Visible = true;

            }
            else
            {
                lblToplamTutar.Text = tarife[7].ToString() ;
                lblTL.Visible = true;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblÇıkışTarihi.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Araç_Otopark_Kaydı where plaka='"+txtPlaka.Text+"'", baglanti);
            komut.ExecuteNonQuery();

            SqlCommand komut2 = new SqlCommand("update AraçDurumu set durumu='BOŞ' where parkyeri='" + txtPlakaYeri2.Text + "'", baglanti);
            komut2.ExecuteNonQuery();

            SqlCommand komut3 = new SqlCommand("INSERT INTO satis(parkyeri,plaka,geliş_tarihi,çıkış_tarihi,süre,tutar) values(@parkyeri,@plaka,@geliş_tarihi,@çıkış_tarihi,@süre,@tutar)", baglanti);

            komut3.Parameters.AddWithValue("@parkyeri", txtPlakaYeri2.Text);
            komut3.Parameters.AddWithValue("@plaka", txtPlaka.Text);
            komut3.Parameters.AddWithValue("@geliş_tarihi", lblGelişTarihi.Text);
            komut3.Parameters.AddWithValue("@çıkış_tarihi", lblÇıkışTarihi.Text);
            komut3.Parameters.AddWithValue("@süre", double.Parse(lblSüre.Text));
            komut3.Parameters.AddWithValue("@tutar", double.Parse(lblToplamTutar.Text));

            komut3.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Araç Çıkışı Yapıldı...");
            foreach(Control item in groupBox2.Controls)
            {
                if(item is TextBox)
                {
                    item.Text = "";
                    txtParkYeri.Text = "";
                    comboParkYeri.Text = "";
                    comboPlaka.Text = "";

                }
            }

            comboPlaka.Items.Clear();
            comboParkYeri.Items.Clear();
            lblGelişTarihi.Text = "";
            lblÇıkışTarihi.Text = "";
            lblToplamTutar.Text = "";
            lblSüre.Text = "";
            lblTL.Text = "";
            DoluYerler();
            Plakalar();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboPlaka.Text = "";
            comboParkYeri.Text = "";
            txtParkYeri.Clear();
            txtTc.Clear();
            txtPlakaYeri2.Clear();
            txtAd.Clear();
            txtSoyad.Clear(); 
            txtRenk.Clear();
            txtMarka.Clear();
            txtSeri.Clear();
            txtPlaka.Clear();
            lblGelişTarihi.Text = "";
            lblÇıkışTarihi.Text = "";
            lblToplamTutar.Text = "";
            lblSüre.Text = "";
            txtTel.Clear();
            lblTL.Text = "";
            

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            FrmTarifeGuncelle tarifeyiguncelle = new FrmTarifeGuncelle();
                tarifeyiguncelle.ShowDialog();
           
        }
    }
}
