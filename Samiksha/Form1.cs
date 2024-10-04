using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace MyProjectTy125
{
    public partial class Form1 : Form
    {
        private string connectionString = "server=localhost;database=dermatologist_appointment;user=samu;password=samiksha"; // Update with your MySQL connection string

        public Form1()
        {
            InitializeComponent();
            LoadComboBoxItems();
        }

        private void LoadComboBoxItems()
        {
            // Add items to the combo box
            comboBox1.Items.Add("Cash");
            comboBox1.Items.Add("Credit Card");
            comboBox1.Items.Add("Debit Card");
            comboBox1.Items.Add("Other");
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Logic to add data to the database
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO appointments (Yourname, PhoneNumber, Day, Membership, Issues, PaymentMode) VALUES (@name, @phone, @day, @membership, @issues, @paymentMode)";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", textBox1.Text);
                    cmd.Parameters.AddWithValue("@phone", textBox2.Text);
                    cmd.Parameters.AddWithValue("@day", DateTime.Now.ToShortDateString());
                    cmd.Parameters.AddWithValue("@membership", radioButton1.Checked ? "Yes" : "No");
                    cmd.Parameters.AddWithValue("@issues", GetIssues());
                    cmd.Parameters.AddWithValue("@paymentMode", comboBox1.SelectedItem?.ToString());

                    cmd.ExecuteNonQuery(); // Execute the insert command
                }
            }

            MessageBox.Show("Record added successfully!"); // Message after successful addition
            ClearFields();
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            // MySQL connection
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Select query to retrieve data
                string sql = "SELECT * FROM appointments"; // Make sure your table name is correct

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                // Add data to DataGridView
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["Yourname"], reader["PhoneNumber"],
                                            reader["Day"], reader["Membership"],
                                            reader["Issues"], reader["PaymentMode"]);
                }
            }
        }

        private string GetIssues()
        {
            string issues = "";
            if (checkBox1.Checked) issues += "Skin Cancer, ";
            if (checkBox2.Checked) issues += "Acne, ";
            if (checkBox3.Checked) issues += "Rashes, ";
            if (checkBox4.Checked) issues += "Pimple, ";
            if (checkBox5.Checked) issues += "Scars, ";

            // Optionally truncate if necessary
            if (issues.Length > 255) // Adjust based on your column's max length
            {
                issues = issues.Substring(0, 255);
                MessageBox.Show("Issues text truncated to fit database column.");
            }

            return issues.TrimEnd(',', ' '); // Remove trailing comma and space
        }

        private void ClearFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            comboBox1.SelectedIndex = -1;
            textBox3.Clear();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
