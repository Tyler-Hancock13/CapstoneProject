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
    public partial class UpdateJobInfo : Form
    {
        Employee employee;
        User user;
        List<Job> jobs;
        List<Department> departments;
        List<Employee> supervisors;
        EmployeeService employeeService = new EmployeeService();
        DepartmentService departmentService = new DepartmentService();
        JobService jobService = new JobService();
        UserService userService = new UserService();
        Main main;

        public UpdateJobInfo(Employee employee, Main main)
        {
            this.employee = employee;
            this.main = main;
            InitializeComponent();
        }

        private void UpdateJobInfo_Load(object sender, EventArgs e)
        {
            employee = employeeService.Get(employee.ID);
            user = userService.Get(employee.ID);
            departments = departmentService.Get();
            jobs = jobService.Get();

            cboDepartment.DisplayMember = "Name";
            cboDepartment.ValueMember = "ID";
            cboDepartment.DataSource = departments;

            cboJob.DisplayMember = "Name";
            cboJob.ValueMember = "ID";
            cboJob.DataSource = jobs;

            cboRole.DataSource = Enum.GetValues(typeof(Role));

            DisplayEmployeeInfo();
        }

        private void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateSupervisorDropdown();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (main.User.EmployeeID == employee.ID && main.User.Role == Role.HREmployee)
            {
                employee.Errors.Add(new Error("HR Employees cannot edit their own job information.", ErrorType.Business));
            }
            else
            {
                employee.SIN = txtSIN.Text;
                employee.JobID = (int)cboJob.SelectedValue;
                employee.JobStartDate = dtpJobStartDate.Value;
                employee.DepartmentID = (int)cboDepartment.SelectedValue;
                employee.OfficeAddress = txtOfficeAddress.Text;
                employee.OfficeCity = txtOfficeCity.Text;
                employee.OfficeUnit = Convert.ToInt32(nudUnit.Value);

                if (cboSupervisor.SelectedValue != null)
                {
                    employee.SupervisorID = cboSupervisor.SelectedValue.ToString();
                }

                user.Role = (Role)cboRole.SelectedItem;

                Tuple<Employee, User> results = employeeService.Update(employee, user);
                employee = results.Item1;
                user = results.Item2;
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

            cboJob.SelectedIndex = jobs.IndexOf(jobs.Find(j => j.ID == employee.JobID));
            cboDepartment.SelectedIndex = departments.IndexOf(departments.Find(d => d.ID == employee.DepartmentID));
            cboRole.SelectedItem = user.Role;

            PopulateSupervisorDropdown();

            cboSupervisor.SelectedIndex = employee.SupervisorID == null
                ? -1
                : supervisors.IndexOf(supervisors.Find(s => s.ID == employee.SupervisorID));

            txtOfficeAddress.Text = employee.OfficeAddress;
            txtOfficeCity.Text = employee.OfficeCity;
            nudUnit.Value = Convert.ToDecimal(employee.OfficeUnit);
        }

        /// <summary>
        /// Populates the Supervisor Dropdown with the Supervisors for the selected Department.
        /// </summary>
        private void PopulateSupervisorDropdown()
        {
            supervisors = employeeService.GetSupervisors().Where(s => s.DepartmentID == Convert.ToInt32(cboDepartment.SelectedValue)).ToList();
            cboSupervisor.DisplayMember = "FullName";
            cboSupervisor.ValueMember = "ID";
            cboSupervisor.DataSource = supervisors;
        }

        /// <summary>
        /// Builds the messsage to be displayed to the user and displays it in a MessageBox.
        /// </summary>
        private void ShowMessage()
        {
            string msg;
            MessageBoxIcon icon;

            if (employee.IsValid() && user.IsValid())
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

                foreach (Error error in user.Errors)
                {
                    sb.Append($"{error.Message}\n");
                }

                msg = sb.ToString();
                icon = MessageBoxIcon.Error;

                employee = employeeService.Get(employee.ID);
                user = userService.Get(employee.ID);
                DisplayEmployeeInfo();
            }

            MessageBox.Show(msg, "", MessageBoxButtons.OK, icon);
        }
    }
}
