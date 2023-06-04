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
    public partial class ThongTinUser : Form
    {
        string ID;
        string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DB;Integrated Security=True";
        public ThongTinUser(string id)
        {
            InitializeComponent();
            this.ID = id;
        }

        private void ThongTinUser_Load(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("GetUserById", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@user_id", ID);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string username = reader["username"].ToString();
                string fullName = reader["full_name"].ToString();
                string a = reader["email"].ToString();
                string b = reader["phone_number"].ToString();
                string c = reader["address"].ToString();
                string d = reader["balance"].ToString();
                textBox1.Text = username;
                textBox2.Text = fullName;
                textBox3.Text = a;
                textBox4.Text = b;
                textBox5.Text = c;
                textBox6.Text = d;
            }
            reader.Close();
            connection.Close();
        }
    }
}
