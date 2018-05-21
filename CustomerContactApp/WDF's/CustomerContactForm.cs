using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerContactApp
{
    public partial class CustomerContactForm : Form
    {
        private static string dbConnectionString = ConfigurationManager.ConnectionStrings["CustomerContactConnectionString"].ConnectionString;
        int customerID = 0;
        public CustomerContactForm()
        {
            InitializeComponent();
        }

        private void CustomerContactForm_Load(object sender, EventArgs e)
        {
            getCustomerID();
        }
     
        public void getCustomerID()
        {

            using (SqlConnection con = new SqlConnection(dbConnectionString))
            {
                try
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter("select * from customer", con))
                    {
                        //Fill the DataTable with records from Table.
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        //Insert the Default Item to DataTable.
                        DataRow row = dt.NewRow();
                        row[0] = 0;
                        row[1] = "Please select Customer Name";
                        dt.Rows.InsertAt(row, 0);

                        //Assign DataTable as DataSource.
                        comboBoxCustomerID.DataSource = dt;
                        comboBoxCustomerID.DisplayMember = "Name";
                        comboBoxCustomerID.ValueMember = "CustomerId";
                    }
                }
                catch (Exception es)
                {
                    
                   MessageBox.Show(es.Message);
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                string messageName = comboBoxCustomerID.Text;
                messageName += Environment.NewLine;
                //messageName += comboBoxCustomerID.SelectedValue;

                int custID;
                string messageCustID = "";
                messageCustID += comboBoxCustomerID.SelectedValue;

                custID = int.Parse(messageCustID);

                if (string.IsNullOrEmpty(custID.ToString()))
                {
                    MessageBox.Show("name is required!");
                }
                else
                {
                    AddCustomerContactDetails(messageName, textBoxEmail.Text, textBoxContNumber.Text, custID);
                }
             
               
            

          
        }
        public void AddCustomerContactDetails(string name, string email, string contactNumber, int customerID)
        {
            

            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlCommand cmd = new SqlCommand("InsertingCustomerContactDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
            cmd.Parameters.AddWithValue("@CustomerID", customerID);

            con.Open();
            int i = cmd.ExecuteNonQuery();

            con.Close();

            if (i != 0)
            {
                textBoxEmail.Text = String.Empty;
                textBoxContNumber.Text = String.Empty;
                MessageBox.Show(i + "Customer contact details Saved");
            }
            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowContactReports m = new ShowContactReports();
            this.Hide();
            m.Show();
        }

       
    }
}

