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
    public partial class ogrenciekle : Form
    {

        public ogrenciekle()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void temizle()
        {
            txtid.Text = "";
            txtno.Text = "";
            txtad.Text = "";
            txtsoyad.Text = "";
            cmbsinif.Text = "";
            msktc.Text = "";
        }

        void siniflar()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_siniflar", bgl.baglanti());
            da.Fill(dt);
            cmbsinif.ValueMember = "id";
            cmbsinif.DisplayMember = "sinif";
            cmbsinif.DataSource = dt;
            bgl.baglanti().Close();
        }

        bool durum;
        void varmi()
        {
            MySqlCommand komut = new MySqlCommand("select * from tbl_ogrenciler where tc=@p1 or okulno=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", msktc.Text);
            komut.Parameters.AddWithValue("@p2", txtno.Text);
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

        void listele()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select id as 'ID',okulno as 'Numara',ad as 'Ad',soyad as 'Soyad',tc as 'TC',sinif as 'Sınıf' from tbl_ogrenciler", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }

        private void ogrenciekle_Load(object sender, EventArgs e)
        {
            listele();
            siniflar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            varmi();

            try
            {
                if (durum == false)
                {
                    if (txtad.Text == "" || txtsoyad.Text == "" || msktc.Text == "" || cmbsinif.Text == "" || txtno.Text == "")
                    {
                        MessageBox.Show("Boş Bir Alan Bıraktınız.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult secenek = MessageBox.Show("Öğrenciyi eklemek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (secenek == DialogResult.Yes)
                        {
                            MySqlCommand komut = new MySqlCommand("insert into tbl_ogrenciler (okulno,ad,soyad,tc,sinif) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                            komut.Parameters.AddWithValue("@p1", txtno.Text);
                            komut.Parameters.AddWithValue("@p2", txtad.Text);
                            komut.Parameters.AddWithValue("@p3", txtsoyad.Text);
                            komut.Parameters.AddWithValue("@p4", msktc.Text);
                            komut.Parameters.AddWithValue("@p5", cmbsinif.SelectedValue);
                            komut.ExecuteNonQuery();
                            bgl.baglanti().Close();
                            MessageBox.Show("Öğrenci Başarılı Bir Şekilde Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            listele();
                            temizle();
                        }
                        else if (secenek == DialogResult.No)
                        {

                        }
                        

                    }

                }
                else
                {
                    MessageBox.Show("Bu TC/Okul Numarası Daha Önce Kaydedilmiş.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Hatalı Bilgi Girişi.", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Öğrenciyi güncellemek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                MySqlCommand komut = new MySqlCommand("update tbl_ogrenciler set okulno=@p1,ad=@p2,soyad=@p3,tc=@p4,sinif=@p5 where id=@p6", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtno.Text);
                komut.Parameters.AddWithValue("@p2", txtad.Text);
                komut.Parameters.AddWithValue("@p3", txtsoyad.Text);
                komut.Parameters.AddWithValue("@p4", msktc.Text);
                komut.Parameters.AddWithValue("@p5", cmbsinif.SelectedValue);
                komut.Parameters.AddWithValue("@p6", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Öğrenci Başarılı Bir Şekilde Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            else if (secenek == DialogResult.No)
            {

            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Öğrenciyi silmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                MySqlCommand komut = new MySqlCommand("delete from tbl_ogrenciler where id=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Öğrenci Başarılı Bir Şekilde Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            else if (secenek == DialogResult.No)
            {

            }
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            MySqlCommand komut = new MySqlCommand("select * from tbl_ogrenciler where tc=@p1 or okulno=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",msktc.Text);
            komut.Parameters.AddWithValue("@p2", txtno.Text);
            MySqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtid.Text = dr[0].ToString();
                txtno.Text = dr[1].ToString();
                txtad.Text = dr[2].ToString();
                txtsoyad.Text = dr[3].ToString();
                msktc.Text = dr[4].ToString();
                cmbsinif.SelectedValue = dr[5].ToString();
            }
            bgl.baglanti().Close();
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtno.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            msktc.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            cmbsinif.SelectedValue = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            
        }
    }
}
