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
    public partial class AdminStation2 : Form
    {
        string cs = "Data Source=LAPTOP-FH8L2U68;Initial Catalog=RailwayProject;Integrated Security=True;";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;


        public AdminStation2()
        {
            InitializeComponent();
        }

        private void AdminStation2_Load(object sender, EventArgs e)
        {
            showdata();
        }

        public void showdata()
        {
            con = new SqlConnection(cs);
            sda = new SqlDataAdapter("Select * from DepartureStation", con);
            con.Open();
            dt = new DataTable();
            sda.Fill(dt);

            //dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Enter the information, You can't leave the Text boxes blank");
            }

            con = new SqlConnection(cs);
            //con.Open();
            sda = new SqlDataAdapter("select Count(*) from DepartureStation where DepartureStationID = '" + textBox2.Text + "' AND D_StationName='" + textBox1.Text + "' AND Location='" + textBox3.Text + "' ", con);
            dt = new DataTable();
            sda.Fill(dt);
            //con.Close();
            if (dt.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("This Departure Station already Exists!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

            }
            else
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("Insert into DepartureStation (DepartureStationID, D_StationName, Location) values (@DepartureStationID, @D_StationName, @Location)", con);
                cmd.Parameters.Add(new SqlParameter("DepartureStationID", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("D_StationName", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("Location", textBox3.Text));
                cmd.ExecuteNonQuery();

                MessageBox.Show("New Departure stations have been added");
                con.Close();
                showdata();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            int Id = int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());

            con = new SqlConnection(cs);
            con.Open();
            sda = new SqlDataAdapter("select count(*) from Schedule where DepartureStation_DepartureStationID = '" + Id + "'", con);
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
                cmd = new SqlCommand("DELETE FROM DepartureStation WHERE DepartureStationID= '" + Id + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Departure station has been deleted");
                con.Close();

                showdata();
            }

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox5.Text == "" )
            {
                MessageBox.Show("Enter the information, You can't leave the Text boxes blank");
            }
            else
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                int Id = int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());


                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("UPDATE DepartureStation SET D_StationName = '" + textBox5.Text + "', Location = '" + textBox4.Text + "' WHERE DepartureStationID = '" + Id + "' ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Departure station has been Updated");
                con.Close();

                showdata();
                textBox4.Clear();
                textBox5.Clear();
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            AdminStations ss = new AdminStations();
            ss.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();

            Trains ss = new Trains();
            ss.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

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
