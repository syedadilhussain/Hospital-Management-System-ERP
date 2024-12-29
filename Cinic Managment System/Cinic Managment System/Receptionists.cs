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
            DisplayRec();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=FAIZAN\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True;TrustServerCertificate=True");
        private void DelBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Please Select a Receptionist to Delete");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM ReceptionistTB WHERE RecepId = @RID", Con);
                    cmd.Parameters.AddWithValue("@RID", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Receptionist Deleted");
                    Con.Close();
                    DisplayRec();
                }
                catch (Exception Ex)
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

        private void DisplayRec()
        {
            Con.Open();
            string Query = "SELECT * FROM ReceptionistTB";
            SqlDataAdapter sda= new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder= new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ReceptionistDGV.DataSource = ds.Tables[0];
            Con.Close();

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
                    DisplayRec();

                }
                catch(Exception Ex)
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

        private void RAddressTB_TextChanged(object sender, EventArgs e)
        {

        }
        int Key = 0;
        private void ReceptionistDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure the clicked row index is valid
            {
                DataGridViewRow row = ReceptionistDGV.Rows[e.RowIndex];
                RNameTB.Text = row.Cells[1].Value?.ToString();
                RPhoneTB.Text = row.Cells[2].Value?.ToString();
                RAddressTB.Text = row.Cells[3].Value?.ToString();
                RPassword.Text = row.Cells[4].Value?.ToString();

                // Assign a unique key based on the row
                Key = Convert.ToInt32(row.Cells[0].Value); // Assuming the ID is in the first column
            }
            else
            {
                MessageBox.Show("Please select a valid row.");
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Please Select a Receptionist to Edit");
            }
            else if (RNameTB.Text == "" || RPhoneTB.Text == "" || RAddressTB.Text == "" || RPassword.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE ReceptionistTB SET RecepName = @RN, RecepPhone = @RP, RecepAddress = @RA, RecepPass = @RPA WHERE RecepId = @RID", Con);
                    cmd.Parameters.AddWithValue("@RN", RNameTB.Text);
                    cmd.Parameters.AddWithValue("@RP", RPhoneTB.Text);
                    cmd.Parameters.AddWithValue("@RA", RAddressTB.Text);
                    cmd.Parameters.AddWithValue("@RPA", RPassword.Text);
                    cmd.Parameters.AddWithValue("@RID", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Receptionist Updated");
                    Con.Close();
                    DisplayRec();
                    Clear();
                }
                catch (Exception Ex)
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
        private void Clear()
        {
            RNameTB.Text = "";
            RPassword.Text = "";
            RPhoneTB.Text = "";
            RAddressTB.Text = "";
            Key = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
