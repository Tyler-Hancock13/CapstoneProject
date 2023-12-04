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
    public partial class PurchaseOrderRequests : Form
    {
        EmployeeService employeeService = new EmployeeService();
        RequestService requestService = new RequestService();

        private List<PurchaseOrder> _purchaseOrders;
        private Employee emp;
        private PurchaseOrder _purchaseOrder = new PurchaseOrder();
        private User user;

        public PurchaseOrderRequests()
        {
            InitializeComponent();
        }

        public PurchaseOrderRequests(User user, Main main)
        {
            this.user = user;
            InitializeComponent();
        }

        private void RequestCreation_Load(object sender, EventArgs e)
        {
            try
            {
                List<PurchaseOrder> purchaseOrders = requestService.GetPurchaseOrderByEmployeeId(user.EmployeeID);
                dgvPurchaseOrders.DataSource = purchaseOrders;

                DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                btnColumn.HeaderText = "View Order";
                btnColumn.Text = "View";
                dgvPurchaseOrders.Columns.Add(btnColumn);
                dgvPurchaseOrders.Columns[7].DefaultCellStyle.Format = "c";
                dgvPurchaseOrders.Columns[8].DefaultCellStyle.Format = "c";
                dgvPurchaseOrders.Columns[9].DefaultCellStyle.Format = "c";
                dgvPurchaseOrders.Columns[2].Visible = false;
                dgvPurchaseOrders.Columns[5].Visible = false;
                RenameColumns();
                dgvPurchaseOrders.Columns[4].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPurchaseOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _purchaseOrder.PONumber = (Convert.ToInt32(dgvPurchaseOrders.Rows[e.RowIndex].Cells[1].Value));
                RequestDetails requestForm = new RequestDetails(_purchaseOrder, user);
                requestForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                RequestService requestService = new RequestService();
                EmployeeService employeeService = new EmployeeService();

                if (txtSearch.Text != "")
                {
                    _purchaseOrders = requestService.SearchById(Convert.ToInt32(txtSearch.Text), user.EmployeeID);

                    if (_purchaseOrders.Count > 0)
                    {
                        dgvPurchaseOrders.DataSource = _purchaseOrders;

                        //if (dgvPurchaseOrders.Columns.Count != 9)
                        //{
                        //    DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                        //    btnColumn.HeaderText = "View Order";
                        //    btnColumn.Text = "View";
                        //    dgvPurchaseOrders.Columns.Add(btnColumn);
                        //}
                    }
                    else
                    {
                        MessageBox.Show("No results found.");
                    }
                }
                else
                {
                    _purchaseOrders = requestService.SearchByDate(Convert.ToDateTime(dtpStartDate.Value), Convert.ToDateTime(dtpEndDate.Value), user.EmployeeID);

                    if (_purchaseOrders.Count > 0)
                    {
                        dgvPurchaseOrders.DataSource = _purchaseOrders;

                        if (dgvPurchaseOrders.Columns.Count != 9)
                        {
                            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                            btnColumn.HeaderText = "View Order";
                            btnColumn.Text = "View";
                            dgvPurchaseOrders.Columns.Add(btnColumn);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No results found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                CreateNewPurchaseOrder(_purchaseOrder);
                emp = employeeService.Get(user.EmployeeID);
                RequestDetails createRequestForm = new RequestDetails(_purchaseOrder, user);
                createRequestForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CreateNewPurchaseOrder(PurchaseOrder order)
        {
            order.PONumber = 0;
            order.Subtotal = 0m;
            order.SalesTax = 0;
            order.Total = 500;
            order.DateCreated = DateTime.Now;
            order.EmployeeId = user.EmployeeID;
            order.DepartmentId = 2;
            order.Status = Model.Types.RequestStatus.Pending;
            order.Items = new List<PurchaseOrderItem>();
        }

        private void RenameColumns()
        {
            dgvPurchaseOrders.Columns[3].HeaderText = "Employee";
            dgvPurchaseOrders.Columns[4].HeaderText = "Supervisor";
            dgvPurchaseOrders.Columns[6].HeaderText = "Department";
        }

        private void ValidateSearch()
        {
            if(dtpStartDate.Value > dtpEndDate.Value)
            {
                MessageBox.Show("End date cannot be before end date. Please try again", "Invalid Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PurchaseOrderRequests_Activated(object sender, EventArgs e)
        {
            try
            {
                List<PurchaseOrder> purchaseOrders = requestService.GetPurchaseOrderByEmployeeId(user.EmployeeID);
                dgvPurchaseOrders.DataSource = purchaseOrders;
                dgvPurchaseOrders.Columns[7].DefaultCellStyle.Format = "c";
                dgvPurchaseOrders.Columns[8].DefaultCellStyle.Format = "c";
                dgvPurchaseOrders.Columns[9].DefaultCellStyle.Format = "c";
                dgvPurchaseOrders.Columns[2].Visible = false;
                dgvPurchaseOrders.Columns[5].Visible = false;
                RenameColumns();
                dgvPurchaseOrders.Columns[4].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
