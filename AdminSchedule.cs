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
    public partial class AdminSchedule : Form
    {
        string cs = "Data Source=LAPTOP-FH8L2U68;Initial Catalog=RailwayProject;Integrated Security=True;";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public AdminSchedule()
        {
            InitializeComponent();
        }

        private void AdminSchedule_Load(object sender, EventArgs e)
        {
            showdata();

            con = new SqlConnection(cs);
            //dataGridView1.DataSource = dt;
            comboBox2.Items.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select categoryID from Category";
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                comboBox2.Items.Add(dr["categoryID"].ToString());
            }

            con.Close();

            con = new SqlConnection(cs);
            //dataGridView1.DataSource = dt;
            comboBox1.Items.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select TrainID from Train";
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["TrainID"].ToString());
            }

            con.Close();

            con = new SqlConnection(cs);
            //dataGridView1.DataSource = dt;
            comboBox4.Items.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select DepartureStationID from DepartureStation";
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                comboBox4.Items.Add(dr["DepartureStationID"].ToString());
            }

            con.Close();

            con = new SqlConnection(cs);
            //dataGridView1.DataSource = dt;
            comboBox3.Items.Clear();
            con.Open();
            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select ArrivalStationID from ArrivalStation";
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                comboBox3.Items.Add(dr["ArrivalStationID"].ToString());
            }

            con.Close();

        }

        public void showdata()
        {
            con = new SqlConnection(cs);
            sda = new SqlDataAdapter("Select * from Schedule", con);
            con.Open();
            dt = new DataTable();
            sda.Fill(dt);

            //dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "" || comboBox4.Text == "" || comboBox1.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "")
            {
                MessageBox.Show("Enter the information, You can't leave the Text boxes blank");
            }

            con = new SqlConnection(cs);
            //con.Open();
            sda = new SqlDataAdapter("select Count(*) from Schedule where Category_CategoryID = '"+ comboBox2.Text+ "' AND Train_TrainID='" + comboBox1.Text+ "' AND DepartureStation_DepartureStationID = '"+ comboBox4.Text+ "' AND ArrivalStation_ArrivalStationID = '" + comboBox3.Text+ "' AND ArrivalTime = '" +textBox7.Text+ "' AND DepartureTime = '" +textBox6.Text+ "' AND Fare = '"+textBox9.Text+"' and Day_ ='" + textBox8.Text + "' ", con);
            dt = new DataTable();
            sda.Fill(dt);
            //con.Close();
            if (dt.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("This Schedule already Exists!");
                textBox1.Clear();
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                comboBox1.Text = string.Empty;
                comboBox2.Text = string.Empty;
                comboBox3.Text = string.Empty;
                comboBox4.Text = string.Empty;
            }
            else
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("Insert into Schedule (idSchedule, Category_CategoryID, Train_TrainID, DepartureStation_DepartureStationID, ArrivalStation_ArrivalStationID, ArrivalTime, DepartureTime, Fare, Day_) values (@idSchedule, @Category_CategoryID, @Train_TrainID, @DepartureStation_DepartureStationID, @ArrivalStation_ArrivalStationID, @ArrivalTime, @DepartureTime, @Fare, @Day_)", con);
                cmd.Parameters.Add(new SqlParameter("idSchedule", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("Category_CategoryID", comboBox2.Text));
                cmd.Parameters.Add(new SqlParameter("Train_TrainID", comboBox1.Text));
                cmd.Parameters.Add(new SqlParameter("DepartureStation_DepartureStationID", comboBox4.Text));
                cmd.Parameters.Add(new SqlParameter("ArrivalStation_ArrivalStationID", comboBox3.Text));
                cmd.Parameters.Add(new SqlParameter("ArrivalTime", textBox7.Text));
                cmd.Parameters.Add(new SqlParameter("DepartureTime", textBox6.Text));
                cmd.Parameters.Add(new SqlParameter("Fare", textBox9.Text));
                cmd.Parameters.Add(new SqlParameter("Day_", textBox8.Text));
                cmd.ExecuteNonQuery();

                MessageBox.Show("New Schedule has been added");
                con.Close();
                showdata();
                textBox1.Clear();
                comboBox1.Text = string.Empty;
                comboBox2.Text = string.Empty;
                comboBox3.Text = string.Empty;
                comboBox4.Text = string.Empty;
                textBox6.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
            }

            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            int Id = int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());



            con = new SqlConnection(cs);
            con.Open();
            cmd = new SqlCommand("DELETE FROM Schedule WHERE idSchedule= '" + Id + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Schedule has been removed!");
            con.Close();

            showdata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == "" || textBox11.Text == "" || textBox12.Text == "" || textBox13.Text == "" || textBox14.Text == "" || textBox15.Text == "" || textBox16.Text == "" || textBox17.Text == "")
            {
                MessageBox.Show("Enter the information, You can't leave the Text boxes blank");
            }
            else
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                int Id = int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());


                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("UPDATE Schedule SET  Category_categoryID = '" + textBox17.Text + "', Train_TrainID = '" + textBox16.Text + "', DepartureStation_DepartureStationID = '" + textBox15.Text + "', ArrivalStation_ArrivalStationID = '" + textBox14.Text + "', ArrivalTime = '" + textBox13.Text + "', DepartureTime = '" + textBox12.Text + "', Fare = '" + textBox11.Text + "',Day_ = '" + textBox10.Text + "' WHERE idSchedule = '" + Id + "' ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Schedules have been Updated");
                con.Close();

                showdata();
                textBox10.Clear();
                textBox11.Clear();
                textBox12.Clear();
                textBox13.Clear();
                textBox14.Clear();
                textBox15.Clear();
                textBox16.Clear();
                textBox17.Clear();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            AdminStations ss = new AdminStations();
            ss.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();

            AdminStation2 ss = new AdminStation2();
            ss.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();

            Trains ss = new Trains();
            ss.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 ss = new Form1();
            ss.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_Passenger ss = new Admin_Passenger();
            ss.Show();
        }
    }
}
