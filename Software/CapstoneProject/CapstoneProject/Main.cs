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
    public partial class Main : Form
    {
        EmployeeSearch employeeSearch;
        CreateDepartment createDepartment;
        CreateEmployee createEmployee;
        PurchaseOrderRequests purchaseOrders;
        ProcessPurchaseOrders processPO;
        UpdateDepartment updateDepartment;
        DeleteDepartment deleteDepartment;
        string employeeId;
        Employee employee;
        UserService userService = new UserService();
        ReviewService reviewService = new ReviewService(new EmailService(), new EmployeeService());
        EmployeeService employeeService = new EmployeeService();
        public User User;

        public Main()
        {
            InitializeComponent();
        }

        public Main(string id)
        {
            employeeId = id;
            User = userService.Get(employeeId);
            employee = employeeService.Get(employeeId);

            if (User.Role == Role.HREmployee || User.Role == Role.HRSupervisor)
            {
                if (!reviewService.SendReminders())
                    MessageBox.Show("Something went wrong sending the Review Reminders.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            tslUserName.Text = $"Logged In As: {employee.FullName}";
            tslUserRole.Text = "Role: ";

            switch (User.Role)
            {
                case Role.Employee:
                    tslUserRole.Text += "Employee";
                    break;
                case Role.HREmployee:
                    tslUserRole.Text += "HR Employee";
                    break;
                case Role.HRSupervisor:
                    tslUserRole.Text += "HR Supervisor";
                    break;
                case Role.Supervisor:
                    tslUserRole.Text += "Supervisor";
                    break;
            }
        }

        private void tsbSearchEmployees_Click(object sender, EventArgs e)
        {
            NavigateToEmployeeSearch();
        }

        private void tsmiSearchEmployees_Click(object sender, EventArgs e)
        {
            NavigateToEmployeeSearch();
        }

        private void tsbAddEmployee_Click(object sender, EventArgs e)
        {
            NavigateToCreateEmployee();
        }

        private void tsmiAddEmployee_Click(object sender, EventArgs e)
        {
            NavigateToCreateEmployee();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NavigateToCreateDepartment();
        }

        private void tsmiAddDepartment_Click(object sender, EventArgs e)
        {
            NavigateToCreateDepartment();
        }

        private void viewAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NavigateToPurchaseOrders();
        }

        private void tsmiUpdateDepartment_Click(object sender, EventArgs e)
        {
            NavigateToUpdateDepartment();
        }

        private void tsmiDeleteDepartment_Click(object sender, EventArgs e)
        {
            NavigateToDeleteDepartment();
        }

        private void NavigateToDeleteDepartment()
        {
            if (deleteDepartment == null || deleteDepartment.IsDisposed)
                deleteDepartment = new DeleteDepartment(this);

            if (tabControl.Contains(deleteDepartment))
                tabControl.TabPages[deleteDepartment].Select();
            else
                tabControl.TabPages.Add(deleteDepartment);
        }

        private void NavigateToUpdateDepartment()
        {
            if (updateDepartment == null || updateDepartment.IsDisposed)
                updateDepartment = new UpdateDepartment(this, User);

            if (tabControl.Contains(updateDepartment))
                tabControl.TabPages[updateDepartment].Select();
            else
                tabControl.TabPages.Add(updateDepartment);
        }

        private void NavigateToEmployeeSearch()
        {
            if (employeeSearch == null || employeeSearch.IsDisposed)
                employeeSearch = new EmployeeSearch(this);

            if (tabControl.Contains(employeeSearch))
                tabControl.TabPages[employeeSearch].Select();
            else
                tabControl.TabPages.Add(employeeSearch);
        }

        private void NavigateToCreateEmployee()
        {
            if (createEmployee == null || createEmployee.IsDisposed)
                createEmployee = new CreateEmployee(this);

            if (tabControl.Contains(createEmployee))
                tabControl.TabPages[createEmployee].Select();
            else
                tabControl.TabPages.Add(createEmployee);
        }

        private void NavigateToCreateDepartment()
        {
            if (createDepartment == null || createDepartment.IsDisposed)
                createDepartment = new CreateDepartment(this);

            if (tabControl.Contains(createDepartment))
                tabControl.TabPages[createDepartment].Select();
            else
                tabControl.TabPages.Add(createDepartment);
        }

        private void NavigateToPurchaseOrders()
        {
            if (purchaseOrders == null || purchaseOrders.IsDisposed)
            {
                purchaseOrders = new PurchaseOrderRequests(User, this);
            }

            if (tabControl.Contains(purchaseOrders))
            {
                tabControl.TabPages[purchaseOrders].Select();
            }
            else
            {
                tabControl.TabPages.Add(purchaseOrders);
            }
        }

        public void ConfigureNavigation()
        {
            if (User.Role == Role.HREmployee || User.Role == Role.HRSupervisor)
            {
                tsbAddEmployee.Visible = true;
                tsbCreateDepartment.Visible = true;
                tsmiAddEmployee.Visible = true;
                tsmiAddDepartment.Visible = true;
                tsmiDeleteDepartment.Visible = true;
                tsmiDepartments.Visible = true;
            }

            if(User.Role == Role.HRSupervisor || User.Role == Role.Supervisor)
            {
                tsbProcessPO.Visible = true;
                tsmiProcessPO.Visible = true;
            }

            if (User.Role == Role.Supervisor || User.Role == Role.HRSupervisor || User.Role == Role.HREmployee)
            {
                tsmiUpdateDepartment.Visible = true;
                tsmiDepartments.Visible = true;
            }
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            if (purchaseOrders == null || purchaseOrders.IsDisposed)
            {
                purchaseOrders = new PurchaseOrderRequests(User, this);
            }

            if (tabControl.Contains(purchaseOrders))
            {
                tabControl.TabPages[purchaseOrders].Select();
            }
            else
            {
                tabControl.TabPages.Add(purchaseOrders);
            }
        }

        private void tsmiProcessPO_Click(object sender, EventArgs e)
        {
            if(processPO == null || processPO.IsDisposed)
            {
                processPO = new ProcessPurchaseOrders(User, this);
            }

            if (tabControl.Contains(processPO))
            {
                tabControl.TabPages[processPO].Select();
            }
            else
            {
                tabControl.TabPages.Add(processPO);
            }
        }

        private void tsbProcessPO_Click(object sender, EventArgs e)
        {
            if (processPO == null || processPO.IsDisposed)
            {
                processPO = new ProcessPurchaseOrders(User, this);
            }

            if (tabControl.Contains(processPO))
            {
                tabControl.TabPages[processPO].Select();
            }
            else
            {
                tabControl.TabPages.Add(processPO);
            }
        }
    }
}
