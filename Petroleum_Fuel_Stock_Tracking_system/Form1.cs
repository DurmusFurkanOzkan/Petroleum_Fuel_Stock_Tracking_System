using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Petroleum_Fuel_Stock_Tracking_system
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-48MHJFV;Initial Catalog=Petroleum_Fuel_Stock_Tracking_System;Integrated Security=True");
        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select * from Admin where Account_Number=@p1 and Password=@p2", connection);
            command.Parameters.AddWithValue("@p1", int.Parse(txt_account_number.Text));
            command.Parameters.AddWithValue("@p2", txt_password.Text);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                Form2 form3 = new Form2();
                form3.Show();
            }
            else
            {
                MessageBox.Show("Invalid Log in");
            }
            connection.Close();
        }
    }
}
