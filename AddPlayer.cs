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
    public partial class AddPlayer : Form
    {
        string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DB;Integrated Security=True";
        public AddPlayer()
        {
            InitializeComponent();
        }

        private void AddPlayer_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT * FROM Players";

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
                    string ff = row.Cells["player_id"].Value.ToString();
                    string zb = row.Cells["team_id"].Value.ToString();
                    string a = row.Cells["player_name"].Value.ToString();
                    string b = row.Cells["nationality"].Value.ToString();
                    string c = row.Cells["position"].Value.ToString();
                    string d = row.Cells["jersey_number"].Value.ToString();
                    string ee = row.Cells["birthdate"].Value.ToString();
                    string f = row.Cells["salary"].Value.ToString();
                    textBox1.Text = ff;
                    textBox2.Text = zb;
                    textBox3.Text = a;
                    textBox4.Text = b;
                    textBox5.Text = c;
                    textBox6.Text = d;
                    dateTimePicker1.Value = Convert.ToDateTime(ee);//textBox7.Text = ee; 
                    textBox8.Text = f;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("InsertPlayer", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@player_id", textBox1.Text);
                    command.Parameters.AddWithValue("@player_name", textBox3.Text);
                    command.Parameters.AddWithValue("@nationality", textBox4.Text);
                    command.Parameters.AddWithValue("@position", textBox5.Text);
                    command.Parameters.AddWithValue("@jersey_number", textBox6.Text);
                    command.Parameters.AddWithValue("@birthdate", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@team_id", textBox2.Text);
                    command.Parameters.AddWithValue("@salary", textBox8.Text);

                    command.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Thêm thành công");
            AddPlayer_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DeletePlayer", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@player_id", textBox1.Text);

                    command.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Xóa thành công");
            AddPlayer_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Mở kết nối
                connection.Open();

                // Tạo đối tượng SqlCommand với stored procedure UpdatePlayer và kết nối
                using (SqlCommand command = new SqlCommand("UpdatePlayer", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@player_id", textBox1.Text);
                    command.Parameters.AddWithValue("@player_name", textBox3.Text);
                    command.Parameters.AddWithValue("@nationality", textBox4.Text);
                    command.Parameters.AddWithValue("@position", textBox5.Text);
                    command.Parameters.AddWithValue("@jersey_number", textBox6.Text);
                    command.Parameters.AddWithValue("@birthdate", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@team_id", textBox2.Text);
                    command.Parameters.AddWithValue("@salary", textBox8.Text);

                    command.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Sửa thành công");
            AddPlayer_Load(sender, e);
        }
    }
}
