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
    public partial class EmployeeSearch : Form
    {
        Main mainForm;
        EmployeeService service = new EmployeeService();
        Employee employee;
        List<Employee> employees;

        public EmployeeSearch(Main main)
        {
            this.mainForm = main;
            InitializeComponent();
        }

        private void EmployeeSearch_Load(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        private void EmployeeSearch_Enter(object sender, EventArgs e)
        {
            txtSearch.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Clear();

            if (txtSearch.Text == "")
            {
                MessageBox.Show("Please input a search term.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (int.TryParse(txtSearch.Text, out int result))
                {
                    employee = service.Get(txtSearch.Text);

                    if (employee == null)
                        MessageBox.Show("No Results found for the specified search criteria.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        DisplayEmployeeInfo(employee);
                }
                else
                {
                    employees = service.Search(txtSearch.Text);

                    if (employees.Count == 0)
                        MessageBox.Show("No Results found for the specified search criteria.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        DisplayEmployees(employees);
                }
            }
        }

        private void dgvEmployees_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            Employee selected = employees.ElementAt(index);
            DisplayEmployeeInfo(selected);
            employee = selected;
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            UpdatePersonalInfo updatePersonalInfo = new UpdatePersonalInfo(employee);
            updatePersonalInfo.ShowDialog();
        }

        private void btnJob_Click(object sender, EventArgs e)
        {
            UpdateJobInfo updateJobInfo = new UpdateJobInfo(employee, mainForm);
            updateJobInfo.ShowDialog();
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            UpdateStatus updateStatus = new UpdateStatus(employee, mainForm);
            updateStatus.ShowDialog();
        }

        /// <summary>
        /// Displays a List of Employees in the DataGridView.
        /// </summary>
        /// <param name="employees">The List of Employees to be displayed.</param>
        private void DisplayEmployees(List<Employee> employees)
        {
            dgvEmployees.DataSource = employees;

            dgvEmployees.Columns.Remove("MiddleInitial");
            dgvEmployees.Columns.Remove("Version");
            dgvEmployees.Columns.Remove("DateOfBirth");
            dgvEmployees.Columns.Remove("StreetAddress");
            dgvEmployees.Columns.Remove("City");
            dgvEmployees.Columns.Remove("PostalCode");
            dgvEmployees.Columns.Remove("SIN");
            dgvEmployees.Columns.Remove("SeniorityDate");
            dgvEmployees.Columns.Remove("JobStartDate");
            dgvEmployees.Columns.Remove("WorkPhone");
            dgvEmployees.Columns.Remove("CellPhone");
            dgvEmployees.Columns.Remove("JobID");
            dgvEmployees.Columns.Remove("DepartmentID");
            dgvEmployees.Columns.Remove("SupervisorID");
            dgvEmployees.Columns.Remove("EmailAddress");
            dgvEmployees.Columns.Remove("FullName");
            dgvEmployees.Columns.Remove("OfficeAddress");
            dgvEmployees.Columns.Remove("OfficeCity");
            dgvEmployees.Columns.Remove("OfficeUnit");
            dgvEmployees.Columns.Remove("Status");
            dgvEmployees.Columns.Remove("EndDate");
        }

        /// <summary>
        /// Displays Employee information.
        /// </summary>
        /// <param name="employee">The Employee to be displayed.</param>
        private void DisplayEmployeeInfo(Employee employee)
        {
            lblFullName.Text = employee.MiddleInitial == null ?
                $"{employee.FirstName} {employee.LastName}" :
                $"{employee.FirstName} {employee.MiddleInitial} {employee.LastName}";

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

            if (mainForm.User.Role == Role.HREmployee || mainForm.User.Role == Role.HRSupervisor)
            {
                btnJob.Enabled = true;
                btnPersonal.Enabled = true;
                btnStatus.Enabled = true;
            }
        }

        /// <summary>
        /// Clears all data from the form.
        /// </summary>
        private void Clear()
        {
            lblFullName.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtWorkPhone.Text = "";
            txtCellPhone.Text = "";
            dgvEmployees.DataSource = null;
            btnJob.Enabled = false;
            btnPersonal.Enabled = false;
            btnStatus.Enabled = false;
        }
    }
}
