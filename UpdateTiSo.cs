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
    public partial class UpdateTiSo : Form
    {
        string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DB;Integrated Security=True";
        public UpdateTiSo()
        {
            InitializeComponent();
        }

        private void UpdateTiSo_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Matches";
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

                using (SqlCommand command = new SqlCommand("AddMatchDone", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@match_id", SqlDbType.Int).Value = textBox1.Text;
                    command.Parameters.Add("@home_team_id", SqlDbType.Int).Value = textBox2.Text;
                    command.Parameters.Add("@away_team_id", SqlDbType.Int).Value = textBox3.Text;
                    command.Parameters.Add("@match_date", SqlDbType.Date).Value = dateTimePicker1.Value;
                    command.Parameters.Add("@home_team_goals", SqlDbType.Int).Value = textBox5.Text;
                    command.Parameters.Add("@away_team_goals", SqlDbType.Int).Value = textBox6.Text;
                    command.Parameters.Add("@result", SqlDbType.VarChar, 255).Value = textBox7.Text;
                    command.Parameters.Add("@stadium", SqlDbType.VarChar, 255).Value = textBox8.Text;

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            UpdateTiSo_Load(sender, e);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    string ff = row.Cells["match_id"].Value.ToString();
                    string zb = row.Cells["home_team_id"].Value.ToString();
                    string a = row.Cells["away_team_id"].Value.ToString();
                    string b = row.Cells["stadium"].Value.ToString();
                    string d = row.Cells["date_time"].Value.ToString();
                    textBox1.Text = ff;
                    textBox2.Text = zb;
                    textBox3.Text = a;
                    dateTimePicker1.Value = Convert.ToDateTime(d);
                    textBox8.Text = b;
                }
            }
        }
    }
}
        
