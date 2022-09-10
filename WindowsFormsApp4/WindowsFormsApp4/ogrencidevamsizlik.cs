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
    public partial class ogrencidevamsizlik : Form
    {
        public ogrencidevamsizlik()
        {
            InitializeComponent();
        }

        public string tc;


        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT tarih as 'Tarih',gun as 'Gün',izin as 'İzin' from tbl_devamsizliklar where ogrencino=" + tc, bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }


        private void ogrencidevamsizlik_Load(object sender, EventArgs e)
        {
            
            MySqlCommand komut = new MySqlCommand("select SUM(gun) from tbl_devamsizliklar where izin=1 and ogrencino=" + tc, bgl.baglanti());
            MySqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                label4.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
            MySqlCommand komut1 = new MySqlCommand("select SUM(gun) from tbl_devamsizliklar where izin=0 and ogrencino=" + tc, bgl.baglanti());
            MySqlDataReader dr2 = komut1.ExecuteReader();
            while (dr2.Read())
            {
                label3.Text = dr2[0].ToString();
            }
            bgl.baglanti().Close();


            listele();
        }
    }
}
