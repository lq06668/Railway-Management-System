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
    public partial class Trains : Form
    {
        string cs = "Data Source=LAPTOP-FH8L2U68;Initial Catalog=RailwayProject;Integrated Security=True;";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public Trains()
        {
            InitializeComponent();
        }

        public void showdata()
        {
            con = new SqlConnection(cs);
            sda = new SqlDataAdapter("Select * from Train", con);
            con.Open();
            dt = new DataTable();
            sda.Fill(dt);

            //dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        private void Trains_Load(object sender, EventArgs e)
        {
            showdata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Enter the information, You can't leave the Text boxes blank");
            }

            con = new SqlConnection(cs);
            //con.Open();
            sda = new SqlDataAdapter("select Count(*) from Train  where TrainID = '" + textBox2.Text + "' AND Name='" + textBox1.Text + "' ", con);
            dt = new DataTable();
            sda.Fill(dt);
            //con.Close();
            if (dt.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("This Train already Exists!");
                textBox1.Clear();
                textBox2.Clear();
                
            }
            else
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("Insert into Train (TrainID, Name) values (@TrainID, @Name)", con);
                cmd.Parameters.Add(new SqlParameter("TrainID", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("Name", textBox1.Text));
                cmd.ExecuteNonQuery();

                MessageBox.Show("New Trains have been added");
                con.Close();
                showdata();
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            int Id = int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());

            con = new SqlConnection(cs);
            con.Open();
            sda = new SqlDataAdapter("select count(*) from Schedule where Train_TrainID = '" + Id + "'", con);
            dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            if (dt.Rows[0][0].ToString() != "0")
            {
                MessageBox.Show("Sorry you can't delete this Train directly, you'll have to remove its connections from Schedule first");
            }
            else
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("DELETE FROM Train WHERE TrainID= '" + Id + "'", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Train has been deleted");
                con.Close();

                showdata();

            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
                MessageBox.Show("Enter the information, You can't leave this Text box blank");
            }
            else
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                int Id = int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());


                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("UPDATE Train SET Name = '" + textBox5.Text + "' WHERE TrainID = '" + Id + "' ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Train's Information has been Updated");
                con.Close();

                showdata();
                textBox5.Clear();
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();

            AdminSchedule ss = new AdminSchedule();
            ss.Show();
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
