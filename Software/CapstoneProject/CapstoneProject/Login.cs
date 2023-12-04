using CapstoneProject.Model.Entities;
using CapstoneProject.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapstoneProject
{
    public partial class Login : Form
    {
        private User user;

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserService userService = new UserService();
            string password = GetMd5Hash(txtPassword.Text);
            user = userService.Get(txtEmployeeId.Text, password);

            if(user != null){
                this.Hide();
                Main mainForm = new Main(user.EmployeeID);
                mainForm.FormClosed += (s, args) => this.Close();
                mainForm.Show();
                mainForm.ConfigureNavigation();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password. Please Try Again.", "Invalid Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = string.Empty;
                txtEmployeeId.SelectAll();
                txtEmployeeId.Focus();
            }
        }

        public string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            Splash splash = new Splash();

            splash.ShowDialog();
            txtPassword.UseSystemPasswordChar = true;
        }
    }
}
