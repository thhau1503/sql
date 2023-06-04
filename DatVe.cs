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
    public partial class DatVe : Form
    {
        string ID;
        string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DB;Integrated Security=True";
        public DatVe(string ID)
        {
            InitializeComponent();
            this.ID = ID;
        }

        private void DatVe_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM TicketInfo";

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    string ticketId = row.Cells["ticket_id"].Value.ToString();
                    textBox1.Text = ticketId;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("BuyTicket", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ticket_id", textBox1.Text);
                command.Parameters.AddWithValue("@username", ID);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Mua vé thành công.");
                    DatVe_Load(sender, e);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Lỗi: " + ex.Message);
                }
            }

        }
    }
}
