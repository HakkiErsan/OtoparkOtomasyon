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

namespace OtoparkOtomasyonu
{
    public partial class frmAraçOtoparkYerleri : Form
    {
        public frmAraçOtoparkYerleri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = DESKTOP-KKRCQCS\\SQL2019; Initial Catalog = Koç_Otopark; Integrated Security = True");

        private void frmAraçOtoparkYerleri_Load(object sender, EventArgs e)
        {
            
            
            Plaka();
            DoluParkYerleri();
            

        }
            private void Plaka()
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select parkyeri,plaka from Araç_Otopark_Kaydı", baglanti);
                SqlDataReader read = komut.ExecuteReader();
                while (read.Read())
                {
                    foreach (Control item in this.Controls)
                    {
                        if (item is Button)
                        {
                            if (item.Text == read["parkyeri"].ToString())
                            {
                                item.Text = read["plaka"].ToString();
                    
                            }
                      
                        

                        }
                    }
                }
                baglanti.Close();
            }
        

        private void DoluParkYerleri()
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select* from AraçDurumu", baglanti);
            SqlDataReader oku = komut2.ExecuteReader();
            while (oku.Read())
            {

                foreach (Control item in this.Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text == oku["parkyeri"].ToString())
                        {
                            item.BackColor = Color.Gray;

                        }
                        


                    }
                }
            }
            baglanti.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
    }
}