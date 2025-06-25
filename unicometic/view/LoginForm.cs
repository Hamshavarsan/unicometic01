using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Views
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Enter both username and password.", "Login Failed");
                return;
            }

            using (var conn = DatabaseManager.GetConnection())
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM Users WHERE Username = @username AND Password = @password";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    var reader = await cmd.ExecuteReaderAsync();

                    if (reader.Read())
                    {
                        string role = reader["Role"].ToString();
                        int userId = Convert.ToInt32(reader["UserID"]);

                        this.Hide();
                        MainForm mainForm = new MainForm(username, role, userId);
                        mainForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid credentials.", "Login Failed");
                    }
                }
            }
        }
    }
}
