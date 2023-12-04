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
using static CapstoneProject.Model.Types;

namespace CapstoneProject
{
    public partial class UpdateStatus : Form
    {
        Employee employee;
        EmployeeService service = new EmployeeService();
        Main main;

        public UpdateStatus(Employee employee, Main main)
        {
            this.employee = employee;
            this.main = main;
            InitializeComponent();
        }

        private void UpdateStatus_Load(object sender, EventArgs e)
        {
            employee = service.Get(employee.ID);

            cboStatus.DataSource = Enum.GetValues(typeof(EmployeeStatus));
            DisplayEmployeeInfo();

            if (employee.Status == EmployeeStatus.Retired)
            {
                cboStatus.Enabled = false;
                dtpEndDate.Enabled = false;
                btnUpdate.Enabled = false;
            }
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtpEndDate.Enabled = true;

            if ((EmployeeStatus)cboStatus.SelectedItem == EmployeeStatus.Retired || (EmployeeStatus)cboStatus.SelectedItem == EmployeeStatus.Active)
            {
                dtpEndDate.Enabled = false;
                dtpEndDate.Value = DateTime.Now;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (main.User.EmployeeID == employee.ID && main.User.Role == Role.HREmployee)
            {
                employee.Errors.Add(new Error("HR Employees cannot edit their own status.", ErrorType.Business));
            }
            else
            {
                employee.Status = (EmployeeStatus)cboStatus.SelectedItem;

                if ((EmployeeStatus)cboStatus.SelectedItem != EmployeeStatus.Active)
                {
                    employee.EndDate = dtpEndDate.Value;
                }
                else
                {
                    employee.EndDate = DateTime.MinValue;
                }

                employee = service.Update(employee);
            }

            ShowMessage();
        }

        /// <summary>
        /// Displays Employee information.
        /// </summary>
        private void DisplayEmployeeInfo()
        {
            lblFullName.Text = employee.MiddleInitial == null ?
                $"{employee.FirstName} {employee.LastName}" :
                $"{employee.FirstName} {employee.MiddleInitial} {employee.LastName}";

            txtSIN.Text = employee.SIN;
            txtAddress.Text = $"{employee.StreetAddress}, {employee.City}, {employee.PostalCode}";
            txtEmail.Text = employee.EmailAddress;

            if (employee.WorkPhone != null)
            {
                txtWorkPhone.Text = employee.WorkPhone;
            }

            if (employee.CellPhone != null)
            {
                txtCellPhone.Text = employee.CellPhone;
            }

            txtSeniorityDate.Text = employee.SeniorityDate.ToString("dddd, MMMM d, yyyy");
            txtStatus.Text = employee.Status.ToString();
            cboStatus.SelectedItem = employee.Status;

            if (employee.EndDate != DateTime.MinValue)
            {
                dtpEndDate.Value = employee.EndDate;
            }
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

                if (employee.Status == EmployeeStatus.Retired)
                {
                    dtpEndDate.Enabled = false;
                    cboStatus.Enabled = false;
                    btnUpdate.Enabled = false;
                }
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
