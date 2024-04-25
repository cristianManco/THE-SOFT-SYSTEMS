using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace metodos_CRUD_entregable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            mostrarData();
        }


        private void mostrarData()
        {

            string sql = "SELECT tblempleados.cedula, tblempleados.nombreempleado, tblempleados.edad, tblpagos.horas, tblpagos.valorhora, tblpagos.totalpago  FROM tblempleados INNER JOIN tblpagos ON tblempleados.cedula = tblpagos.codigoempleado;";
            MySqlDataReader reader = null;
            //activando la connection
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
                        //renderizando la informacion en el datagridview
                        dataGridView1.Rows.Add(
                            reader.GetInt32(0).ToString(), // cedula
                            reader.GetString(1),           // nombreempleado
                            reader.GetInt32(2).ToString(), // edad
                            reader.GetInt32(3).ToString(), // horas
                            reader.GetInt32(4).ToString(), // valorhora
                            reader.GetInt32(5).ToString()  // totalpago
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



            try
            {   
                //creando la columna de botones en el datagridview

                DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
                btnEliminar.HeaderText = "Eliminar";
                btnEliminar.Text = "Eliminar";
                btnEliminar.Name = "btnEliminar";
                btnEliminar.UseColumnTextForButtonValue = true;

                dataGridView1.Columns.AddRange(new DataGridViewColumn[] { btnEliminar });


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



                    if (dataGridView1.Columns[e.ColumnIndex].Name == "btnEliminar")
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




        }
        private void btnEliminar_Click(object value1, object value2)
        {       //metodo eliminar
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
                    //limpiar();
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

        private void button2_Click_1(object sender, EventArgs e)
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
