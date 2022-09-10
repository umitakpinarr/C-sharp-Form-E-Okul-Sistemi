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

    

    public partial class dersler : Form
    {
        public dersler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter("select id as 'ID',ad as 'Ders Adı' from tbl_dersler", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
            
        }

        private void dersler_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtdersid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtdersad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Dersi Eklemek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                MySqlCommand komut = new MySqlCommand("insert into tbl_dersler (ad) values (@p1)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtdersad.Text);
                komut.ExecuteNonQuery();

                bgl.baglanti().Close();
                MessageBox.Show("Ders Başarılı Bir Şekilde Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else if (secenek == DialogResult.No)
            {

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Dersi Silmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                MySqlCommand komut = new MySqlCommand("delete from tbl_dersler where id=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtdersid.Text);
                komut.ExecuteNonQuery();

                bgl.baglanti().Close();
                MessageBox.Show("Ders Başarılı Bir Şekilde Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else if (secenek == DialogResult.No)
            {

            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Dersi Güncellemek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (secenek == DialogResult.Yes)
            {
                MySqlCommand komut = new MySqlCommand("update tbl_dersler set ad=@p1 where id=@p2", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", txtdersad.Text);
                komut.Parameters.AddWithValue("@p2", txtdersid.Text);
                komut.ExecuteNonQuery();

                bgl.baglanti().Close();
                MessageBox.Show("Ders Başarılı Bir Şekilde Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else if (secenek == DialogResult.No)
            {

            }
            
        }
    }
}
