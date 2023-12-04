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
    public partial class UpdateDepartment : Form
    {
        Main mainForm;
        DepartmentService departmentService = new DepartmentService();
        EmployeeService employeeService = new EmployeeService();
        List<Department> departments = new List<Department>();
        Department department;
        User user;

        public UpdateDepartment(Main main, User user)
        {
            this.mainForm = main;
            this.user = user;
            InitializeComponent();
        }

        private void UpdateDepartment_Load(object sender, EventArgs e)
        {
            departments = departmentService.Get();
            cboDepartment.DisplayMember = "Name";
            cboDepartment.ValueMember = "ID";
            cboDepartment.DataSource = departments;
            

            if (user.Role == Role.Supervisor)
            {
                cboDepartment.Enabled = false;
                txtName.Enabled = false;
                dtpInvocationDate.Enabled = false;

                Employee employee = employeeService.Get(user.EmployeeID);
                int departmentIndex = departments.IndexOf(departments.Find(d => d.ID == employee.DepartmentID));
                cboDepartment.SelectedIndex = departmentIndex;
            }
        }

        private void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDepartment.SelectedValue != null)
            {
                department = departmentService.Get((int) cboDepartment.SelectedValue);
                txtName.Text = department.Name;
                txtDescription.Text = department.Description;
                dtpInvocationDate.Value = department.InvocationDate;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            department.Name = txtName.Text;
            department.Description = txtDescription.Text;
            department.InvocationDate = dtpInvocationDate.Value;

            department = departmentService.Update(department);

            ShowMessage(department);
        }

        /// <summary>
        /// Builds the message to display the user and displays it in a MessageBox.
        /// </summary>
        /// <param name="department">The Department updated by the form.</param>
        private void ShowMessage(Department department)
        {
            string msg;
            MessageBoxIcon icon;

            if (department.IsValid())
            {
                msg = "Department Successfuly Updated";
                icon = MessageBoxIcon.Information;
                departments = departmentService.Get();
                int index = cboDepartment.SelectedIndex;
                cboDepartment.DataSource = departments;
                cboDepartment.SelectedIndex = index;
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                foreach (Error error in department.Errors)
                {
                    sb.Append($"{error.Message}\n");
                }

                msg = sb.ToString();
                icon = MessageBoxIcon.Error;
            }

            MessageBox.Show(msg, "", MessageBoxButtons.OK, icon);
        }
    }
}
