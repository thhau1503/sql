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
    public partial class Admin : Form
    {
        string ID;
        string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=DB;Integrated Security=True";
        public Admin(string ID)
        {
            InitializeComponent();
            this.ID = ID;
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddPlayer f = new AddPlayer();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM View_CustomerInfo where username IN (SELECT username FROM Accounts WHERE account_type = 'user')";
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

        private void button2_Click(object sender, EventArgs e)
        {
            AddTick f = new AddTick();
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UpdateTiSo f = new UpdateTiSo();
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LuongTB f = new LuongTB();
            f.ShowDialog();
              
        }
    }
}
