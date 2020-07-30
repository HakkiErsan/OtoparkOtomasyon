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
    public partial class FrmTarifeGuncelle : Form
    {
        public FrmTarifeGuncelle()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source = DESKTOP-KKRCQCS\\SQL2019; Initial Catalog = Koç_Otopark; Integrated Security = True");
  

        private void kulbutton_Click_1(object sender, EventArgs e)
        {  
      
                    if (kul01.Text == "" || kul1224.Text == "" || kul13.Text == "" || kul2448.Text == "" || kul36.Text == "" || kul4872.Text == "" || kul612.Text == "" || kuldiger.Text == "")
                    {

                    kul01.BackColor = Color.Yellow;
                    kul1224.BackColor = Color.Yellow;
                    kul13.BackColor = Color.Yellow;
                    kul2448.BackColor = Color.Yellow;
                    kul36.BackColor = Color.Yellow;
                    kul4872.BackColor = Color.Yellow;
                    kul612.BackColor = Color.Yellow;
                    kuldiger.BackColor = Color.Yellow;

                    MessageBox.Show("BOŞ ALAN BIRAKMAYIN", "BOŞ ALAN HATASI");
                    }


                    else
                    {
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("update tarifeler set sifirbir='" + kul01.Text + "', biruc='" + kul13.Text + "',ucaltı='" + kul36.Text + "',altıoniki='" + kul612.Text + "',onikiyirmidort='" + kul1224.Text + "',yirmidortkirksekiz='" + kul2448.Text + "',kirksekizyetmisiki='" + kul4872.Text + "',yuz='" + kuldiger.Text + "' where musteriler='1' ", baglanti);
                        komut.ExecuteNonQuery();
                        baglanti.Close();

                        kul01.Clear();
                        kul1224.Clear();
                        kul13.Clear();
                        kul2448.Clear();
                        kul36.Clear();
                        kul4872.Clear();
                        kul612.Clear();
                        kuldiger.Clear();
                        MessageBox.Show("TARİFELER GÜNCELLENDİ");
                    }
                    
                }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }

