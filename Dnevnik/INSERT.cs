using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Dnevnik
{
    public partial class INSERT : Form
    {

        private SqlConnection sqlConnection;

        public INSERT(SqlConnection connection)
        {
            InitializeComponent();

            sqlConnection = connection;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlCommand addStudentCommand = new SqlCommand("INSERT INTO [dnevnik] (Id, Data, Zadanie, Ozenka)VALUES(@Id, @Data, @Zadanie, @Ozenka)", sqlConnection);

            addStudentCommand.Parameters.AddWithValue("Id", textBox1.Text);
            addStudentCommand.Parameters.AddWithValue("Data", textBox2.Text);
            addStudentCommand.Parameters.AddWithValue("Zadanie", textBox3.Text);
            addStudentCommand.Parameters.AddWithValue("Ozenka", textBox4.Text);

            try
            {
                await addStudentCommand.ExecuteNonQueryAsync();

                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
