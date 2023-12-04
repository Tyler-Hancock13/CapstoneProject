using CapstoneProject.Model.Entities;
using CapstoneProject.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapstoneProject
{
    public partial class UpdatePersonalInfo : Form
    {
        Employee employee;
        EmployeeService service = new EmployeeService();

        public UpdatePersonalInfo(Employee employee)
        {
            this.employee = employee;
            InitializeComponent();
        }

        private void UpdatePersonalInfo_Load(object sender, EventArgs e)
        {
            employee = service.Get(employee.ID);
            DisplayEmployeeInfo();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            employee.StreetAddress = txtAddress.Text;
            employee.City = txtCity.Text;
            employee.PostalCode = txtPostalCode.Text;
            employee.WorkPhone = txtWorkPhone.Text;
            employee.CellPhone = txtCellPhone.Text;

            employee = service.Update(employee);

            ShowMessage();
        }

        /// <summary>
        /// Displays the Employee's Information
        /// </summary>
        private void DisplayEmployeeInfo()
        {
            txtAddress.Text = employee.StreetAddress;
            txtCity.Text = employee.City;
            txtPostalCode.Text = employee.PostalCode;
            txtWorkPhone.Text = employee.WorkPhone;
            txtCellPhone.Text = employee.CellPhone;
        }

        /// <summary>
        /// Builds the message to display the user and displays it in a MessageBox.
        /// </summary>
        private void ShowMessage()
        {
            string msg;
            MessageBoxIcon icon;

            if (employee.IsValid())
            {
                msg = "Employee Successfuly Updated";
                icon = MessageBoxIcon.Information;
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                foreach (Error error in employee.Errors)
                {
                    sb.Append($"{error.Message}\n");
                }

                msg = sb.ToString();
                icon = MessageBoxIcon.Error;

                employee = service.Get(employee.ID);
                DisplayEmployeeInfo();
            }

            MessageBox.Show(msg, "", MessageBoxButtons.OK, icon);
        }
    }
}
