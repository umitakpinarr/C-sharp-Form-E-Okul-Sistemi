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
    public partial class ogretmenanasayfa : Form
    {
        public ogretmenanasayfa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        public string tc2,ders;
        void tiklandi(Form frm)
        {
            panel2.Controls.Clear();
            frm.MdiParent = this;
            panel2.Controls.Add(frm);
            frm.Show();

        }

        private void label6_Click(object sender, EventArgs e)
        {
            notlar fr = new notlar();
            fr.ders2 = ders;
            tiklandi(fr);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            ogrenciekle fr = new ogrenciekle();
            tiklandi(fr);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            iletisim fr2 = new iletisim();
            tiklandi(fr2);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            devamsizlik fr3 = new devamsizlik();
            tiklandi(fr3);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            dersler fr5 = new dersler();
            tiklandi(fr5);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            siniflar fr6 = new siniflar();
            tiklandi(fr6);
        }

        private void ogretmenanasayfa_Load(object sender, EventArgs e)
        {
            MySqlCommand komut = new MySqlCommand("select tbl_ogretmenler.ad,soyad,tbl_dersler.ad,tbl_dersler.id from tbl_ogretmenler inner join tbl_dersler on tbl_dersler.id=tbl_ogretmenler.dal where tc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", tc2);
            MySqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                label1.Text = dr[0] + " " + dr[1];
                label4.Text = dr[2].ToString();
                ders = dr[3].ToString();

            }
            bgl.baglanti().Close();
        }
    }
}
