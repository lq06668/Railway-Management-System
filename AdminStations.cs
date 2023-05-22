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
    public partial class AdminStations : Form
    {
        string cs = "Data Source=LAPTOP-FH8L2U68;Initial Catalog=RailwayProject;Integrated Security=True;";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        public AdminStations()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void AdminStations_Load(object sender, EventArgs e)
        {
            showdata();
        }

        public void showdata()
        {
            con = new SqlConnection(cs);
            sda = new SqlDataAdapter("Select * from ArrivalStation", con);
            con.Open();
            dt = new DataTable();
            sda.Fill(dt);

            //dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Enter the information, You can't leave the Text boxes blank");
            }

            con = new SqlConnection(cs);
            //con.Open();
            sda = new SqlDataAdapter("select Count(*) from ArrivalStation where ArrivalStationID = '" + textBox2.Text + "' AND A_StationName='" + textBox1.Text + "' AND Location='"+ textBox3.Text + "' ", con);
            dt = new DataTable();
            sda.Fill(dt);
            //con.Close();
            if (dt.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("This Arrival Station already Exists!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

            }

            else
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("Insert into ArrivalStation (ArrivalStationID, A_StationName, Location) values (@ArrivalStationID, @A_StationName, @Location)", con);
                cmd.Parameters.Add(new SqlParameter("ArrivalStationID", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("A_StationName", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("Location", textBox3.Text));
                cmd.ExecuteNonQuery();

                MessageBox.Show("New Arrival stations have been added");
                con.Close();
                showdata();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Hide();

            AdminStation2 ss = new AdminStation2();
            ss.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            int Id = int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());

            con = new SqlConnection(cs);
            con.Open();
            sda = new SqlDataAdapter("select count(*) from Schedule where ArrivalStation_ArrivalStationID = '" + Id + "'", con);
            dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            if (dt.Rows[0][0].ToString() != "0")
            {
                MessageBox.Show("Sorry you can't delete this Station directly, you'll have to remove its connections from Schedule first");
            }
            else
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("DELETE FROM ArrivalStation WHERE ArrivalStationID= '" + Id + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Arrival station has been deleted");
                con.Close();

                showdata();
            }


            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Enter the information, You can't leave the Text boxes blank");
            }
            else
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                int Id = int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());


                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("UPDATE ArrivalStation SET A_StationName = '" + textBox5.Text + "', Location = '" + textBox4.Text + "' WHERE ArrivalStationID = '" + Id + "' ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Arrival station has been Updated");
                con.Close();

                showdata();
                textBox4.Clear();
                textBox5.Clear();

            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();

            Trains ss = new Trains();
            ss.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();

            AdminSchedule ss = new AdminSchedule();
            ss.Show();
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            Admin_Passenger ss = new Admin_Passenger();
            ss.Show();
        }
    }
}
