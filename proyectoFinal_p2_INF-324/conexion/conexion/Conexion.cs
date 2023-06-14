using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace conexion
{
    class Conexion
    {
        private readonly MySqlConnection cnx = null;
        private readonly MySqlConnectionStringBuilder cadena = new MySqlConnectionStringBuilder();
        public Conexion()
        {
            MySqlConnection conexion = new MySqlConnection();
            conexion.ConnectionString = "Server=localhost;Database=bdimagen; Uid=root;Pwd="+"";
            conexion.Open();
            cnx = new MySqlConnection(conexion.ConnectionString);
        }
        public MySqlConnection Cnx()
        {
            //MessageBox.Show("correcto");
            return cnx;
        }
    }
}
