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
    public partial class notlar : Form
    {
        public notlar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        string donem;
        bool durum;
        float ortalama2;

        void ortalama()
        {
            if (txtper1.Text == "" && txtnot2.Text == "" && txtper2.Text == "")
            {
                ortalama2 = float.Parse(txtnot1.Text);
                
            }
            else if(txtnot2.Text == "" && txtper2.Text == "")
            {
                ortalama2 = (float.Parse(txtnot1.Text) + float.Parse(txtper1.Text)) / 2;
            }
            else if (txtper2.Text == "")
            {
                ortalama2 = (float.Parse(txtnot1.Text) + float.Parse(txtper1.Text) + float.Parse(txtnot2.Text)) / 3;
            }
            else
            {
                ortalama2 = (float.Parse(txtper1.Text) + float.Parse(txtnot1.Text) + float.Parse(txtnot2.Text) + float.Parse(txtper2.Text)) / 4;
            }

        }

        void varmi()
        {
            MySqlCommand komut = new MySqlCommand("select * from tbl_notlar where okulno=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtogrno.Text);
            
            MySqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                durum = true;
            }
            else
            {
                durum = false;
            }
            bgl.baglanti().Close();
        }

        public string ders2;

        void siniflar()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_siniflar", bgl.baglanti());
            da.Fill(dt);
            comboBox2.ValueMember = "id";
            comboBox2.DisplayMember = "sinif";
            comboBox2.DataSource = dt;

        }

        void listele()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT okulno as 'Okul Numarası',sinav1 as 'Sınav 1',sinav2 as 'Sınav 2',per1 as 'Performans 1',per2 as 'Performans 2',ortalama as 'Ortalama' from tbl_notlar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ortalama();
            varmi();
            if (durum == true)
            {
                MessageBox.Show("Öğrencinin Notu Zaten Girilmiş.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                
                DialogResult secenek = MessageBox.Show("Notu eklemek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (secenek == DialogResult.Yes)
                {
                    MySqlCommand komut = new MySqlCommand("insert into tbl_notlar (okulno,sinav1,sinav2,per1,per2,ortalama,ders) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7)", bgl.baglanti());
                    komut.Parameters.AddWithValue("@p1", txtogrno.Text);
                    komut.Parameters.AddWithValue("@p2", txtnot1.Text);
                    komut.Parameters.AddWithValue("@p3", txtnot2.Text);
                    komut.Parameters.AddWithValue("@p4", txtper1.Text);
                    komut.Parameters.AddWithValue("@p5", txtper2.Text);
                    komut.Parameters.AddWithValue("@p6", ortalama2);
                    komut.Parameters.AddWithValue("@p7", ders2);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    listele();
                    MessageBox.Show("Not Başarılı Bir Şekilde Girildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (secenek == DialogResult.No)
                {

                }
            
            }
            
            

            
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ortalama();
            DialogResult secenek = MessageBox.Show("Notu güncellemek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (secenek == DialogResult.Yes)
            {
                MySqlCommand komut = new MySqlCommand("update tbl_notlar set sinav1=@p1,sinav2=@p2,per1=@p3,per2=@p4,ortalama=@p5 where okulno=@p6", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtnot1.Text);
                komut.Parameters.AddWithValue("@p2", txtnot2.Text);
                komut.Parameters.AddWithValue("@p3", txtper1.Text);
                komut.Parameters.AddWithValue("@p4", txtper2.Text);
                komut.Parameters.AddWithValue("@p5", ortalama2);
                komut.Parameters.AddWithValue("@p6", txtogrno.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                listele();
                MessageBox.Show("Not Başarılı Bir Şekilde Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (secenek == DialogResult.No)
            {
                
            }



                
           

        }

        

        private void notlar_Load(object sender, EventArgs e)
        {
            
            siniflar();
            listele();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtogrno.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtnot1.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtper1.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            donem = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlCommand komut = new MySqlCommand("select okulno,ad,soyad from tbl_ogrenciler where sinif=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", comboBox2.SelectedValue);
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(komut);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            bgl.baglanti().Close();
            listele(); 
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            listele();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            listele();
        }
    }
}
