using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp4
{
    public partial class anasayfa : Form
    {
        public anasayfa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void button2_Click(object sender, EventArgs e)
        {

            MySqlCommand komut = new MySqlCommand("select * from tbl_ogretmenler where tc=@p1 and sifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskogretmentc.Text);
            komut.Parameters.AddWithValue("@p2", txtogretmensifre.Text);
            MySqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                ogretmenanasayfa fr = new ogretmenanasayfa();
                fr.tc2 = mskogretmentc.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Giriş Başarısız Bilgilerinizi Kontrol Ediniz!", "Hatalı Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void anasayfa_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand komut = new MySqlCommand("select * from tbl_ogrenciler where okulno=@p1 and tc=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtogrno.Text);
            komut.Parameters.AddWithValue("@p2", mskogrtc.Text);
            MySqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                ogrencianasayfa fr = new ogrencianasayfa();
                fr.tc2 = txtogrno.Text;
                fr.Show();
            }
            else
            {
                MessageBox.Show("Giriş Başarısız Bilgilerinizi Kontrol Ediniz!", "Hatalı Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
