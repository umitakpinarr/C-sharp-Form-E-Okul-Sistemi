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
    
    public partial class ogrencianasayfa : Form
    {
        public ogrencianasayfa()
        {
            InitializeComponent();
        }

        public string tc2;

        sqlbaglantisi bgl = new sqlbaglantisi();

        void tiklandi(Form frm)
        {
            panel2.Controls.Clear();
            frm.MdiParent = this;
            panel2.Controls.Add(frm);
            frm.Show();

        }
        private void label2_Click(object sender, EventArgs e)
        {
            ogrencinotlar fr = new ogrencinotlar();
            fr.tc = tc2;
            tiklandi(fr);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            ogrencidevamsizlik fr2 = new ogrencidevamsizlik();
            fr2.tc = tc2;
            tiklandi(fr2);
        }

        private void ogrencianasayfa_Load(object sender, EventArgs e)
        {

            MySqlCommand komut = new MySqlCommand("select ad,soyad,tbl_siniflar.sinif from tbl_ogrenciler inner join tbl_siniflar on tbl_siniflar.id=tbl_ogrenciler.sinif where okulno=" + tc2, bgl.baglanti()); ;
            MySqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                label1.Text = dr[0] + " " + dr[1];
                label4.Text = dr[2].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}
