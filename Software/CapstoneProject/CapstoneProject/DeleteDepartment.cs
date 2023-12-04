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
    public partial class DeleteDepartment : Form
    {
        Main mainForm;
        DepartmentService service = new DepartmentService();

        public DeleteDepartment(Main main)
        {
            mainForm = main;
            InitializeComponent();
        }

        private void DeleteDepartment_Load(object sender, EventArgs e)
        {
            cboDepartment.DisplayMember = "Name";
            cboDepartment.ValueMember = "ID";
            cboDepartment.DataSource = service.Get();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Department department = service.Get(Convert.ToInt32(cboDepartment.SelectedValue));
            department = service.Delete(department);
            cboDepartment.DataSource = service.Get();
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
                msg = $"The {department.Name} Department has been Successfuly Deleted";
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
    }
}
