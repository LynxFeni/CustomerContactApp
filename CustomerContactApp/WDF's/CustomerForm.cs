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
    public partial class Form1 : Form
    {
        private static string dbConnectionString = ConfigurationManager.ConnectionStrings["CustomerContactConnectionString"].ConnectionString;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
        public void AddCustomerDetails(string name, string latitude, string longitude)
        {
            
            try
            {
                SqlConnection con = new SqlConnection(dbConnectionString);
                SqlCommand cmd = new SqlCommand("InsertingCustomerDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Latitude", latitude);
                cmd.Parameters.AddWithValue("@Longitude", longitude);

                con.Open();
                int i = cmd.ExecuteNonQuery();

                con.Close();

                if (i != 0)
                {
                    textName.Text = String.Empty;
                    textLatitude.Text = String.Empty;
                    textLongitude.Text = String.Empty;
                    MessageBox.Show(i + " Customer details Saved");
                }

            }
            catch (Exception es)
            {
                
                MessageBox.Show(es.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textName.Text))
            {  
                textName.Text = "name is required!";
                textLatitude.Text = "Latitue is required!";
                textLongitude.Text = "Longitude is required!";
            }
            else
            {
                AddCustomerDetails(textName.Text, textLatitude.Text, textLongitude.Text);
            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {

            CustomerContactForm m = new CustomerContactForm();
            this.Hide();
             m.Show();
             

        }
    }
}
