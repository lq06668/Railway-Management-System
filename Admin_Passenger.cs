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
    public partial class Admin_Passenger : Form
    {
        string cs = "Data Source=LAPTOP-FH8L2U68;Initial Catalog=RailwayProject;Integrated Security=True;";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        public Admin_Passenger()
        {
            InitializeComponent();
        }

        private void Admin_Passenger_Load(object sender, EventArgs e)
        {
            showdata();
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

        public void showdata()
        {
            con = new SqlConnection(cs);
            sda = new SqlDataAdapter("Select * from Passenger", con);
            con.Open();
            dt = new DataTable();
            sda.Fill(dt);

            //dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == ""|| textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Enter the information, You can't leave the Text boxes blank");
            }

            con = new SqlConnection(cs);
            //con.Open();
            sda = new SqlDataAdapter("select Count(*) from Passenger where Email ='" + textBox2.Text + "' AND Name ='" + textBox3.Text + "' AND Gender ='" + textBox5.Text + "' AND Age ='" + textBox6.Text + "' AND PhoneNumber ='" + textBox4.Text + "' ", con);
            dt = new DataTable();
            sda.Fill(dt);
            //con.Close();
            if (dt.Rows[0][0].ToString() == "1")
            {
                MessageBox.Show("This Passenger already Exists!");
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();

            }

            else
            {
                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("Insert into Passenger (PassengerCNIC, Email, Name, Gender, Age, PhoneNumber) values (@PassengerCNIC, @Email, @Name, @Gender, @Age, @PhoneNumber)", con);
                cmd.Parameters.Add(new SqlParameter("PassengerCNIC", textBox1.Text));
                cmd.Parameters.Add(new SqlParameter("Email", textBox2.Text));
                cmd.Parameters.Add(new SqlParameter("Name", textBox3.Text));
                cmd.Parameters.Add(new SqlParameter("Gender", textBox5.Text));
                cmd.Parameters.Add(new SqlParameter("Age", textBox6.Text));
                cmd.Parameters.Add(new SqlParameter("PhoneNumber", textBox4.Text));
                cmd.ExecuteNonQuery();

                MessageBox.Show("New Passenger has been added");
                con.Close();
                showdata();

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            int Id = int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());



            con = new SqlConnection(cs);
            con.Open();
            cmd = new SqlCommand("DELETE FROM Passenger WHERE PassengerCNIC= '" + Id + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Passenger has been removed!");
            con.Close();

            showdata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "" || textBox11.Text == "")
            {
                MessageBox.Show("Enter the information, You can't leave the Text boxes blank");
            }
            else
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                int Id = int.Parse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString());


                con = new SqlConnection(cs);
                con.Open();
                cmd = new SqlCommand("UPDATE Passenger SET Email ='" + textBox11.Text + "' , Name ='" + textBox10.Text + "' , Gender ='" + textBox9.Text + "' , Age ='" + textBox7.Text + "' , PhoneNumber ='" + textBox8.Text + "' WHERE PassengerCNIC = '" + Id + "' ", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Passenger Information has been Updated");
                con.Close();

                showdata();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                textBox10.Clear();
                textBox11.Clear();
                
            }
        }
    }
}
