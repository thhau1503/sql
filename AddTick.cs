using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_DoiBong
{
    public partial class AddTick : Form
    {
        string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DB;Integrated Security=True";
        public AddTick()
        {
            InitializeComponent();
        }

        private void AddTick_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Tickets";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("InsertTickets", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ticket_id", textBox5.Text);
                    command.Parameters.AddWithValue("@match_id", textBox1.Text);
                    command.Parameters.AddWithValue("@seat_number", textBox2.Text);
                    command.Parameters.AddWithValue("@price", textBox3.Text);
                    command.Parameters.AddWithValue("@ticket_type", textBox4.Text);
                    command.ExecuteNonQuery();
                }
            }
            AddTick_Load(sender, e);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    string d = row.Cells["match_id"].Value.ToString();
                    string a = row.Cells["seat_number"].Value.ToString();
                    string b = row.Cells["price"].Value.ToString();
                    string c = row.Cells["ticket_type"].Value.ToString();
                    textBox1.Text = d;
                    textBox2.Text = a;
                    textBox3.Text = b;
                    textBox4.Text = c;
                }   
            }
        }
    }
}
