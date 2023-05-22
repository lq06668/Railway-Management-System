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
    public partial class PassengerSchedule : Form
    {
        string cs = "Data Source=LAPTOP-FH8L2U68;Initial Catalog=RailwayProject;Integrated Security=True;";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public PassengerSchedule()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            PassengerMain ss = new PassengerMain();
            ss.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

            PassengerMain ss = new PassengerMain();
            ss.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*this.Hide();

            PassengerTicket ss = new PassengerTicket();
            ss.Show();*/
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Select * from DepartureStation where Location ='" + comboBox1.Text + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string Location = (string)reader["Location"].ToString();
                textBox6.Text = Location;
            }

            con.Close();
            
        }

        private void PassengerSchedule_Load(object sender, EventArgs e)
        {
            button2.Visible= false;
            button7.Visible= false;
            label14.Visible= false;
            textBox5.Visible= false;    
            checkBox3.Visible= false;
            checkBox4.Visible= false;

            label13.Visible = false;
            button8.Visible = false;
            button4.Visible = false;

            con = new SqlConnection(cs);
            //dataGridView1.DataSource = dt;
            comboBox1.Items.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select Location from DepartureStation";
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            foreach (DataRow dr in dt.Rows){
                comboBox1.Items.Add(dr["Location"].ToString());
            }

            con.Close();


            con = new SqlConnection(cs);
            //dataGridView1.DataSource = dt;
            comboBox2.Items.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select Location from ArrivalStation";
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["Location"].ToString());
            }

            con.Close();

            con = new SqlConnection(cs);
            //dataGridView1.DataSource = dt;
            comboBox3.Items.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select CategoryType from Category";
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                comboBox3.Items.Add(dr["CategoryType"].ToString());
            }

            con.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmd = new SqlCommand("Select * from ArrivalStation where Location ='" + comboBox2.Text + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string Location = (string)reader["Location"].ToString();
                textBox7.Text = Location;
            }

            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label13.Visible = true;
            button8.Visible = true;
            button4.Visible = true;

            con = new SqlConnection(cs);
            con.Open();
            sda = new SqlDataAdapter("select idSchedule, T.Name, ArrivalTime, DepartureTime, Fare from Schedule S INNER JOIN Train T ON T.TrainID = S.Train_TrainID where (DepartureStation_DepartureStationID = (select DepartureStationID from DepartureStation where D_StationName = '" + textBox6.Text + "' ))  and (ArrivalStation_ArrivalStationID = (select ArrivalStationID from ArrivalStation where A_StationName = '" + textBox7.Text + "' )) and (Category_categoryID = (select CategoryID from Category where CategoryType ='" + comboBox3.Text + "' ))", con);

            dt = new DataTable();
            sda.Fill(dt);

            textBox13.Text = dt.Rows[0][0].ToString();
            textBox11.Text = dt.Rows[0][1].ToString();
            textBox2.Text = dt.Rows[0][2].ToString();
            textBox1.Text = dt.Rows[0][3].ToString();
            textBox4.Text = dt.Rows[0][4].ToString();

            textBox14.Text = dt.Rows[1][0].ToString();
            textBox8.Text = dt.Rows[1][1].ToString();
            textBox9.Text = dt.Rows[1][2].ToString();
            textBox10.Text = dt.Rows[1][3].ToString();
            textBox12.Text = dt.Rows[1][4].ToString();

      

        con.Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show("Please Enter your CNIC");
            }

            else if (checkBox3.CheckState == CheckState.Unchecked && checkBox4.CheckState == CheckState.Unchecked)
            {
                MessageBox.Show("Please Choose one of the given Slots");
            }

            con = new SqlConnection(cs);
            sda = new SqlDataAdapter("select Count(*) from Passenger where PassengerCNIC='" + textBox5.Text + "' ", con);
            dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                PassengerTicket f2 = new PassengerTicket(this);
                f2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sorry you're not a Registered Passenger!");
            }


            
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            button7.Visible = true;
            label14.Visible = true;
            textBox5.Visible = true;
            checkBox3.Visible = true;
            checkBox4.Visible = true;

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
