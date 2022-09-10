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
    public partial class ogretmenler : Form
    {
        public ogretmenler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("execute ogretmenler", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }

        void bosalt()
        {
            txtid.Text = "";
            txtad.Text = "";
            txtsifre.Text = "";
            txtsoyad.Text = "";
            cmbders.Text = "";
            msktc.Text = "";
        }

        void dersler()
        {
            DataTable dt2 = new DataTable();
            MySqlDataAdapter da2 = new MySqlDataAdapter("select * from tbl_dersler", bgl.baglanti());
            da2.Fill(dt2);
            cmbders.ValueMember = "id";
            cmbders.DisplayMember = "ad";
            cmbders.DataSource = dt2;
            bgl.baglanti().Close();
        }

        bool durum;
        void varmi()
        {
            MySqlCommand komut = new MySqlCommand("select * from tbl_ogretmenler where tc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", msktc.Text);
            MySqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                durum = true;
            }
            else
            {
                durum = false;
            }
        }

        private void ogretmenler_Load(object sender, EventArgs e)
        {
            listele();
            dersler();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            varmi();
            
            try
            {
                DialogResult secenek = MessageBox.Show("Öğretmeni Eklemek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (secenek == DialogResult.Yes)
                {
                    if (durum == false)
                    {
                        if (txtad.Text == "" || txtsoyad.Text == "" || msktc.Text == "" || txtsifre.Text == "" || cmbders.Text == "")
                        {
                            MessageBox.Show("Boş Bir Alan Bıraktınız.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {

                            MySqlCommand komut = new MySqlCommand("insert into tbl_ogretmenler (tc,ad,soyad,dal,sifre) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                            komut.Parameters.AddWithValue("@p1", msktc.Text);
                            komut.Parameters.AddWithValue("@p2", txtad.Text);
                            komut.Parameters.AddWithValue("@p3", txtsoyad.Text);
                            komut.Parameters.AddWithValue("@p4", cmbders.SelectedValue);
                            komut.Parameters.AddWithValue("@p5", txtsifre.Text);
                            komut.ExecuteNonQuery();

                            bgl.baglanti().Close();
                            MessageBox.Show("Öğretmen Başarılı Bir Şekilde Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            listele();
                            bosalt();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Bu TC Daha Önce Kaydedilmiş.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (secenek == DialogResult.No)
                {

                }

            }
            catch (Exception)
            {

                MessageBox.Show("Hatalı Bilgi Girişi.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           

            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            msktc.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            cmbders.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtsifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Öğretmeni Silmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                MySqlCommand komut = new MySqlCommand("delete from tbl_ogretmenler where id=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Öğretmen Başarılı Bir Şekilde Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                bosalt();
            }
            else if (secenek == DialogResult.No)
            {

            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Öğretmeni Güncellemek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                MySqlCommand komut = new MySqlCommand("update tbl_ogretmenler set tc=@p1,ad=@p2,soyad=@p3,dal=@p4,sifre=@p5 where id=@p6", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", msktc.Text);
                komut.Parameters.AddWithValue("@p2", txtad.Text);
                komut.Parameters.AddWithValue("@p3", txtsoyad.Text);
                komut.Parameters.AddWithValue("@p4", cmbders.SelectedValue);
                komut.Parameters.AddWithValue("@p5", txtsifre.Text);
                komut.Parameters.AddWithValue("@p6", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Öğretmen Başarılı Bir Şekilde Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                bosalt();
            }
            else if (secenek == DialogResult.No)
            {

            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySqlCommand komut = new MySqlCommand("select * from tbl_ogretmenler where tc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", msktc.Text);
            MySqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtid.Text = dr[0].ToString();
                txtad.Text = dr[2].ToString();
                txtsoyad.Text = dr[3].ToString();
                cmbders.SelectedValue = dr[4].ToString();
                txtsifre.Text = dr[5].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}

