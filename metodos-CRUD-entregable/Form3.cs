using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace metodos_CRUD_entregable
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            renderDB();
        }



        private void renderDB()
        {
            string sql = "SELECT nombreempleado FROM tblempleados;";

            MySqlDataReader reader = null;

            MySqlConnection connection = ConnectDB.connectDB();
            connection.Open();

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetString(0));
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros");
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error:  " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                int valorhora = int.Parse(textBox1.Text);
                int horas = int.Parse(textBox2.Text);
                int totalpago = horas * valorhora;
                string nombreEmpleado = comboBox1.SelectedItem.ToString(); // Asume que el ComboBox tiene los nombres de los empleados

                if (horas > 0 && valorhora > 0 && !string.IsNullOrEmpty(nombreEmpleado))
                {
                    int cedula = ObtenerCedula(nombreEmpleado); // Obtiene la cédula del empleado seleccionado

                    if (cedula != 0)
                    {
                        string sql = "INSERT INTO tblpagos (codigoempleado, horas, valorhora, totalpago) VALUES ('" + cedula + "', '" + horas + "', '" + valorhora + "','" + totalpago + "')";

                        MySqlConnection connection = ConnectDB.connectDB();
                        connection.Open();

                        try
                        {
                            MySqlCommand cmd = new MySqlCommand(sql, connection);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Registro guardado");
                          
                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show("Error al guardar: " + ex.Message);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró la cédula del empleado seleccionado");
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos");
                }
            }
            catch (FormatException fex)
            {
                MessageBox.Show("Datos incorrectos: " + fex.Message);
            }
        }

        private int ObtenerCedula(string nombreEmpleado)
        {
            int cedula = 0;
            string sql = "SELECT cedula FROM tblempleados WHERE nombreempleado = '" + nombreEmpleado + "'";

            MySqlConnection connection = ConnectDB.connectDB();
            connection.Open();

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cedula = reader.GetInt32(0);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al obtener la cédula: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return cedula;
        }

  
    }
}
