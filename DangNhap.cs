using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace QL_DoiBong
{
    public partial class DangNhap : Form
    {
        string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DB;Integrated Security=True";
        public DangNhap()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("LoginUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@username", textBox1.Text);
                    command.Parameters.AddWithValue("@password", textBox2.Text);
                    command.Parameters.Add("@loggedIn", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    connection.Open();
                    command.ExecuteNonQuery();

                    bool loggedIn = (bool)command.Parameters["@loggedIn"].Value;

                    if (radioButton1.Checked==true && loggedIn)
                    {
                        User f = new User(textBox1.Text);
                        f.ShowDialog();
                    }
                    else if (radioButton2.Checked ==true && loggedIn)
                    {
                        Admin a = new Admin(textBox1.Text);
                        a.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập thất bại");
                    }
                }
            }

        }
    }
}
