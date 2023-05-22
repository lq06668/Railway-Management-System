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

namespace Railwaymanagement
{
    public partial class PassengerMain : Form
    {
        //string cs = "Data Source=LAPTOP-FH8L2U68;Initial Catalog=RailwayProject;Integrated Security = True;";
        string cs = "Data Source=LAPTOP-FH8L2U68;Initial Catalog=RailwayProject;Integrated Security=True;";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public PassengerMain()
        {
            InitializeComponent();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 ss = new Form1();
            ss.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void PassengerMain_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Hide();

            PassengerSchedule ss = new PassengerSchedule();
            ss.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Visible = true;
            if (textBox1.Text == "" || textBox3.Text == "" || textBox5.Text == "" || comboBox1.Text == "" || textBox2.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Enter your information, You can't leave the Text boxes blank");
            }
            else
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("Insert into Passenger (PassengerCNIC, Email, Name, Gender, Age, PhoneNumber) values (@PassengerCNIC, @Email, @Name, @Gender, @Age, @PhoneNumber)", con);
                cmd.Parameters.Add(new SqlParameter("PassengerCNIC", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("Email", textBox3.Text));
                cmd.Parameters.Add(new SqlParameter("Name", textBox5.Text));
                cmd.Parameters.Add(new SqlParameter("Gender", comboBox1.Text));
                cmd.Parameters.Add(new SqlParameter("Age", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("PhoneNumber", textBox6.Text));
                cmd.ExecuteNonQuery();

                MessageBox.Show("Passenger Data has been inserted");
                button4.Visible = false;
                button7.Visible = true;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();

            PassengerSchedule ss = new PassengerSchedule();
            ss.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(cs);
            //con.Open();
            sda = new SqlDataAdapter("select Count(*) from Passenger where PassengerCNIC='" + textBox4.Text + "'", con);
            dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                PassengerSchedule ss = new PassengerSchedule();
                ss.Show();
            }
            else
            {
                MessageBox.Show("Sorry you are not a registered user, please Register yourself");
                textBox4.Clear();
            }
        }
    }
}
