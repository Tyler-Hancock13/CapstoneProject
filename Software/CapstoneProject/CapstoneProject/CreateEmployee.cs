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
using static CapstoneProject.Model.Types;

namespace CapstoneProject
{
    public partial class CreateEmployee : Form
    {
        Main mainForm;
        EmployeeService employeeService = new EmployeeService();
        DepartmentService departmentService = new DepartmentService();
        JobService jobService = new JobService();

        public CreateEmployee(Main main)
        {
            mainForm = main;
            InitializeComponent();
        }

        private void CreateEmployee_Load(object sender, EventArgs e)
        {
            cboDepartment.DataSource = departmentService.Get();
            cboDepartment.DisplayMember = "Name";
            cboDepartment.ValueMember = "ID";

            cboJob.DataSource = jobService.Get();
            cboJob.DisplayMember = "Name";
            cboJob.ValueMember = "ID";

            cboRole.DataSource = Enum.GetValues(typeof(Role));

            PopulateSupervisorDropdown();

            txtFirstName.Focus();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Employee employee = PackEmployee();
            User user = PackUser();

            Tuple<Employee, User> tuple = employeeService.Add(employee, user);
            employee = tuple.Item1;
            user = tuple.Item2;

            ShowMessage(employee, user);
        }

        private void cboDepartment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            PopulateSupervisorDropdown();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        /// <summary>
        /// Clears all fields.
        /// </summary>
        private void Clear()
        {
            txtFirstName.Text = "";
            txtMiddleInitial.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtPostalCode.Text = "";
            dtpDateOfBirth.Value = DateTime.Now;
            txtEmail.Text = "";
            txtWorkPhone.Text = "";
            txtCellPhone.Text = "";
            txtSIN.Text = "";
            cboJob.SelectedIndex = 0;
            cboDepartment.SelectedIndex = 0;
            dtpSeniorityDate.Value = DateTime.Now;
            dtpJobStartDate.Value = DateTime.Now;
            cboSupervisor.SelectedIndex = 0;
            cboRole.SelectedIndex = 0;
            txtPassword.Text = "";
            txtOfficeAddress.Text = "";
        }

        /// <summary>
        /// Builds the messsage to be displayed to the user and displays it in a MessageBox.
        /// </summary>
        /// <param name="employee">The Employee created from the form.</param>
        /// <param name="user">The User created from the form.</param>
        private void ShowMessage(Employee employee, User user)
        {
            string msg;
            MessageBoxIcon icon;

            if (employee.IsValid() && user.IsValid())
            {
                msg = "Employee Successfuly Created";
                icon = MessageBoxIcon.Information;
                Clear();
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                foreach (Error error in employee.Errors)
                {
                    sb.Append($"{error.Message}\n");
                }

                foreach (Error error in user.Errors)
                {
                    sb.Append($"{error.Message}\n");
                }

                msg = sb.ToString();
                icon = MessageBoxIcon.Error;
            }

            MessageBox.Show(msg, "", MessageBoxButtons.OK, icon);
        }

        /// <summary>
        /// Populates an Employee model with data from the form.
        /// </summary>
        /// <returns>The populated Employee object.</returns>
        private Employee PackEmployee()
        {
            return new Employee
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                MiddleInitial = txtMiddleInitial.Text,
                DateOfBirth = dtpDateOfBirth.Value,
                StreetAddress = txtAddress.Text,
                City = txtCity.Text,
                PostalCode = txtPostalCode.Text,
                SIN = txtSIN.Text,
                SeniorityDate = dtpSeniorityDate.Value,
                JobStartDate = dtpJobStartDate.Value,
                WorkPhone = txtWorkPhone.Text == "" ? null : txtWorkPhone.Text,
                CellPhone = txtCellPhone.Text == "" ? null : txtCellPhone.Text,
                EmailAddress = txtEmail.Text,
                Status = EmployeeStatus.Active,
                OfficeAddress = txtOfficeAddress.Text,
                OfficeCity = txtOfficeCity.Text,
                OfficeUnit = Convert.ToInt32(nudUnit.Value),
                JobID = Convert.ToInt32(cboJob.SelectedValue),
                DepartmentID = Convert.ToInt32(cboDepartment.SelectedValue),
                SupervisorID = cboSupervisor.SelectedValue.ToString()
            };
        }

        /// <summary>
        /// Populates a User model with data from the form.
        /// </summary>
        /// <returns>The populated User object.</returns>
        private User PackUser()
        {
            return new User
            {
                Role = (Role)cboRole.SelectedItem,
                Password = txtPassword.Text
            };
        }

        /// <summary>
        /// Populates the Supervisor Dropdown with the Supervisors for the selected Department.
        /// </summary>
        private void PopulateSupervisorDropdown()
        {
            cboSupervisor.DataSource = employeeService.GetSupervisors().Where(s => s.DepartmentID == Convert.ToInt32(cboDepartment.SelectedValue)).ToList();
            cboSupervisor.DisplayMember = "FullName";
            cboSupervisor.ValueMember = "ID";
        }
    }
}
