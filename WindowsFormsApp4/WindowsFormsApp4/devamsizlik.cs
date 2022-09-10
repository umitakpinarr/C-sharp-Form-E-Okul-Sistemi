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
    public partial class devamsizlik : Form
    {
        public devamsizlik()
        {
            InitializeComponent();
        }


        sqlbaglantisi bgl = new sqlbaglantisi();


        void listele()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select * from tbl_devamsizliklar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void devamsizlik_Load(object sender, EventArgs e)
        {
            dtptarih.Format = DateTimePickerFormat.Custom;
            dtptarih.CustomFormat = "yyyy-MM-dd";

            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Devamsızlığı eklemek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                MySqlCommand komut = new MySqlCommand("insert into tbl_devamsizliklar (ogrencino,tarih,gun,izin) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtogrencino.Text);
                komut.Parameters.AddWithValue("@p2", dtptarih.Text);
                komut.Parameters.AddWithValue("@p3", float.Parse(comboBox1.Text));
                if (checkBox1.Checked)
                {
                    komut.Parameters.AddWithValue("@p4", 1);
                }
                else
                {
                    komut.Parameters.AddWithValue("@p4", 0);
                }
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Öğrenci Devamsızlığı Başarılı Bir Şekilde Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else if (secenek == DialogResult.No)
            {

            }

            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {


            DialogResult secenek = MessageBox.Show("Devamsızlığı güncellemek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                MySqlCommand komut = new MySqlCommand("update tbl_devamsizliklar set ogrencino=@p1,tarih=@p2,gun=@p3,izin=@p4 where id=@p5", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtogrencino.Text);
                komut.Parameters.AddWithValue("@p2", dtptarih.Text);
                komut.Parameters.AddWithValue("@p3", float.Parse(comboBox1.Text));
                if (checkBox1.Checked)
                {
                    komut.Parameters.AddWithValue("@p4", 1);
                }
                else
                {
                    komut.Parameters.AddWithValue("@p4", 0);
                }
                komut.Parameters.AddWithValue("@p5", txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Öğrenci Devamsızlığı Başarılı Bir Şekilde Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else if (secenek == DialogResult.No)
            {

            }

            
        }

        string izin;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtogrencino.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            dtptarih.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            izin = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (izin == "True")
            {
                checkBox1.Checked = true;
            }
            else if(izin == "False")
            {
                checkBox1.Checked = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}
