using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DON_T_KNOW
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textName.Text;
                string address = textAddress.Text;
                string contact = textContact.Text;
                int age = Convert.ToInt32(textAge.Text);
                string gender = comboGender.Text;
                string blood = textBlood.Text;
                string disease = textDisease.Text;
                int pid = Convert.ToInt32(textPatientId.Text);

                // Establish the connection
                using (SqlConnection con = new SqlConnection("Data Source = localhost; Initial Catalog = hospital; Integrated Security = True"))
                {
                    con.Open(); // Open the connection

                    // Construct the SQL command with parameters to prevent SQL injection
                    string sqlQuery = "INSERT INTO AddPatient (Name, Address, Contact, Age, Gender, Blood, Disease, PatientId) VALUES (@Name, @Address, @Contact, @Age, @Gender, @Blood, @Disease, @PatientId)";

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        // Add parameters to the command
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.Parameters.AddWithValue("@Contact", contact);
                        cmd.Parameters.AddWithValue("@Age", age);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@Blood", blood);
                        cmd.Parameters.AddWithValue("@Disease", disease);
                        cmd.Parameters.AddWithValue("@PatientId", pid);

                        // Execute the command
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            MessageBox.Show("Saved Successfully.");
            textName.Clear();
            textAddress.Clear();
            textContact.Clear();
            textAge.Clear();
            textBlood.Clear();
            textDisease.Clear();
            textPatientId.Clear();
            comboGender.ResetText();
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                try
                {
                    int pid = Convert.ToInt32(textBox1.Text);

                    using (SqlConnection con = new SqlConnection("Data Source=localhost; Initial Catalog=hospital; Integrated Security=True"))
                    {
                        con.Open();

                        string sqlQuery = "SELECT * FROM AddPatient WHERE PatientId = @PatientId";

                        using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@PatientId", pid);

                            SqlDataAdapter DA = new SqlDataAdapter(cmd); // Pass the command to the SqlDataAdapter
                            DataSet ds = new DataSet();
                            DA.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                dataGridView1.DataSource = ds.Tables[0];
                            }
                            else
                            {
                                MessageBox.Show("No records found for the specified PatientId.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid PatientId.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int pid = Convert.ToInt32(textBox1.Text);
                string sympt = textSymptoms.Text;
                string diag = textDaignosis.Text;
                string medi = textMedicines.Text;
                string war = comboWardRequire.Text;
                string warT = comboTypeOfWard.Text;

                using (SqlConnection con = new SqlConnection("Data Source=localhost; Initial Catalog=hospital; Integrated Security=True"))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "INSERT INTO PatientMore VALUES (@PatientId, @Symptoms, @Diagnosis, @Medicines, @WardRequired, @TypeOfWard)";
                        cmd.Parameters.AddWithValue("@PatientId", pid);
                        cmd.Parameters.AddWithValue("@Symptoms", sympt);
                        cmd.Parameters.AddWithValue("@Diagnosis", diag);
                        cmd.Parameters.AddWithValue("@Medicines", medi);
                        cmd.Parameters.AddWithValue("@WardRequired", war);
                        cmd.Parameters.AddWithValue("@TypeOfWard", warT);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            MessageBox.Show("Data is Saved Successfully.");

            textDaignosis.Clear();
            textMedicines.Clear();
            textSymptoms.Clear();
            comboWardRequire.ResetText();
            comboTypeOfWard.ResetText();
            textBox1.Clear();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = true;
            panel4.Visible = false;

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=localhost; Initial Catalog=hospital; Integrated Security=True";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from AddPatient inner join PatientMore on AddPatient.PatientId = PatientMore.PatientId";
            SqlDataAdapter DA = new SqlDataAdapter(cmd);
            DataSet DS = new DataSet();
            DA.Fill(DS);

            dataGridView2.DataSource = DS.Tables[0];

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = "Data Source=localhost; Initial Catalog=hospital; Integrated Security=True";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                // First, delete from the "PatientMore" table
                cmd.CommandText = "DELETE FROM PatientMore";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                // Then, delete from the "AddPatient" table
                cmd.CommandText = "DELETE FROM AddPatient";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Data deleted successfully.");
                cmd.CommandText = "Select * from AddPatient inner join PatientMore on AddPatient.PatientId = PatientMore.PatientId";
                SqlDataAdapter DA = new SqlDataAdapter(cmd);
                DataSet DS = new DataSet();
                DA.Fill(DS);

                dataGridView3.DataSource = DS.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
;

        }
    }
}
