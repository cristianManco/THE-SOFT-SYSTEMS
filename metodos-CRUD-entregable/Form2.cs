using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace metodos_CRUD_entregable
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            mostrarData();
        }


        private void mostrarData()
        {
            try
            {


                string sql = "SELECT tblempleados.cedula, tblempleados.nombreempleado, tblempleados.edad FROM tblempleados;";
                MySqlDataReader reader = null;

                MySqlConnection connection = ConnectDB.connectDB();
                connection.Open();

                try
                {
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    reader = cmd.ExecuteReader();

                    // Crear las columnas de botones editar e eliminar
                    DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
                    btnEditar.HeaderText = "Editar";
                    btnEditar.Text = "Editar";
                    btnEditar.Name = "btnEditar";
                    btnEditar.UseColumnTextForButtonValue = true;

                    DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
                    btnEliminar.HeaderText = "Eliminar";
                    btnEliminar.Text = "Eliminar";
                    btnEliminar.Name = "btnEliminar";
                    btnEliminar.UseColumnTextForButtonValue = true;

                    dataGridView1.Columns.AddRange(new DataGridViewColumn[] { btnEditar, btnEliminar });

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(
                                reader.GetInt32(0).ToString(), // cedula
                                reader.GetString(1),           // nombreempleado
                                reader.GetInt32(2).ToString()  // edad
                            );
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("failet to search " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                // Evento click del botón en el DataGridView
               
                dataGridView1.CellClick += (s, e) =>
                {
                    // Comprobar si se ha hecho clic en la columna del botón
                    if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                    {
                        // Obtener la cédula, el nombre y la edad del registro seleccionado
                        String cedula = dataGridView1.Rows[e.RowIndex].Cells["column1"].Value.ToString();
                        String nombre = dataGridView1.Rows[e.RowIndex].Cells["Column2"].Value.ToString();
                        String edad = dataGridView1.Rows[e.RowIndex].Cells["Column3"].Value.ToString();

                        if (dataGridView1.Columns[e.ColumnIndex].Name == "btnEditar")
                        {
                            // Aquí puedes manejar el evento click del botón Editar
                            // Habilitar las celdas para la edición
                            dataGridView1.ReadOnly = false;
                            dataGridView1.Rows[e.RowIndex].ReadOnly = false;
                            dataGridView1.Rows[e.RowIndex].Cells["column1"].ReadOnly = true; // La cédula no debe ser editable
                        }
                        else if (dataGridView1.Columns[e.ColumnIndex].Name == "btnEliminar")
                        {
                            
                            // Mostrar un cuadro de diálogo de confirmación y luego eliminar el registro
                            DialogResult dialogResult = MessageBox.Show("¿Estás seguro de que quieres eliminar este registro?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                textBox1.Text = cedula;
                                btnEliminar_Click(null, null); // Llamar al método de eliminación
                            }
                        }
                    }
                };

                // Evento CellEndEdit del DataGridView
                dataGridView1.CellEndEdit += (s, e) =>
                {
                    // Obtener la cédula, el nombre y la edad editados
                    String cedula = dataGridView1.Rows[e.RowIndex].Cells["column1"].Value.ToString();
                    String nombre = dataGridView1.Rows[e.RowIndex].Cells["Column2"].Value.ToString();
                    String edad = dataGridView1.Rows[e.RowIndex].Cells["Column3"].Value.ToString();


                    btnActualizar_Click(null, null); // Llamar al método de actualización
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:   " + ex.Message);
            }
        }

        private void btnActualizar_Click(object value1, object value2)
        {
            try
            {

                String cedula = textBox1.Text;
                String nombre = textBox2.Text;
                String edad = textBox3.Text;

                string sql = "UPDATE tblempleados SET cedula='" + cedula + "', nombreempleado='" + nombre + "', edad='" + edad + "' WHERE cedula='" + cedula + "'";

                MySqlConnection connection = ConnectDB.connectDB();
                connection.Open();


                try
                {
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro modificado");

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error al modificar: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                //throw new NotImplementedException();


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error:   " + ex.Message);
            }

        }

        private void btnEliminar_Click(object value1, object value2)
        {
            try
            {

                String cedula = textBox1.Text;

                string sql = "DELETE FROM tblempleados WHERE cedula='" + cedula + "'";


                MySqlConnection connection = ConnectDB.connectDB();
                connection.Open();

                try
                {
                    MySqlCommand comando = new MySqlCommand(sql, connection);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Registro eliminado");

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }

                //throw new NotImplementedException();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error:   " + ex.Message);
            }




        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }


        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                String cedula = textBox1.Text;
                String nombre = textBox2.Text;
                int edad;
                edad = int.Parse(textBox3.Text);


                if (cedula != "" && nombre != "" && edad > 0)
                {

                    string sql = "INSERT INTO tblempleados (cedula, nombreempleado, edad) VALUES ('" + cedula + "', '" + nombre + "','" + edad + "')";


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
                    MessageBox.Show("Debe completar todos los campos");
                }
            }
            catch (FormatException fex)
            {
                MessageBox.Show("Datos incorrectos: " + fex.Message);
            }
        }

   
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }
    }
}
