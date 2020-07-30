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
    public partial class FrmSeri : Form
    {
        public FrmSeri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source = DESKTOP-KKRCQCS\\SQL2019; Initial Catalog = Koç_Otopark; Integrated Security = True");

        private void marka()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka from Marka_Bilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["marka"].ToString());
            }
            baglanti.Close();
        }

        private void FrmSeri_Load(object sender, EventArgs e)
        {
            marka();
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO Seri_Bilgileri(marka,seri) values('"+comboBox1.Text+"','" + textBox1.Text + "')", baglanti);
          
            komut.ExecuteNonQuery();
            
            baglanti.Close();
            MessageBox.Show("Araç Serisi Eklendi...");
            textBox1.Clear();
           
            marka();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("UPDATE Seri_Bilgileri SET markaID = (SELECT markaID FROM Marka_Bilgileri WHERE Seri_Bilgileri.marka = Marka_Bilgileri.marka)", baglanti);

            komut2.ExecuteNonQuery();
            baglanti.Close();

            this.Close();
        }
    }
}
