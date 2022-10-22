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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-48MHJFV;Initial Catalog=Petroleum_Fuel_Stock_Tracking_System;Integrated Security=True");
        private void Form2_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("Select Name,Purchase_price,Sale_price from Petroleum", connection);
            SqlDataReader dr = command.ExecuteReader();
            int i = 0;
            while(dr.Read())
            {   if(i==0)
                {
                    label1.Text = dr[0].ToString();
                    Kursunsuz95_lbl_pp.Text = dr[1].ToString();
                    Kursunsuz95_lbl_sp.Text = dr[2].ToString();
                }
                else if(i==1)
                {

                    label4.Text = dr[0].ToString();
                    label7.Text = dr[1].ToString();
                    label8.Text = dr[2].ToString();
                }
                else if (i == 2)
                {
                    label5.Text = dr[0].ToString();
                    label9.Text = dr[1].ToString();
                    label11.Text = dr[2].ToString();
                }
                else
                {
                    label6.Text = dr[0].ToString();
                    label10.Text = dr[1].ToString();
                    label12.Text = dr[2].ToString();
                }
                i++;

            }
            connection.Close();
            connection.Open();
            SqlCommand command1 = new SqlCommand("Select Stock from Petroleum", connection);
            SqlDataReader dr1 = command1.ExecuteReader();
            int j = 0;
            while(dr1.Read())
            {   if(j==0)
                {
                    Progress_bar(progressBar1, double.Parse(dr1[0].ToString()));
                }
                else if (j == 1)
                {
                    Progress_bar(progressBar2, double.Parse(dr1[0].ToString()));
                }
                else if (j == 2)
                {
                    Progress_bar(progressBar3, double.Parse(dr1[0].ToString()));
                }
                else if (j == 3)
                {
                    Progress_bar(progressBar4, double.Parse(dr1[0].ToString()));
                }
                j++;
            }
            connection.Close();
            connection.Open();
            SqlCommand command2 = new SqlCommand("Select safe from Safe", connection);
            SqlDataReader dr2 = command2.ExecuteReader();
            if(dr2.Read())
            {
                label20.Text = dr2[0].ToString();
            }
            connection.Close();
        }
        public void Progress_bar(ProgressBar pr,double value)
        {
            pr.Value = (int)(value * 1.0f); 
        }
        public void Update(string txt1,string txt2,ProgressBar progress_Bar,string txt3,string txt4)

        {
            connection.Open();
            SqlCommand command = new SqlCommand("Update Petroleum set Stock=Stock-@p1 where Name=@p2", connection);
            command.Parameters.AddWithValue("@p1", txt1);
            command.Parameters.AddWithValue("@p2", txt2);
            command.ExecuteNonQuery();
            connection.Close();
            connection.Open();
            SqlCommand command1 = new SqlCommand("Select Stock from Petroleum where Name=@p3", connection);
            command1.Parameters.AddWithValue("@p3", txt2);
            SqlDataReader dr1 = command1.ExecuteReader();
            if (dr1.Read())
            {
                Progress_bar(progress_Bar, double.Parse(dr1[0].ToString()));
            }
            connection.Close();
            connection.Open();
            SqlCommand command2 = new SqlCommand("Update Safe set safe=safe+@p1", connection);
            decimal result = (decimal.Parse(txt3) - decimal.Parse(txt4)) * decimal.Parse(txt1) / 100;
            command2.Parameters.AddWithValue("@p1", decimal.Parse(result.ToString()));
            command2.ExecuteNonQuery();
            connection.Close();
            connection.Open();
            SqlCommand command3 = new SqlCommand("Select safe from Safe", connection);
            SqlDataReader dr3 = command3.ExecuteReader();
            if (dr3.Read())
            {
                label20.Text = dr3[0].ToString();
            }
            connection.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Update(txt_first.Text, label1.Text, progressBar1, Kursunsuz95_lbl_sp.Text, Kursunsuz95_lbl_pp.Text);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Update(txt_second.Text, label4.Text, progressBar2, label8.Text, label7.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Update(txt_third.Text, label5.Text, progressBar3, label11.Text, label9.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Update(txt_fourth.Text, label6.Text, progressBar4, label12.Text, label10.Text);
        }
    }
}
