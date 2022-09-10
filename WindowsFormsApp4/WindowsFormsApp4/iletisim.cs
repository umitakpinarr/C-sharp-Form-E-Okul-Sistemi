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
    public partial class iletisim : Form
    {
        public iletisim()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();


        void listele()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_iletisim", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void temizle()
        {
            txtid.Text = "";
            txtveliad.Text = "";
            mskogrencitel.Text = "";
            mskvelitel.Text = "";
            txtogrno.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("İletişim Bilgilerini Eklemek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (secenek == DialogResult.Yes)
            {
                MySqlCommand komut = new MySqlCommand("insert into tbl_iletisim (okulno,veliad,velitel,ogrencitel) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtogrno.Text);
                komut.Parameters.AddWithValue("@p2", txtveliad.Text);
                komut.Parameters.AddWithValue("@p3", mskvelitel.Text);
                komut.Parameters.AddWithValue("@p4", mskogrencitel.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("İletişim Bilgileri Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bgl.baglanti().Close();
                temizle();
                listele();
            }
            else if (secenek == DialogResult.No)
            {

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("İletişim Bilgilerini Silmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (secenek == DialogResult.Yes)
            {
                MySqlCommand komut = new MySqlCommand("delete from tbl_iletisim where id=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtid.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("İletişim Bilgileri Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bgl.baglanti().Close();
                temizle();
                listele();
            }
            else if (secenek == DialogResult.No)
            {

            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("İletişim Bilgilerini Güncellemek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (secenek == DialogResult.Yes)
            {
                MySqlCommand komut = new MySqlCommand("update tbl_iletisim set okulno=@p1,veliad=@p2,velitel=@p3,ogrencitel=@p4 where id=@p5", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtogrno.Text);
                komut.Parameters.AddWithValue("@p2", txtveliad.Text);
                komut.Parameters.AddWithValue("@p3", mskvelitel.Text);
                komut.Parameters.AddWithValue("@p4", mskogrencitel.Text);
                komut.Parameters.AddWithValue("@p5", txtid.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("İletişim Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bgl.baglanti().Close();
                temizle();
                listele();
            }
            else if (secenek == DialogResult.No)
            {

            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySqlCommand komut = new MySqlCommand("select * from tbl_iletisim where okulno=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtogrno.Text);
            MySqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtid.Text = dr[0].ToString();
                txtogrno.Text = dr[1].ToString();
                txtveliad.Text = dr[2].ToString();
                mskvelitel.Text = dr[3].ToString();
                mskogrencitel.Text = dr[4].ToString();
            }
            bgl.baglanti().Close();
        }

        private void iletisim_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtogrno.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtveliad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            mskvelitel.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskogrencitel.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }
    }
}
