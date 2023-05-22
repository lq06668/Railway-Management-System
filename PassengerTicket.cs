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
using System.Configuration;
using System.Data.OleDb;

namespace Railwaymanagement
{
    public partial class PassengerTicket : Form
    {
        string cs = "Data Source=LAPTOP-FH8L2U68;Initial Catalog=RailwayProject;Integrated Security=True;";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        PassengerSchedule f1;
        public PassengerTicket(PassengerSchedule frm1)
        {
            InitializeComponent();
            this.f1 = frm1;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void PassengerTicket_Load(object sender, EventArgs e)
        {
            textBox16.Text = f1.textBox5.Text;
            textBox2.Text = f1.textBox6.Text;
            textBox1.Text = f1.textBox6.Text;
            textBox4.Text = f1.textBox7.Text;
            textBox5.Text = f1.textBox7.Text;
            textBox6.Text = f1.comboBox4.Text;
            textBox14.Text = f1.comboBox3.Text;

            con = new SqlConnection(cs);
            con.Open();
            sda = new SqlDataAdapter("select Name, Gender from Passenger  where PassengerCNIC = '" + textBox16.Text + "' ", con);

            dt = new DataTable();
            sda.Fill(dt);

            textBox3.Text = dt.Rows[0][0].ToString();
            textBox11.Text = dt.Rows[0][1].ToString();
            con.Close();

            


            if (f1.checkBox3.CheckState == CheckState.Checked)
            {
                textBox8.Text = f1.textBox10.Text;
                textBox10.Text = f1.textBox9.Text;
                textBox9.Text = f1.textBox12.Text;
                textBox12.Text = f1.textBox8.Text;
                
            }

            if (f1.checkBox4.CheckState == CheckState.Checked)
            {
                textBox8.Text = f1.textBox1.Text;
                textBox10.Text = f1.textBox2.Text;
                textBox9.Text = f1.textBox4.Text;
                textBox12.Text = f1.textBox11.Text;
            }

            con = new SqlConnection(cs);
            con.Open();
            sda = new SqlDataAdapter("select MinimumBaggage  from Category  where CategoryType = '" + textBox14.Text + "' ", con);

            dt = new DataTable();
            sda.Fill(dt);

            textBox13.Text = dt.Rows[0][0].ToString();

            con.Close();


            con = new SqlConnection(cs);
            con.Open();
            string myquery = "Select TicketID from Tickets";
            cmd = new SqlCommand();
            cmd.CommandText = myquery;
            cmd.Connection = con;
            sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;
            DataSet ds = new DataSet();
            sda.Fill(ds);
            con.Close();

            if (ds.Tables[0].Rows.Count < 1)
            {
                label17.Text = "1";
            }
            else
            {
                string mycon1 = "Data Source=LAPTOP-FH8L2U68;Initial Catalog=RailwayProject;Integrated Security=True;";
                SqlConnection scon1 = new SqlConnection(mycon1);
                string myquery1 = "Select max(TicketID) from Tickets";
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandText = myquery1;
                cmd1.Connection = scon1;
                SqlDataAdapter sda1 = new SqlDataAdapter();
                sda1.SelectCommand = cmd1;
                DataSet ds1 = new DataSet();
                sda1.Fill(ds1);
                label17.Text = ds1.Tables[0].Rows[0][0].ToString();
                int a;
                a = Convert.ToInt16(label17.Text);
                a = a + 1;
                label17.Text = a.ToString();
                con.Close();

            }

        }

         
  
        private void button1_Click(object sender, EventArgs e)
        {


            //"select idSchedule from Schedule S INNER JOIN Train T ON T.TrainID = S.Train_TrainID where (DepartureStation_DepartureStationID = (select DepartureStationID from DepartureStation where D_StationName = '" + f1.textBox6.Text + "' ))  and (ArrivalStation_ArrivalStationID = (select ArrivalStationID from ArrivalStation where A_StationName = '" + f1.textBox7.Text + "' )) and (Category_categoryID = (select CategoryID from Category where CategoryType ='" + f1.comboBox3.Text + "' )) and (Train_TrainID = select TrainID from Train where Name = '" + textBox12.Text + "' ))", con);

            //string updatepass = "Insert into Ticket (TicketID, Schedule_idSchedule, Passenger_PassengerCNIC, ModeofPayment) values (" + textBox15.Text + ",  select idSchedule from Schedule S INNER JOIN Train T ON T.TrainID = S.Train_TrainID where (DepartureStation_DepartureStationID = (select DepartureStationID from DepartureStation where D_StationName = '" + f1.textBox6.Text + "' ))  and (ArrivalStation_ArrivalStationID = (select ArrivalStationID from ArrivalStation where A_StationName = '" + f1.textBox7.Text + "' )) and (Category_categoryID = (select CategoryID from Category where CategoryType ='" + f1.comboBox3.Text + "' )) and (Train_TrainID = select TrainID from Train where Name = '" + textBox12.Text + "' )) , '" + textBox16.Text + "', '" + textBox7.Text + "')";
            con = new SqlConnection(cs);
            con.Open();

            cmd = new SqlCommand("Insert into Tickets (TicketID, Schedule_idSchedule, Passenger_PassengerCNIC, ModeofPayment) values (@TicketID, @Schedule_idSchedule, @Passenger_PassengerCNIC, @ModeofPayment)", con);
            cmd.Parameters.Add(new SqlParameter("TicketID", label17.Text));
            if (f1.checkBox4.CheckState == CheckState.Checked)
            {
                cmd.Parameters.Add(new SqlParameter("Schedule_idSchedule", f1.textBox13.Text));
            }

            if (f1.checkBox3.CheckState == CheckState.Checked)
            {
                cmd.Parameters.Add(new SqlParameter("Schedule_idSchedule", f1.textBox14.Text));
            }


            cmd.Parameters.Add(new SqlParameter("Passenger_PassengerCNIC", textBox16.Text));
            cmd.Parameters.Add(new SqlParameter("ModeofPayment", comboBox1.Text));
            cmd.ExecuteNonQuery();

            MessageBox.Show("Ticket has been printed!");
            //dt = new DataTable();

            this.Hide();
            Thankyou ss = new Thankyou();
            ss.Show();

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
