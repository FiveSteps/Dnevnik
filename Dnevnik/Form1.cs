using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Dnevnik
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=DESKTOP-EE4RPR4\SQLEXPRESS;Initial Catalog=DnevnikDB;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            //await sqlConnection.OpenAsync();

            listView1.GridLines = true;

            listView1.FullRowSelect = true;

            listView1.View = View.Details;

            listView1.Columns.Add("Id");
            listView1.Columns.Add("Data");
            listView1.Columns.Add("Zadanie");
            listView1.Columns.Add("Ozenka");

            await LoadStudentsAsync();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null || sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
        }

        private async Task LoadStudentsAsync() //SELECT
        {
            SqlDataReader sqlReader = null;

            SqlCommand getStudentsCommand = new SqlCommand("SELECT * FROM [dnevnik]", sqlConnection);

            try
            {
                sqlReader = await getStudentsCommand.ExecuteReaderAsync();

                while(await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] { 

                        Convert.ToString(sqlReader["Id"]),
                        Convert.ToString(sqlReader["Data"]),
                        Convert.ToString(sqlReader["Zadanie"]),
                        Convert.ToString(sqlReader["Ozenka"])
                    });

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

    }
}
