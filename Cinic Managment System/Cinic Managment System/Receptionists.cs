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

namespace Cinic_Managment_System
{
    public partial class Receptionists : Form
    {
        public Receptionists()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=FAIZAN\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;TrustServerCertificate=True");
        private void DelBtn_Click(object sender, EventArgs e)
        {

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if(RNameTB.Text == "" || RPhoneTB.Text == "" || RAddressTB.Text == "" || RPassword.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO ReceptionistTB (RecepName, RecepPhone, RecepAddress, RecepPass) VALUES (@RN, @RP, @RA, @RPA)", Con);
                    cmd.Parameters.AddWithValue("@RN", RNameTB.Text);
                    cmd.Parameters.AddWithValue("@RP", RPhoneTB.Text);
                    cmd.Parameters.AddWithValue("@RA", RAddressTB.Text);
                    cmd.Parameters.AddWithValue("@RPA", RPassword.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Receptionist Added");
                    Con.Close();

                }catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    if (Con.State == ConnectionState.Open)
                    {
                        Con.Close();
                    }
                }
            }
        }

        private void RNameTB_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
