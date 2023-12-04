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
    public partial class CreateDepartment : Form
    {
        Main mainForm;
        DepartmentService service = new DepartmentService();


        public CreateDepartment(Main main)
        {
            mainForm = main;
            InitializeComponent();
        }

        private void CreateDepartment_Load(object sender, EventArgs e)
        {
            txtName.Focus();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            Department department = PopulateDepartment();

            department = service.Add(department);
            
            ShowMessage(department);
        }

        /// <summary>
        /// Builds the message to display the user and displays it in a MessageBox.
        /// </summary>
        /// <param name="department">The Department created by the form.</param>
        private static void ShowMessage(Department department)
        {
            string msg;
            MessageBoxIcon icon;

            if (department.IsValid())
            {
                msg = "Department Successfuly Created";
                icon = MessageBoxIcon.Information;
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

        /// <summary>
        /// Populates a Department model with data from the form.
        /// </summary>
        /// <returns>The populated Department object.</returns>
        private Department PopulateDepartment()
        {
            return new Department()
            {
                Name = txtName.Text,
                Description = txtDescription.Text,
                InvocationDate = dtpInvocationDate.Value
            };
        }
    }
}
