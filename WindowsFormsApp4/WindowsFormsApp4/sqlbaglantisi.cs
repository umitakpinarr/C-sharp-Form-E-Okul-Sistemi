using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp4
{
    class sqlbaglantisi
    {
        public MySqlConnection baglanti()
        {
            MySqlConnection baglan = new MySqlConnection("server=localhost;user id=root;database=deneme;SslMode=None");
            baglan.Open();
            return baglan;
        }
    }
}
