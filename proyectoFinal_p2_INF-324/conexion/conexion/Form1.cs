using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace conexion
{
    public partial class Form1 : Form
    {
        private MySqlConnection cnx = null;
        MySqlCommand cmd = null;
        DataTable dt = null;
        Conexion C = null;        
        int RR, GG, BB;
        int mmR, mmG, mmB;
        int r2, g2, b2;
        public Form1()
        {
            InitializeComponent();           
            C = new Conexion();
            cnx = C.Cnx();
            Listar();
        }      
        private void btnAbrir_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Todos|*.*|Archivos JPEG|*.jpg|Archivos GIF|*.gif";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            Bitmap bmp = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = bmp;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "")
            {
                Guardar(txtNombre.Text, int.Parse(txtR.Text), int.Parse(txtG.Text), int.Parse(txtB.Text), r2, g2, b2);
                Listar();
            }
        }
        private void btnColor_Click(object sender, EventArgs e)
        {
            r2 = Int32.Parse(textBox3.Text);
            g2 = Int32.Parse(textBox2.Text);
            b2 = Int32.Parse(textBox1.Text);
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Color c = new Color();
            int x, y, mR = 0, mG = 0, mB = 0;
            x = e.X; 
            y = e.Y;
            for (int i = x; i < x + 10; i++)
                for (int j = y; j < y + 10; j++)
                {
                    c = bmp.GetPixel(i, j);
                    mR = mR + c.R;
                    mG = mG + c.G;
                    mB = mB + c.B;
                }
            mR = mR / 100;
            mG = mG / 100;
            mB = mB / 100;            
            txtR.Text = mR.ToString();
            txtG.Text = mG.ToString();
            txtB.Text = mB.ToString();            
        }
        public void Guardar(string nomb, int r1, int g1, int b1, int r2, int g2, int b2)
        {
            try
            {
                cmd = new MySqlCommand();
                cmd.Connection = cnx;
                cmd.CommandText = "INSERT INTO datos(Nombre,R1,G1,B1,R2,G2,B2 )  VALUES(@nomb,@r1,@g1,@b1,@r2,@g2,@b2 )";
                cmd.Parameters.Add(new MySqlParameter("@nomb", nomb));
                cmd.Parameters.Add(new MySqlParameter("@r1", r1));
                cmd.Parameters.Add(new MySqlParameter("@g1", g1));
                cmd.Parameters.Add(new MySqlParameter("@b1", b1));
                cmd.Parameters.Add(new MySqlParameter("@r2", r2));
                cmd.Parameters.Add(new MySqlParameter("@g2", g2));
                cmd.Parameters.Add(new MySqlParameter("@b2", b2));
                cnx.Open();
                MySqlDataReader lector = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            { cnx.Close(); }
        }
        
        private void Listar()
        {
            dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cnx;
            cmd.CommandText = "SELECT Nombre,R1 AS 'R select', G1 AS 'G select', B1 AS 'B select', R2 AS 'textura R', G2 AS 'textura G', B2 AS 'textura B' FROM datos ORDER BY Id DESC;"; ;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            // Agregar la nueva columna para mostrar el color
            DataColumn colorColumn = new DataColumn("Color", typeof(Color));
            dt.Columns.Add(colorColumn);

            foreach (DataRow row in dt.Rows)
            {
                // Obtener los valores RGB de la base de datos
                int r = Convert.ToInt32(row[4]);
                int g = Convert.ToInt32(row[5]);
                int b = Convert.ToInt32(row[6]);

                // Crear el color a partir de los valores RGB
                Color color = Color.FromArgb(r, g, b);

                // Asignar el color a la columna "Color" en la fila actual
                row["Color"] = color;
            }

            // Eliminar todas las columnas del DataGridView
            dataGridView1.Columns.Clear();

            // Crear la columna "Color" en el DataGridView
            DataGridViewTextBoxColumn colorColumnDataGridView = new DataGridViewTextBoxColumn();
            colorColumnDataGridView.DataPropertyName = "Color";
            colorColumnDataGridView.Name = "Color";

            // Agregar la columna "Color" al principio del DataGridView
            dataGridView1.Columns.Add(colorColumnDataGridView);

            dataGridView1.DataSource = dt;

            // Manejar el evento CellFormatting para establecer el estilo de la celda de color
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Color")
            {
                // Obtener el valor de la celda
                object cellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                // Verificar si el valor es nulo
                if (cellValue != null && cellValue != DBNull.Value)
                {
                    // Convertir el valor a Color
                    Color color = (Color)cellValue;

                    // Establecer el color de fondo de la celda
                    e.CellStyle.BackColor = color;
                }
                else
                {
                    // Valor nulo, establecer el color de fondo a blanco o cualquier otro color predeterminado
                    e.CellStyle.BackColor = Color.White;
                }
            }
        }

        private void btnColorear_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Color c = new Color();
            //bucle para 10 pixeles
            for (int i = 0; i < bmp.Width - 10; i += 10)
                for (int j = 0; j < bmp.Height - 10; j += 10)
                {
                    mmR = 0;
                    mmG = 0;
                    mmB = 0;
                    //bucle para iterar a travez de los 10 pixeles
                    for (int i2 = i; i2 < i + 10; i2++)
                        for (int j2 = j; j2 < j + 10; j2++)
                        {
                            c = bmp.GetPixel(i2, j2);
                            mmR = mmR + c.R;
                            mmG = mmG + c.G;
                            mmB = mmB + c.B;
                        }
                    //obtenemos valores promedio de componentes RGB en 10x10
                    mmR = mmR / 100;
                    mmG = mmG / 100;
                    mmB = mmB / 100;

                    bool sw = false;
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        RR = int.Parse(dt.Rows[k][1].ToString());
                        GG = int.Parse(dt.Rows[k][2].ToString());
                        BB = int.Parse(dt.Rows[k][3].ToString());
                        int RBD = int.Parse(dt.Rows[k][4].ToString());
                        int GBD = int.Parse(dt.Rows[k][5].ToString());
                        int BBD = int.Parse(dt.Rows[k][6].ToString());
                        if (((RR - 10) < mmR) && (mmR < (RR + 10)) &&
                            ((GG - 10) < mmG) && (mmG < (GG + 10)) &&
                            ((BB - 10) < mmB) && (mmB < (BB + 10)))
                        {
                            for (int i2 = i; i2 < i + 10; i2++)
                                for (int j2 = j; j2 < j + 10; j2++)
                                {
                                    bmp2.SetPixel(i2, j2, Color.FromArgb(RBD, GBD, BBD));
                                    sw = true;
                                }
                        }
                    }
                    if (!sw)
                    {
                        for (int i2 = i; i2 < i + 10; i2++)
                            for (int j2 = j; j2 < j + 10; j2++)
                            {
                                c = bmp.GetPixel(i2, j2);
                                bmp2.SetPixel(i2, j2, c);
                            }
                    }
                }
            pictureBox2.Image = bmp2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Color c = new Color();

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);

                    bool sw = false;
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        int RR = int.Parse(dt.Rows[k][1].ToString());
                        int GG = int.Parse(dt.Rows[k][2].ToString());
                        int BB = int.Parse(dt.Rows[k][3].ToString());
                        int RBD = int.Parse(dt.Rows[k][4].ToString());
                        int GBD = int.Parse(dt.Rows[k][5].ToString());
                        int BBD = int.Parse(dt.Rows[k][6].ToString());

                        if (((RR - 10) < c.R) && (c.R < (RR + 10)) &&
                            ((GG - 10) < c.G) && (c.G < (GG + 10)) &&
                            ((BB - 10) < c.B) && (c.B < (BB + 10)))
                        {
                            bmp2.SetPixel(i, j, Color.FromArgb(RBD, GBD, BBD));
                            sw = true;
                            break;
                        }
                    }

                    if (!sw)
                    {
                        bmp2.SetPixel(i, j, c);
                    }
                }
            }

            pictureBox2.Image = bmp2;
        }              
        
    }   
}
