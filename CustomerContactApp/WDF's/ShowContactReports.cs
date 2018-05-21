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
    public partial class ShowContactReports : Form
    {
        private static string dbConnectionString = ConfigurationManager.ConnectionStrings["CustomerContactConnectionString"].ConnectionString;
       
        public ShowContactReports()
        {
            InitializeComponent();
        }

        private void ShowContactReports_Load(object sender, EventArgs e)
        {
            GetCustomerContactList();
        }
        public  void  GetCustomerContactList()
        {
           
            try
            {
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(dbConnectionString);
                conn.Open();
                SqlCommand myCmd = new SqlCommand("ExtractCustomerDetails", conn);
                myCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(myCmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;


            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            DialogResult dialog = new DialogResult();

            dialog = MessageBox.Show("Do you want to close?", "Alert!", MessageBoxButtons.YesNo);

            if (dialog == DialogResult.Yes)
            {
                System.Environment.Exit(1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 m = new Form1();
            this.Hide();
            m.Show();
        }
    }
}
