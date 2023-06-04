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
    public partial class LuongTB : Form
    {
        string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DB;Integrated Security=True";
        public LuongTB()
        {
            InitializeComponent();
        }

        private void LuongTB_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT team_name FROM Teams";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string teamName = reader["team_name"].ToString();
                    comboBox1.Items.Add(teamName);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Tạo đối tượng SqlCommand và gọi function trong truy vấn
                string query = "SELECT dbo.CalculateAverageSalary(@team_id)";
                SqlCommand command = new SqlCommand(query, connection);

                // Thêm tham số và gán giá trị
                command.Parameters.AddWithValue("@team_id", textBox2.Text); // Thay đổi team_id thành giá trị thích hợp

                // Thực thi truy vấn và đọc kết quả
                decimal avgSalary = (decimal)command.ExecuteScalar();

                // Hiển thị lương trung bình trong TextBox
                textBox1.Text = avgSalary.ToString("0.00");
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedTeamName = comboBox1.SelectedItem.ToString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT team_id FROM Teams WHERE team_name = @team_name";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_name", selectedTeamName);

                int teamId = (int)command.ExecuteScalar();

                textBox2.Text = teamId.ToString();
            }
        }
    }
}
